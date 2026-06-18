import { ProductSchema, ProductsResponseSchema, type Product } from "../types";

const API_URL = "https://dummyjson.com/products";

export async function fetchProducts(category?: string): Promise<Product[]> {
  const url = category
    ? `${API_URL}/category/${encodeURIComponent(category)}`
    : `${API_URL}?limit=12`;

  const res = await fetch(url);
  if (!res.ok) throw new Error("Ошибка загрузки товаров");

  const data = await res.json();
  const parsed = ProductsResponseSchema.safeParse(data);

  if (!parsed.success) throw new Error("Ошибка структуры данных");

  return parsed.data.products;
}

export async function fetchProductById(id: number): Promise<Product> {
  const res = await fetch(`${API_URL}/${id}`);
  if (!res.ok) throw new Error("Товар не найден");

  const data = await res.json();
  const parsed = ProductSchema.safeParse(data);

  if (!parsed.success) throw new Error("Ошибка структуры данных");

  return parsed.data;
}

export async function createProduct(p: Product): Promise<Product> {
  const res = await fetch(`${API_URL}/add`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(p),
  });

  if (!res.ok) throw new Error("Ошибка добавления товара");

  const data = await res.json();
  const merged = {
    id: data.id ?? p.id,
    title: data.title ?? p.title,
    price: data.price ?? p.price,
  };

  const parsed = ProductSchema.safeParse(merged);
  if (!parsed.success) throw new Error("Ошибка структуры данных");

  return parsed.data;
}

export async function updateProduct(p: Product): Promise<Product> {
  const res = await fetch(`${API_URL}/${p.id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ title: p.title, price: p.price }),
  });

  if (!res.ok) throw new Error("Ошибка обновления товара");

  const data = await res.json();
  const merged = {
    id: data.id ?? p.id,
    title: data.title ?? p.title,
    price: data.price ?? p.price,
  };

  const parsed = ProductSchema.safeParse(merged);
  if (!parsed.success) throw new Error("Ошибка структуры данных");

  return parsed.data;
}

export async function deleteProduct(id: number): Promise<void> {
  const res = await fetch(`${API_URL}/${id}`, {
    method: "DELETE",
  });

  if (!res.ok) throw new Error("Ошибка удаления товара");
}
