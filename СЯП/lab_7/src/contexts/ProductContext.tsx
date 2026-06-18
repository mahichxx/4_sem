import { createContext, useContext, useReducer, type ReactNode } from "react";
import { z } from "zod";

export const ProductSchema = z.object({
    id: z.number(),
    title: z.string().min(3, "Название слишком короткое (минимум 3 символа)"),
    price: z.number().positive("Цена должна быть больше нуля"),
    description: z.string().optional(),
    thumbnail: z.string().optional(),
});

export type IProduct = z.infer<typeof ProductSchema>;

interface ProductState {
    items: IProduct[];
    isLoading: boolean;
    error: string | null;
}

type ProductAction =
    | { type: "FETCH_START" }
    | { type: "FETCH_SUCCESS"; payload: IProduct[] }
    | { type: "FETCH_ERROR"; payload: string }
    | { type: "ADD_PRODUCT"; payload: IProduct }
    | { type: "UPDATE_PRODUCT"; payload: IProduct }
    | { type: "DELETE_PRODUCT"; payload: number };

const initialState: ProductState = {
    items: [],
    isLoading: false,
    error: null,
};

function productReducer(state: ProductState, action: ProductAction): ProductState {
    switch (action.type) {
        case "FETCH_START":
            return { ...state, isLoading: true, error: null };
        case "FETCH_SUCCESS":
            return { ...state, items: action.payload, isLoading: false };
        case "FETCH_ERROR":
            return { ...state, error: action.payload, isLoading: false };
        case "ADD_PRODUCT":
            return { ...state, items: [action.payload, ...state.items] };
        case "UPDATE_PRODUCT":
            return {
                ...state,
                items: state.items.map((item) =>
                    item.id === action.payload.id ? action.payload : item
                ),
            };
        case "DELETE_PRODUCT":
            return {
                ...state,
                items: state.items.filter((item) => item.id !== action.payload),
            };
        default:
            return state;
    }
}

interface ProductContextType {
    state: ProductState;
    fetchProducts: () => Promise<void>;
    addProduct: (productInfo: Omit<IProduct, "id">) => Promise<void>;
    updateProduct: (product: IProduct) => Promise<void>;
    deleteProduct: (id: number) => Promise<void>;
}

const ProductContext = createContext<ProductContextType | undefined>(undefined);

export function ProductProvider({ children }: { children: ReactNode }) {
    const [state, dispatch] = useReducer(productReducer, initialState);

    const fetchProducts = async () => {
        dispatch({ type: "FETCH_START" });
        try {
            const response = await fetch("https://dummyjson.com/products?limit=10");
            if (!response.ok) throw new Error("Ошибка при загрузке продуктов с сервера");
            const data = await response.json();
            
            const products = data.products.map((p: any) => ({
                id: p.id,
                title: p.title,
                price: p.price,
                description: `Это качественный товар: ${p.title}. Прекрасный выбор для повседневного использования.`,
                thumbnail: p.thumbnail,
            }));
            dispatch({ type: "FETCH_SUCCESS", payload: products });
        } catch (error: any) {
            dispatch({ type: "FETCH_ERROR", payload: error.message || "Неизвестная ошибка загрузки" });
        }
    };

    const addProduct = async (productInfo: Omit<IProduct, "id">) => {
        try {
            const response = await fetch('https://dummyjson.com/products/add', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(productInfo)
            });
            if (!response.ok) throw new Error("Не удалось добавить товар на сервере");
            const data = await response.json();
            dispatch({ type: "ADD_PRODUCT", payload: { ...productInfo, id: data.id } });
        } catch (error) {
            console.error(error);
            alert("Не удалось добавить товар.");
        }
    };

    const updateProduct = async (product: IProduct) => {
        try {
            const response = await fetch(`https://dummyjson.com/products/${product.id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(product)
            });
            if (!response.ok) throw new Error("Не удалось обновить товар на сервере");
            
            dispatch({ type: "UPDATE_PRODUCT", payload: product });
        } catch (error) {
            console.error(error);
            alert("Не удалось обновить товар.");
        }
    };

    const deleteProduct = async (id: number) => {
        try {
            const response = await fetch(`https://dummyjson.com/products/${id}`, {
                method: 'DELETE',
            });
            if (!response.ok && response.status !== 404) {
                throw new Error("Не удалось удалить товар на сервере");
            }
            
            dispatch({ type: "DELETE_PRODUCT", payload: id });
        } catch (error) {
            console.error(error);
            alert("Произошла ошибка при удалении товара.");
        }
    };

    return (
        <ProductContext.Provider value={{ state, fetchProducts, addProduct, updateProduct, deleteProduct }}>
            {children}
        </ProductContext.Provider>
    );
}

export function useProducts() {
    const context = useContext(ProductContext);
    if (!context) {
        throw new Error("Использование продуктов должно осуществляться внутри поставщика продуктов (ProductProvider).");
    }
    return context;
}
