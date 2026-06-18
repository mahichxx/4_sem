import { useReducer, type ReactNode, type Dispatch } from "react";
import { z } from "zod";
import { useAuth } from "../contexts/AuthContext";

const RegistrationSchema = z.object({
    email: z.string().regex(/^[^\s@]+@[^\s@]+\.[^\s@]+$/, "Введите корректный email"),
    password: z.string().min(8, "Пароль должен содержать минимум 8 символов"),
    username: z.string().min(1, "Имя пользователя не может быть пустым"),
    city: z.string().min(1, "Укажите ваш город"),
    occupation: z.string().min(1, "Выберите вашу деятельность"),
    terms: z.literal(true, { message: "Необходимо принять правила пользования" }),
});

const Step1Schema = RegistrationSchema.pick({ email: true, password: true });
const Step2Schema = RegistrationSchema.pick({ username: true, city: true });
const Step3Schema = RegistrationSchema.pick({ occupation: true, terms: true });

type IFormData = z.infer<typeof RegistrationSchema>;

interface IFormState {
    currentStep: 1 | 2 | 3;
    formData: IFormData;
    errors: Partial<Record<keyof IFormData, string>>;
    isSubmitting: boolean;
    isSuccess: boolean;
}

type TFormAction =
    | { type: "UPDATE_FIELD"; field: keyof IFormData; value: string | boolean }
    | { type: "SET_ERRORS"; errors: IFormState["errors"] }
    | { type: "NEXT_STEP" }
    | { type: "PREV_STEP" }
    | { type: "SUBMIT_START" }
    | { type: "SUBMIT_SUCCESS" };

const initialState: IFormState = {
    currentStep: 1,
    formData: { email: "", password: "", username: "", city: "", occupation: "", terms: false as true },
    errors: {},
    isSubmitting: false,
    isSuccess: false,
};

function validateStep(step: number, formData: IFormData): IFormState["errors"] {
    const schemaMap: Record<number, z.ZodType> = { 1: Step1Schema, 2: Step2Schema, 3: Step3Schema };
    const schema = schemaMap[step];
    if (!schema) return {};

    const result = schema.safeParse(formData);
    if (result.success) return {};

    const errors: IFormState["errors"] = {};
    for (const issue of result.error.issues) {
        const field = issue.path[0] as keyof IFormData;
        if (field && !errors[field]) {
            errors[field] = issue.message;
        }
    }
    return errors;
}

function registrationReducer(state: IFormState, action: TFormAction): IFormState {
    switch (action.type) {
        case "UPDATE_FIELD":
            return {
                ...state,
                formData: { ...state.formData, [action.field]: action.value },
                errors: { ...state.errors, [action.field]: undefined },
            };
        case "SET_ERRORS":
            return { ...state, errors: action.errors };
        case "NEXT_STEP": {
            const errors = validateStep(state.currentStep, state.formData);
            if (Object.keys(errors).length > 0) return { ...state, errors };
            return { ...state, currentStep: (state.currentStep + 1) as 1 | 2 | 3, errors: {} };
        }
        case "PREV_STEP":
            return { ...state, currentStep: (Math.max(1, state.currentStep - 1)) as 1 | 2 | 3, errors: {} };
        case "SUBMIT_START":
            return { ...state, isSubmitting: true };
        case "SUBMIT_SUCCESS":
            return { ...state, isSubmitting: false, isSuccess: true };
        default:
            return state;
    }
}

interface FieldProps {
    label: string;
    error?: string;
    children: ReactNode;
}

function Field({ label, error, children }: FieldProps) {
    return (
        <div className="field">
            <label>{label}</label>
            {children}
            {error && <span className="err-msg">{error}</span>}
        </div>
    );
}

interface StepProps {
    formData: IFormData;
    errors: IFormState["errors"];
    dispatch: Dispatch<TFormAction>;
}

function Step1({ formData, errors, dispatch }: StepProps) {
    const up = (field: keyof IFormData, value: string) =>
        dispatch({ type: "UPDATE_FIELD", field, value });
    return (
        <>
            <p className="step-label">Шаг 1 из 3</p>
            <h1 className="step-title">Создать аккаунт</h1>
            <p className="step-sub">Начнём с базовой информации для входа</p>
            <Field label="EMAIL" error={errors.email}>
                <input type="email" className={errors.email ? "err" : ""} placeholder="you@example.com"
                       value={formData.email} onChange={(e) => up("email", e.target.value)} />
            </Field>
            <Field label="ПАРОЛЬ" error={errors.password}>
                <input type="password" className={errors.password ? "err" : ""} placeholder="Минимум 8 символов"
                       value={formData.password} onChange={(e) => up("password", e.target.value)} />
            </Field>
        </>
    );
}

