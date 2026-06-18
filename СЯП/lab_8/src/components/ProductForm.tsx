import React, { useState } from "react";
import { ProductSchema, type Product } from "../types";
import { useQueryClient, useMutation } from "@tanstack/react-query";
import { createProduct, updateProduct } from "../api/products";

type Props = {
  initialProduct: Product | null;
  onClose: () => void;
  category?: string;
};

export const ProductForm: React.FC<Props> = ({ initialProduct, onClose, category }) => {
  const isEdit = initialProduct !== null;
  const queryClient = useQueryClient();

  const [id, setId] = useState<number>(initialProduct?.id ?? 0);
  const [title, setTitle] = useState<string>(initialProduct?.title ?? "");
  const [price, setPrice] = useState<number>(initialProduct?.price ?? 0);
  const [errors, setErrors] = useState<string[]>([]);

  const queryKey = ["products", category || null];

  const mutation = useMutation({
    mutationFn: async (p: Product) => {
      return isEdit ? updateProduct(p) : createProduct(p);
    },
    onSuccess: (saved: Product) => {
     
      queryClient.setQueryData<Product[] | undefined>(queryKey, (prev) => {
        if (!prev) return [saved];
        if (isEdit) {
          return prev.map((p) => (p.id === saved.id ? saved : p));
        }
        return [...prev, saved];
      });

      onClose();
    },
    onError: (err: unknown) => {
      if (err instanceof Error) alert(err.message);
      else alert("Ошибка при сохранении товара");
    },
  });

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    setErrors([]);

    const data = { id, title, price };
    const result = ProductSchema.safeParse(data);

    if (!result.success) {
      const flat = result.error.flatten();
      const messages: string[] = [];

      if (flat.fieldErrors.id) messages.push(...flat.fieldErrors.id);
      if (flat.fieldErrors.title) messages.push(...flat.fieldErrors.title);
      if (flat.fieldErrors.price) messages.push(...flat.fieldErrors.price);

      setErrors(messages);
      return;
    }

    mutation.mutate(result.data);
  };

  return (
    <div
      style={{
        marginTop: 16,
        padding: 12,
        border: "1px solid #999",
        borderRadius: 4,
        maxWidth: 400,
      }}
    >
      <h3>{isEdit ? "Изменить товар" : "Добавить товар"}</h3>

      {errors.length > 0 && (
        <div style={{ color: "red", marginBottom: 8 }}>
          {errors.map((e) => (
            <div key={e}>{e}</div>
          ))}
        </div>
      )}

      <form onSubmit={handleSubmit}>
        <div>
          <label>ID (number)</label>
          <input
            type="number"
            value={id}
            onChange={(e) => setId(Number(e.target.value))}
            disabled={isEdit}
          />
        </div>

        <div>
          <label>Название</label>
          <input
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            style={{ width: "100%" }}
          />
        </div>

        <div>
          <label>Цена</label>
          <input
            type="number"
            value={price}
            onChange={(e) => setPrice(Number(e.target.value))}
          />
        </div>

        <div style={{ marginTop: 8 }}>
          <button
            type="submit"
            style={{ marginRight: 8 }}
            disabled={mutation.isPending}
          >
            {isEdit ? "Сохранить" : "Добавить"}
          </button>
          <button type="button" onClick={onClose}>
            Отмена
          </button>
        </div>
      </form>
    </div>
  );
};
