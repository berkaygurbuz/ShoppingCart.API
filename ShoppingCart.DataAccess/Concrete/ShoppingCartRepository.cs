using Microsoft.EntityFrameworkCore;
using ShoppingCart.DataAccess.Abstract;
using ShoppingCart.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DataAccess.Concrete
{
    //implement IShoppingCartRepository
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        public async Task<Products> createProduct(Products products)
        {
            using (var shoppingCartDbContext = new ShoppingCartDbContext())
            {
                shoppingCartDbContext.Products.Add(products);
                await shoppingCartDbContext.SaveChangesAsync();
                return products;
            }
        }

        public async Task deleteProduct(int id)
        {
            using (var shoppingCartDbContext = new ShoppingCartDbContext())
            {
                var deletedProduct = await getProductById(id);
                shoppingCartDbContext.Products.Remove(deletedProduct);
                await shoppingCartDbContext.SaveChangesAsync();
                
            }
        }
        
        public async Task<List<Cart>> getCart()
        {
            using (var shoppingCartDbContext = new ShoppingCartDbContext())
            {
                
                return await shoppingCartDbContext.Carts.ToListAsync();
            }
        }

        public async Task<List<Products>> getAllProducts()
        {
            using (var shoppingCartDbContext = new ShoppingCartDbContext())
            {
                return await shoppingCartDbContext.Products.ToListAsync();
            }
        }

        
        public async Task<Products> getProductById(int id)
        {
            using (var shoppingCartDbContext = new ShoppingCartDbContext())
            {
                return await shoppingCartDbContext.Products.FindAsync(id);
            }
        }

        public async Task<Products> updateProduct(Products products)
        {
            using (var shoppingCartDbContext = new ShoppingCartDbContext())
            {
                shoppingCartDbContext.Products.Update(products);
                await shoppingCartDbContext.SaveChangesAsync();
                return products;
            }
        }

        public async Task<decimal> totalCart()
        {
            using (var shoppingCartDbContext = new ShoppingCartDbContext())
            {
                decimal total = 0;
                total= await shoppingCartDbContext.Carts.SumAsync(x => x.price*x.piece);
                return total;
            }
        }

        public async Task<List<Cart>> productToCart(int id,int quantity)
        {
            using(var shoppingCartDbContext=new ShoppingCartDbContext())
            {
                var x = await shoppingCartDbContext.Products.FirstOrDefaultAsync(x => x.productId == id);
                if (x.stock > quantity)
                {
                    //if product added before increase the piece
                    if (shoppingCartDbContext.Carts.Any(y => y.productId == x.productId))
                    {
                        var prdctCart = shoppingCartDbContext.Carts.FirstOrDefault(y => y.productId == x.productId);

                        if (quantity == null || quantity <= 0)
                        {
                            quantity = 1;

                        }
                        prdctCart.piece += quantity;
                    }
                    else
                    {
                        Cart cart = new Cart
                        {
                            productId = x.productId,
                            name = x.name,
                            price = x.price,
                            piece = quantity,
                            totalPrice = quantity * x.price

                        };
                        shoppingCartDbContext.Carts.Add(cart);
                    }
                }

                await shoppingCartDbContext.SaveChangesAsync();
                return await getCart();

            }
            
        }

        //take the product id
        public async Task deleteProductInCart(int id)
        {
            using (var shoppingCartDbContext = new ShoppingCartDbContext())
            {
                var x = shoppingCartDbContext.Products.Find(id);
                var cartId = shoppingCartDbContext.Carts.First(y => y.productId == x.productId);
                //if product added before,delete it
                if (shoppingCartDbContext.Carts.Any(y => y.productId == x.productId))
                {
                    shoppingCartDbContext.Carts.Remove(cartId);
                }
                //if product did not add before,do nothing

               await shoppingCartDbContext.SaveChangesAsync();
                
                
            }
        }

        public async Task buy()
        {
            using(var shoppingCartDbContext=new ShoppingCartDbContext())
            {

                
                var cart = shoppingCartDbContext.Carts;
                var productTable = shoppingCartDbContext.Products;
                //if cart is not empty, delete the bought items from stock.
                if (cart != null)
                {
                    foreach (var x in cart)
                    {

                        int id = x.productId;
                        var deletedCartProduct = cart.First(z => z.productId == id);
                        var y = productTable.Find(id);
                        y.stock -= x.piece;
                        cart.Remove(deletedCartProduct);

                    }
                }
               await shoppingCartDbContext.SaveChangesAsync();
            }
        }
    }
}