function Step2({ formData, errors, dispatch }: StepProps) {
    const up = (field: keyof IFormData, value: string) =>
        dispatch({ type: "UPDATE_FIELD", field, value });
    return (
        <>
            <p className="step-label">Шаг 2 из 3</p>
            <h1 className="step-title">Ваш профиль</h1>
            <p className="step-sub">Расскажите немного о себе</p>
            <Field label="ИМЯ ПОЛЬЗОВАТЕЛЯ" error={errors.username}>
                <input type="text" className={errors.username ? "err" : ""} placeholder="@username"
                       value={formData.username} onChange={(e) => up("username", e.target.value)} />
            </Field>
            <Field label="ГОРОД" error={errors.city}>
                <input type="text" className={errors.city ? "err" : ""} placeholder="Минск"
                       value={formData.city} onChange={(e) => up("city", e.target.value)} />
            </Field>
        </>
    );
}

const OCCUPATIONS = ["", "Разработчик", "Дизайнер", "Менеджер", "Маркетолог", "Предприниматель", "Студент", "Другое"];

function Step3({ formData, errors, dispatch }: StepProps) {
    const up = (field: keyof IFormData, value: string | boolean) =>
        dispatch({ type: "UPDATE_FIELD", field, value });
    return (
        <>
            <p className="step-label">Шаг 3 из 3</p>
            <h1 className="step-title">О вас</h1>
            <p className="step-sub">Последний шаг — почти готово!</p>
            <Field label="РОД ДЕЯТЕЛЬНОСТИ" error={errors.occupation}>
                <select className={errors.occupation ? "err" : ""} value={formData.occupation}
                        onChange={(e) => up("occupation", e.target.value)}>
                    {OCCUPATIONS.map((o) => <option key={o} value={o}>{o || "Выберите вариант..."}</option>)}
                </select>
            </Field>
            <div className={`checkbox-row ${errors.terms ? "err-box" : ""}`}
                 onClick={() => up("terms", !formData.terms)}>
                <input type="checkbox" checked={formData.terms} onChange={() => {}} />
                <span>Я прочитал и принимаю <strong>Правила пользования</strong> и <strong>Политику конфиденциальности</strong></span>
            </div>
            {errors.terms && <span className="err-msg">{errors.terms}</span>}
        </>
    );
}

export default function RegistrationForm() {
    const [state, dispatch] = useReducer(registrationReducer, initialState);
    const { currentStep, formData, errors, isSubmitting } = state;
    const { login } = useAuth();

    const handleNext = () => dispatch({ type: "NEXT_STEP" });
    const handleBack = () => dispatch({ type: "PREV_STEP" });

    const handleSubmit = () => {
        const errs = validateStep(3, formData);
        if (Object.keys(errs).length > 0) {
            dispatch({ type: "SET_ERRORS", errors: errs });
            return;
        }
        dispatch({ type: "SUBMIT_START" });
        setTimeout(() => {
            console.log("Данные формы:", formData);
            login({ username: formData.username });
        }, 1000);
    };

    const segClass = (n: number) => {
        if (n < currentStep) return "progress-seg done";
        if (n === currentStep) return "progress-seg active";
        return "progress-seg";
    };


    return (
        <div className="wrap">
            <div className="card">
                <div className="card-accent" />
                <div className="progress-track">
                    {[1, 2, 3].map((n) => <div key={n} className={segClass(n)} />)}
                </div>
                {currentStep === 1 && <Step1 formData={formData} errors={errors} dispatch={dispatch} />}
                {currentStep === 2 && <Step2 formData={formData} errors={errors} dispatch={dispatch} />}
                {currentStep === 3 && <Step3 formData={formData} errors={errors} dispatch={dispatch} />}
                <div className="btn-row">
                    {currentStep > 1 && (
                        <button className="btn-back" onClick={handleBack}>← Назад</button>
                    )}
                    {currentStep < 3 ? (
                        <button className="btn-next" onClick={handleNext}>Продолжить →</button>
                    ) : (
                        <button className="btn-next" onClick={handleSubmit} disabled={isSubmitting}>
                            {isSubmitting ? "Отправка..." : "Зарегистрироваться →"}
                        </button>
                    )}
                </div>
            </div>
        </div>
    );
}