import React from "react";
import { createRoute, redirect, useRouter } from "@tanstack/react-router";
import { Route as RootRoute } from "./__root";
import { useQuery } from "@tanstack/react-query";
import { fetchProducts } from "../api/products";
import { useAuth } from "../AuthContext";
import { ProductGrid } from "../components/ProductGrid";
import { ProductForm } from "../components/ProductForm";
import type { Product } from "../types";
import { z } from "zod";

const SearchSchema = z.object({
  category: z.string().optional(),
});

export const Route = createRoute({
  getParentRoute: () => RootRoute,
  path: "catalog",
  validateSearch: (search) => SearchSchema.parse(search),
  
  beforeLoad: () => {
    const raw = localStorage.getItem("lab8_user");
    if (!raw) {
      throw redirect({ to: "/login" });
    }
    return {};
  },
  component: CatalogPage,
});

const CATEGORY_OPTIONS: { label: string; value: string }[] = [
  { label: "Все категории", value: "" },
  { label: "Смартфоны", value: "smartphones" },
  { label: "Ноутбуки", value: "laptops" },
  { label: "Платья (женские)", value: "womens-dresses" },
  { label: "Обувь (женская)", value: "womens-shoes" },
  { label: "Обувь (мужская)", value: "mens-shoes" },
  { label: "Футболки (мужские)", value: "mens-shirts" },
  { label: "Украшения", value: "womens-jewellery" },
  { label: "Продукты", value: "groceries" },
];

function CatalogPage() {
  const { user } = useAuth();
  const router = useRouter();

  const search = Route.useSearch();
  const category = search.category ?? "";

  const [editingProduct, setEditingProduct] = React.useState<Product | null>(null);
  const [isFormOpen, setIsFormOpen] = React.useState(false);
  const [selectedCategory, setSelectedCategory] = React.useState(category);

  const { data, isLoading, isError, error } = useQuery({
    queryKey: ["products", category || null],
    queryFn: () => fetchProducts(category || undefined),
    staleTime: 60000,
    gcTime: 300000,
  });

  const handleCategoryChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const value = e.target.value;
    setSelectedCategory(value);
    router.navigate({
      to: "/catalog",
      search: {
        category: value || undefined,
      },
    });
  };

  return (
    <div>
      <header
        style={{
          display: "flex",
          justifyContent: "space-between",
          marginBottom: 16,
          gap: 16,
          alignItems: "center",
        }}
      >
        <div>
          <h2>Каталог товаров</h2>
          <div>Пользователь: {user?.name}</div>
        </div>

        <div style={{ display: "flex", gap: 8, alignItems: "center" }}>
          <label>
            Категория:{" "}
            <select
              value={selectedCategory}
              onChange={handleCategoryChange}
              style={{ minWidth: 260 }}
            >
              {CATEGORY_OPTIONS.map((opt) => (
                <option key={opt.value} value={opt.value}>
                  {opt.label}
                </option>
              ))}
            </select>
          </label>
        </div>

        <button onClick={() => setIsFormOpen(true)}>Добавить товар</button>
      </header>

      {isLoading && <p>Загрузка...</p>}
      {isError && (
        <p style={{ color: "red" }}>
          {error instanceof Error ? error.message : "Ошибка загрузки"}
        </p>
      )}

      {data && (
        <ProductGrid
          products={data}
          onEdit={(p) => {
            setEditingProduct(p);
            setIsFormOpen(true);
          }}
        />
      )}

      {isFormOpen && (
        <ProductForm
          initialProduct={editingProduct}
          category={category || undefined}
          onClose={() => {
            setEditingProduct(null);
            setIsFormOpen(false);
          }}
        />
      )}
    </div>
  );
}
