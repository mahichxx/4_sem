import React from "react";
import { createRoute } from "@tanstack/react-router";
import { Route as RootRoute } from "./__root";
import {
  cartState,
  cartProductsSelector,
  cartCountState,
} from "../state";
import {
  useRecoilState,
  useRecoilValue,
  useSetRecoilState,
} from "recoil";

export const Route = createRoute({
  getParentRoute: () => RootRoute,
  path: "cart",
  component: CartPage,
});

function CartPage() {
  const [cart, setCart] = useRecoilState(cartState);
  const products = useRecoilValue(cartProductsSelector);
  const count = useRecoilValue(cartCountState);
  const setCartOnly = useSetRecoilState(cartState);

  const inc = (id: number) => {
    setCart((prev) =>
      prev.map((x) =>
        x.id === id ? { ...x, quantity: x.quantity + 1 } : x
      )
    );
  };

  const dec = (id: number) => {
    setCart((prev) =>
      prev
        .map((x) =>
          x.id === id ? { ...x, quantity: x.quantity - 1 } : x
        )
        .filter((x) => x.quantity > 0)
    );
  };

  const remove = (id: number) => {
    setCart((prev) => prev.filter((x) => x.id !== id));
  };

  const clearAll = () => {
    setCartOnly([]);
  };

  return (
    <div>
      <h2>Корзина</h2>
      <p>Всего товаров: {count}</p>
      <button onClick={clearAll} disabled={cart.length === 0}>
        Очистить корзину
      </button>

      {products.length === 0 ? (
        <p style={{ marginTop: 16 }}>Корзина пуста</p>
      ) : (
        <div style={{ marginTop: 16, display: "flex", flexDirection: "column", gap: 8 }}>
          {products.map((p) => (
            <div
              key={p.id}
              style={{
                border: "1px solid #ccc",
                borderRadius: 4,
                padding: 8,
                display: "flex",
                justifyContent: "space-between",
                alignItems: "center",
              }}
            >
              <div>
                <div>{p.title}</div>
                <div>Цена: {p.price}</div>
              </div>
              <div style={{ display: "flex", gap: 8, alignItems: "center" }}>
                <button onClick={() => dec(p.id)}>-</button>
                <span>{p.quantity}</span>
                <button onClick={() => inc(p.id)}>+</button>
                <button onClick={() => remove(p.id)}>Удалить</button>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}
