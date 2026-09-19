import type { Product } from "../types/product";

const API_URL = "http://localhost:5033/api";

export async function getProducts(): Promise<Product[]> {
  const response = await fetch(`${API_URL}/Products`);

  if (!response.ok) {
    throw new Error("Failed to fetch products");
  }

  return response.json();
}

export async function getProduct(id: number): Promise<Product> {
  const response = await fetch(`${API_URL}/Products/${id}`);

  if (!response.ok) {
    throw new Error("Failed to fetch product");
  }

  return response.json();
}