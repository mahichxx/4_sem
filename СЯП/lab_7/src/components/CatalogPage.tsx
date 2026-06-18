import { useEffect, useState } from "react";
import { useAuth } from "../contexts/AuthContext";
import { type IProduct, useProducts } from "../contexts/ProductContext";
import { ProductForm } from "./ProductForm";
import "./Catalog.css";

export function CatalogPage() {
    const { user, logout } = useAuth();
    const { state, fetchProducts, deleteProduct } = useProducts();
    
    const [isFormOpen, setIsFormOpen] = useState(false);
    const [editingProduct, setEditingProduct] = useState<IProduct | undefined>(undefined);

    //
    useEffect(() => {
        fetchProducts();
    }, []);

    const handleAddClick = () => {
        setEditingProduct(undefined);
        setIsFormOpen(true);
    };

    const handleEditClick = (product: IProduct) => {
        setEditingProduct(product);
        setIsFormOpen(true);
    };

    const handleDeleteClick = (id: number) => {
        if (confirm("Вы уверены, что хотите удалить этот товар?")) {
            deleteProduct(id);
        }
    };

    return (
        <div className="catalog-container">
            <header className="catalog-header">
                <div>
                    <h1>Каталог товаров</h1>
                    <p>Добро пожаловать, {user?.username}!</p>
                </div>
                <div className="header-actions">
                    <button className="btn-add" onClick={handleAddClick}>+ Добавить товар</button>
                    <button className="btn-logout" onClick={logout}>Выйти</button>
                </div>
            </header>

            {state.isLoading && <p className="loading-text">Загрузка товаров...</p>}
            {state.error && <p className="error-text">Ошибка: {state.error}</p>}

            {!state.isLoading && !state.error && (
                <div className="product-grid">
                    {state.items.map((product) => (
                        <div key={product.id} className="product-card">
                            {product.thumbnail && (
                                <img src={product.thumbnail} alt={product.title} className="product-image" />
                            )}
                            <div className="product-info">
                                <h3>{product.title}</h3>
                                <p className="product-price">${product.price}</p>
                                {product.description && <p className="product-desc">{product.description}</p>}
                            </div>
                            <div className="product-actions">
                                <button className="btn-edit" onClick={() => handleEditClick(product)}>Изменить</button>
                                <button className="btn-delete" onClick={() => handleDeleteClick(product.id)}>Удалить</button>
                            </div>
                        </div>
                    ))}
                </div>
            )}

            {isFormOpen && (
                <ProductForm 
                    initialData={editingProduct} 
                    onClose={() => setIsFormOpen(false)} 
                />
            )}
        </div>
    );
}
