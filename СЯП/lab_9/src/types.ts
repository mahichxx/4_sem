import { z } from "zod";

export const ProductSchema = z.object({
  id: z.number(),
  title: z.string().min(3, "title: Название слишком короткое"),
  price: z.number().positive("price: Цена должна быть больше 0"),
});

export const ProductsResponseSchema = z.object({
  products: z.array(ProductSchema),
});

export type Product = z.infer<typeof ProductSchema>;
