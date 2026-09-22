import { useContext } from "react";
import { CartContext } from "../context/CartContext";

function Cart() {
    const cart = useContext(CartContext);

    console.log("Cart items:", cart?.cartItems);

    if (!cart) {
        return <p>Cart is empty</p>;
    }

    return (
        <div>
            <h1>Cart</h1>

            {cart.cartItems.map((product) => (
                <div key={product.id}>
                    <h2>{product.name}</h2>
                    <h2>{product.price} lei</h2>
                </div>
            ))}
        </div>
    );
}export default Cart;