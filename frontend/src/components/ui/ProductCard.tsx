import { Link } from "react-router-dom";
import type { Product } from "../../types/product";

interface ProductCardProps {
    product: Product;
}

function ProductCard(props : ProductCardProps) {
    return (
        <div className="product-card">
            <h2>{props.product.name}</h2>
            <p>Price: ${props.product.price}</p>
            <Link to={`/products/${props.product.id}`}>View Details</Link>
        </div>
    )
}
export default ProductCard;