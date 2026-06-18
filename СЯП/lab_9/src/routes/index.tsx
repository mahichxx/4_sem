import React from "react";
import { createRoute } from "@tanstack/react-router";
import { Route as RootRoute } from "./__root";

export const Route = createRoute({
  getParentRoute: () => RootRoute,
  path: "/",
  component: IndexPage,
});

function IndexPage() {
  return (
    <div>
      <h1>Добро пожаловать</h1>
      <a href="/catalog">Перейти в каталог</a>
    </div>
  );
}
