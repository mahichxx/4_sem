import React from "react";
import { Link, Outlet, createRootRoute } from "@tanstack/react-router";
import { useAuth } from "../AuthContext";
import {
  cartCountState,
  uiSettingsState,
  cartState,
  favoritesState,
} from "../state";
import { useRecoilValue, useRecoilCallback } from "recoil";

function RootLayout() {
  const { isAuthenticated, user, logout } = useAuth();
  const cartCount = useRecoilValue(cartCountState);
  const ui = useRecoilValue(uiSettingsState);

  const resetAll = useRecoilCallback(
    ({ reset }) =>
      () => {
        reset(uiSettingsState);
        reset(cartState);
        reset(favoritesState);
      },
    []
  );

  const bg = ui.theme === "dark" ? "#222" : "#fff";
  const color = ui.theme === "dark" ? "#f5f5f5" : "#000";

  return (
    <div
      style={{
        fontFamily: "sans-serif",
        padding: 16,
        backgroundColor: bg,
        color,
        minHeight: "100vh",
      }}
    >
      <nav
        style={{
          display: "flex",
          justifyContent: "space-between",
          marginBottom: 16,
          borderBottom: "1px solid #ccc",
          paddingBottom: 8,
        }}
      >
        <div style={{ display: "flex", gap: 8 }}>
          <Link to={"/" as any}>Главная</Link>
          <Link to={"/catalog" as any}>Каталог</Link>
          <Link to={"/cart" as any} style={{ position: "relative" }}>
            Корзина
            {cartCount > 0 && (
              <span
                style={{
                  position: "absolute",
                  top: -8,
                  right: -12,
                  background: "red",
                  color: "white",
                  borderRadius: "50%",
                  padding: "2px 6px",
                  fontSize: 12,
                }}
              >
                {cartCount}
              </span>
            )}
          </Link>
        </div>
        <div style={{ display: "flex", gap: 8, alignItems: "center" }}>
          <button onClick={resetAll}>Сбросить настройки</button>
          {isAuthenticated ? (
            <>
              <span style={{ marginRight: 8 }}>Пользователь: {user?.name}</span>
              <button onClick={logout}>Выйти</button>
            </>
          ) : (
            <Link to={"/login" as any}>Войти</Link>
          )}
        </div>
      </nav>
      <Outlet />
    </div>
  );
}

export const Route = createRootRoute({
  component: RootLayout,
});
