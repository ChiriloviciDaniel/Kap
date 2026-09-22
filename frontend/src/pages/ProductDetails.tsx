import { useParams } from "react-router-dom";
import type { Product } from "../types/product";
import { useContext, useEffect, useState } from "react";
import { getProduct } from "../services/productService";
import { CartContext } from "../context/CartContext";


function ProductDetails() {
  const { id } = useParams();
  const [product, setProduct] = useState<Product | null>(null);
  //const [cart, setCart] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const cart = useContext(CartContext);

  useEffect(() =>{
    if (id) {
      getProduct(Number(id)) 
        .then(setProduct)
        .catch(() => setError("Failed to load product"))
        .finally(() => setLoading(false));
    }
  }, [id]);
  if (loading){
    return <p>Loading...</p>;
  }

  if (error){
    return <p>{error}</p>;
  }

  if (!product) {
    return <div>Product not found</div>;
  }

  if (!cart) {
    return <div>Cart context not available</div>;
  }

  return (
    <div>
      <h1>Product Name: {product.name}</h1>
      <p>Product Price: ${product.price}</p>
      <p>Product Description: {product.description}</p>
      <p>Product Stock: {product.stockQuantity}</p>
      <button onClick={() => cart.addToCart(product)}>Add to Cart</button>
    </div>
  );
}

export default ProductDetails;