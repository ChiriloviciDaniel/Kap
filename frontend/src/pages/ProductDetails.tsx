import { useParams } from "react-router-dom";
import type { Product } from "../types/product";


function ProductDetails() {
  const { id } = useParams();
  const product = products.find((p) => p.id === Number(id));

  if (!product) {
    return <div>Product not found</div>;
  }
  return (
    <div>
      <h1>Product Details</h1>
      <p>Product Name: {product.name}</p>
      <p>Product Price: ${product.price}</p>
    </div>
  );
}

export default ProductDetails;