import { useState, useEffect } from "react";
import { ProductSchema, type IProduct, useProducts } from "../contexts/ProductContext";

interface ProductFormProps {
    initialData?: IProduct;
    onClose: () => void;
}

export function ProductForm({ initialData, onClose }: ProductFormProps) {
    const { addProduct, updateProduct } = useProducts();
    const [title, setTitle] = useState(initialData?.title || "");
    const [price, setPrice] = useState(initialData?.price?.toString() || "");
    const [description, setDescription] = useState(initialData?.description || "");
    const [errors, setErrors] = useState<Record<string, string[]>>({});
    const [isSubmitting, setIsSubmitting] = useState(false);

    useEffect(() => {
        if (initialData) {
            setTitle(initialData.title);
            setPrice(initialData.price.toString());
            setDescription(initialData.description || "");
            setErrors({});
        }
    }, [initialData]);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        const rawData = {
            id: initialData?.id || Date.now(),
            title,
            price: parseFloat(price),
            description
        };

        const result = ProductSchema.safeParse(rawData);

        if (!result.success) {
            const flatErrors = result.error.flatten().fieldErrors;
            setErrors(flatErrors as Record<string, string[]>);
            return;
        }

        setErrors({});
        setIsSubmitting(true);

        if (initialData) {
            await updateProduct(result.data);
        } else {
            await addProduct({
                title: result.data.title,
                price: result.data.price,
                description: result.data.description
            });
        }
        setIsSubmitting(false);
        onClose();
    };

    return (
        <div className="modal-overlay">
            <div className="product-form-card">
                <h2>{initialData ? "Изменить товар" : "Добавить новый товар"}</h2>
                <form onSubmit={handleSubmit} className="product-form">
                    <div className="form-group">
                        <label>Название товара</label>
                        <input
                            type="text"
                            value={title}
                            onChange={(e) => setTitle(e.target.value)}
                            className={errors.title ? "input-error" : ""}
                        />
                        {errors.title && <span className="error-text">{errors.title[0]}</span>}
                    </div>

                    <div className="form-group">
                        <label>Цена (в $)</label>
                        <input
                            type="number"
                            step="0.01"
                            value={price}
                            onChange={(e) => setPrice(e.target.value)}
                            className={errors.price ? "input-error" : ""}
                        />
                        {errors.price && <span className="error-text">{errors.price[0]}</span>}
                    </div>

                    <div className="form-group">
                        <label>Описание</label>
                        <textarea
                            value={description}
                            onChange={(e) => setDescription(e.target.value)}
                            rows={3}
                        />
                    </div>

                    <div className="form-action-buttons">
                        <button type="button" onClick={onClose} className="btn-cancel">Отмена</button>
                        <button type="submit" disabled={isSubmitting} className="btn-submit">
                            {isSubmitting ? "Отправка..." : "Сохранить"}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
}
