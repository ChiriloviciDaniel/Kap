import { useEffect, useState } from "react";
import { productsAPI } from "../api/productsAPI";
import type { Product } from "../types/product";
import ProductCard from "../components/ui/ProductCard";

function Home() {

  const [products, setProducts] = useState<Product[]>([]);

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const products = await productsAPI();
        setProducts(products);
        console.log("done fetching products");
      
      } catch (error) {
        console.error("Error fetching products:", error);
      }
    };
  
   fetchProducts();
  }, []);
  
  return (
    <div>
      <h1>Welcome to Kap</h1>
      <h2>Products</h2>
      <div className="product-list">
        {products.map((product) => (
          <ProductCard key={product.id} product={product} />
        ))}
      </div>
    </div>
  );
}

export default Home;