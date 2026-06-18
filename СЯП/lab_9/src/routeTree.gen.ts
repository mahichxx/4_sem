// src/routeTree.gen.ts
import { Route as RootRoute } from "./routes/__root";
import { Route as IndexRoute } from "./routes/index";
import { Route as LoginRoute } from "./routes/login";
import { Route as CatalogRoute } from "./routes/catalog";
import { Route as ProductRoute } from "./routes/product.$id";
import { Route as CartRoute } from "./routes/cart";

export const routeTree = RootRoute.addChildren([
  IndexRoute,
  LoginRoute,
  CatalogRoute,
  ProductRoute,
  CartRoute,
]);
