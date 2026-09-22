import type { Product } from "../types/product";
import { createContext, useState } from "react";

interface CartContextType {
    cartItems: Product[];
    addToCart: (product: Product) => void;
    removeFromCart: (product: Product) => void;
}

const CartContext = createContext<CartContextType | undefined>(undefined);

function CartProvider({ children }: { children: React.ReactNode }) {

  const [cartItems, setCartItems] = useState<Product[]>([]);

  const addToCart = (product: Product) => {
    console.log("Added to cart: ",product);
    setCartItems((currentCart ) => [...currentCart, product]); 

    //Spread Operator
    //[...cartItems, product]
    console.log("Current cart:", cartItems);
    
  };

  const removeFromCart = (product: Product) => {
    setCartItems((currentCart) => currentCart.filter((item) => item.id !== product.id));
  };

  return (
    <CartContext.Provider value={{ cartItems, addToCart, removeFromCart }}>
      {children}
    </CartContext.Provider>
  );
}
export { CartContext, CartProvider };
