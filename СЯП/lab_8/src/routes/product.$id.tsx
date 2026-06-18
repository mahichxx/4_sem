import React from "react";
import { createRoute } from "@tanstack/react-router";
import { Route as RootRoute } from "./__root";
import { useQuery } from "@tanstack/react-query";
import { fetchProductById } from "../api/products";

export const Route = createRoute({
  getParentRoute: () => RootRoute,
  path: "product/$id",
  component: ProductDetailsPage,
});

function ProductDetailsPage() {
  const { id } = Route.useParams();
  const productId = Number(id);

  const { data, isLoading, isError, error } = useQuery({
    queryKey: ["product", productId],
    queryFn: () => fetchProductById(productId),
  });

  if (isLoading) return <p>Загрузка...</p>;
  if (isError)
    return (
      <p style={{ color: "red" }}>
        {error instanceof Error ? error.message : "Ошибка"}
      </p>
    );

  if (!data) return <p>Товар не найден</p>;

  return (
    <div>
      <h2>{data.title}</h2>
      <p>Цена: {data.price}</p>
    </div>
  );
}
