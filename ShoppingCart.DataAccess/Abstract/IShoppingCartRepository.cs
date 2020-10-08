using ShoppingCart.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DataAccess.Abstract
{
    //We keep interface on Abstract files.
    public interface IShoppingCartRepository
    {

        //Product interfaces
        Task<List<Products>> getAllProducts();

        Task<Products> getProductById(int id);

        Task<Products> createProduct(Products products);

        Task<Products> updateProduct(Products products);

        Task deleteProduct(int id);


        //Cart Interfaces
        Task<List<Cart>> getCart();
        Task<decimal> totalCart();
        Task<List<Cart>> productToCart(int id,int quantity);

        Task deleteProductInCart(int id);
        Task buy();
    }
}
