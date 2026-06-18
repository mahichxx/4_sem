import React from "react";
import type { Product } from "../types";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deleteProduct } from "../api/products";
import { Link } from "@tanstack/react-router";
import {
  favoritesState,
  cartState,
  uiSettingsState,
  type ViewMode,
} from "../state";
import {
  useRecoilValue,
  useSetRecoilState,
} from "recoil";

type Props = {
  products: Product[];
  onEdit: (p: Product) => void;
};

export const ProductGrid: React.FC<Props> = ({ products, onEdit }) => {
  const queryClient = useQueryClient();

  const favorites = useRecoilValue(favoritesState);
  const setFavorites = useSetRecoilState(favoritesState);
  const setCart = useSetRecoilState(cartState);
  const ui = useRecoilValue(uiSettingsState);

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

  const toggleFavorite = (id: number) => {
    setFavorites((prev) =>
      prev.includes(id) ? prev.filter((x) => x !== id) : [...prev, id]
    );
  };

  const addToCart = (id: number) => {
    setCart((prev) => {
      const existing = prev.find((x) => x.id === id);
      if (!existing) return [...prev, { id, quantity: 1 }];
      return prev.map((x) =>
        x.id === id ? { ...x, quantity: x.quantity + 1 } : x
      );
    });
  };

  const isGrid: boolean = ui.viewMode === "grid";

  return (
    <div
      style={
        isGrid
          ? {
              display: "grid",
              gridTemplateColumns: "repeat(auto-fill, minmax(200px, 1fr))",
              gap: 12,
            }
          : {
              display: "flex",
              flexDirection: "column",
              gap: 12,
            }
      }
    >
      {products.map((p) => {
        const isFav = favorites.includes(p.id);
        return (
          <div
            key={p.id}
            style={{
              border: "1px solid #ccc",
              borderRadius: 4,
              padding: 8,
              display: "flex",
              flexDirection: "column",
              gap: 4,
            }}
          >
            <h4>{p.title}</h4>
            <p>Цена: {p.price}</p>
            <div style={{ marginBottom: 8 }}>
              <Link to={`/product/${p.id}` as any}>Подробнее</Link>
            </div>
            <div style={{ display: "flex", gap: 8, marginBottom: 8 }}>
              <button onClick={() => toggleFavorite(p.id)}>
                {isFav ? "★ В избранном" : "☆ В избранное"}
              </button>
              <button onClick={() => addToCart(p.id)}>В корзину</button>
            </div>
            <div>
              <button onClick={() => onEdit(p)} style={{ marginRight: 8 }}>
                Изменить
              </button>
              <button onClick={() => handleDelete(p.id)}>Удалить</button>
            </div>
          </div>
        );
      })}
    </div>
  );
};
