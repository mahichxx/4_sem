import { atom, selector } from "recoil";
import type { Product } from "./types";

export type ViewMode = "grid" | "list";
export type Theme = "light" | "dark";

type UiSettings = {
  viewMode: ViewMode;
  theme: Theme;
};

type CartItem = {
  id: number;
  quantity: number;
};


const persistAtom =
  <T,>(key: string) =>
  ({ setSelf, onSet }: any) => {
    if (typeof window === "undefined") return;

    const raw = localStorage.getItem(key);
    if (raw) {
      try {
        setSelf(JSON.parse(raw) as T);
      } catch {
        localStorage.removeItem(key);
      }
    }

    onSet((newValue: T, _oldValue: T, isReset: boolean) => {
      if (isReset) {
        localStorage.removeItem(key);
      } else {
        localStorage.setItem(key, JSON.stringify(newValue));
      }
    });
  };


  export const uiSettingsState = atom<UiSettings>({
  key: "uiSettingsState",
  default: {
    viewMode: "grid",
    theme: "light",
  },
  effects: [persistAtom<UiSettings>("lab9_ui_settings")],
});


export const favoritesState = atom<number[]>({
  key: "favoritesState",
  default: [],
  effects: [persistAtom<number[]>("lab9_favorites")],
});


export const cartState = atom<CartItem[]>({
  key: "cartState",
  default: [],
  effects: [persistAtom<CartItem[]>("lab9_cart")],
});


export const productsCacheState = atom<Product[]>({
  key: "productsCacheState",
  default: [],
});


export const cartCountState = selector<number>({
  key: "cartCountState",
  get: ({ get }) => {
    const items = get(cartState);
    return items.reduce((sum, item) => sum + item.quantity, 0);
  },
});


export const cartProductsSelector = selector<
  (Product & { quantity: number })[]
>({
  key: "cartProductsSelector",
  get: ({ get }) => {
    const items = get(cartState);
    const products = get(productsCacheState);

    return items
      .map((ci) => {
        const p = products.find((x) => x.id === ci.id);
        if (!p) return null;
        return { ...p, quantity: ci.quantity };
      })
      .filter((x): x is Product & { quantity: number } => x !== null);
  },
});
