import type { Product } from "../types/product";

export async function productsAPI(){
const response = await fetch('http://localhost:5033/api/products');
if (!response.ok) {
    throw new Error('Failed to fetch products');
  }
  const data: Product[] = await response.json();
  
  return data;
}