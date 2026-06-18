import React from "react";
import type { Product } from "../types";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deleteProduct } from "../api/products";
import { Link } from "@tanstack/react-router";

type Props = {
  products: Product[];
  onEdit: (p: Product) => void;
};

export const ProductGrid: React.FC<Props> = ({ products, onEdit }) => {
  const queryClient = useQueryClient();

  const deleteMutation = useMutation({
    mutationFn: async (id: number) => {
      await deleteProduct(id);
    },
    onMutate: async (id: number) => {
      await queryClient.cancelQueries({ queryKey: ["products"] });

      const prev = queryClient.getQueryData<Product[]>(["products"]);

      if (prev) {
        queryClient.setQueryData<Product[]>(
          ["products"],
          prev.filter((p) => p.id !== id)
        );
      }

      return { prev };
    },
    onError: (_err: unknown, _id: number, context: any) => {
      if (context?.prev) {
        queryClient.setQueryData(["products"], context.prev);
      }
      alert("Ошибка удаления товара");
    },
    onSettled: () => {
      queryClient.invalidateQueries({ queryKey: ["products"] });
    },
  });

  const handleDelete = (id: number) => {
    if (!confirm("Удалить товар?")) return;
    deleteMutation.mutate(id);
  };

  return (
    <div
      style={{
        display: "grid",
        gridTemplateColumns: "repeat(auto-fill, minmax(200px, 1fr))",
        gap: 12,
      }}
    >
      {products.map((p) => (
        <div
          key={p.id}
          style={{
            border: "1px solid #ccc",
            borderRadius: 4,
            padding: 8,
          }}
        >
          <h4>{p.title}</h4>
          <p>Цена: {p.price}</p>

          <div style={{ marginBottom: 8 }}>
            <Link to={`/product/${p.id}` as any}>Подробнее</Link>
          </div>

          <button onClick={() => onEdit(p)} style={{ marginRight: 8 }}>
            Изменить
          </button>
          <button onClick={() => handleDelete(p.id)}>Удалить</button>
        </div>
      ))}
    </div>
  );
};
