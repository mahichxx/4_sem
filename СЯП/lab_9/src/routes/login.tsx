import React from "react";
import { createRoute, useRouter } from "@tanstack/react-router";
import { Route as RootRoute } from "./__root";
import { AuthForm } from "../components/AuthForm";

export const Route = createRoute({
  getParentRoute: () => RootRoute,
  path: "login",
  component: LoginPage,
});

function LoginPage() {
  const router = useRouter();

  return (
    <div>
      <h2>Авторизация</h2>
      <AuthForm onSuccess={() => router.navigate({ to: "/catalog" as any })} />
    </div>
  );
}
