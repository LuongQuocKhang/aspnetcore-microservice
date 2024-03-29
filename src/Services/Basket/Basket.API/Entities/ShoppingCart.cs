﻿namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            
        }

        public ShoppingCart(string userName)
        {
            this.UserName = userName;
        }

        public string UserName { get; set; }

        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                foreach (ShoppingCartItem item in Items)
                {
                    totalprice += item.Price * item.Quantity;
                }

                return totalprice;
            }
        }
    }
}
