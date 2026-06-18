import React from "react";
import { Link, Outlet, createRootRoute } from "@tanstack/react-router";
import { useAuth } from "../AuthContext";

function RootLayout() {
  const { isAuthenticated, user, logout } = useAuth();

  return (
    <div style={{ fontFamily: "sans-serif", padding: 16 }}>
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
        </div>

        <div>
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
