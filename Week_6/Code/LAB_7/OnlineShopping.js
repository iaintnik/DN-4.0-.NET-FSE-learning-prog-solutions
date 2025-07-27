import React from 'react';
import Cart from './Cart';

class OnlineShopping extends React.Component {
  render() {
    const cartItems = [
      { itemname: 'Mobile', price: 15000 },
      { itemname: 'Laptop', price: 50000 },
      { itemname: 'Headphones', price: 2000 },
      { itemname: 'Watch', price: 3000 },
      { itemname: 'Tablet', price: 18000 }
    ];

    return (
      <div>
        <h1>Online Shopping Cart</h1>
        {cartItems.map((item, index) => (
          <Cart key={index} itemname={item.itemname} price={item.price} />
        ))}
      </div>
    );
  }
}

export default OnlineShopping;
