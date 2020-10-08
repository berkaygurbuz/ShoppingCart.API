using ShoppingCart.Business.Abstract;
using ShoppingCart.DataAccess.Abstract;
using ShoppingCart.DataAccess.Concrete;
using ShoppingCart.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Business.Concrete
{

    public class ShoppingCartManager : IShoppingCartService
    {
        private IShoppingCartRepository _shoppingCartRepository;
        public ShoppingCartManager(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task buy()
        {
             await _shoppingCartRepository.buy();
        }

        public async Task<Products> createProduct(Products products)
        {
            return await _shoppingCartRepository.createProduct(products);
        }

        public async Task deleteProduct(int id)
        {
           await _shoppingCartRepository.deleteProduct(id);
        }

        public async Task deleteProductInCart(int id)
        {
           await _shoppingCartRepository.deleteProductInCart(id);
        }

        public async Task<List<Products>> getAllProducts()
        {
            return await _shoppingCartRepository.getAllProducts();
        }

        public async Task<List<Cart>> getCart()
        {
            
            return await _shoppingCartRepository.getCart();

        }

        public async Task<Products> getProductById(int id)
        {
            if (id > 0)
            {
                return await _shoppingCartRepository.getProductById(id);
            }
            throw new Exception("id can not be less than 1");
        }

        public async Task<List<Cart>> productToCart(int id,int quantity)
        {
            
            return await _shoppingCartRepository.productToCart(id,quantity);
        }

        public async Task<decimal> totalCart()
        {
            return await _shoppingCartRepository.totalCart();
        }

        public async Task<Products> updateProduct(Products products)
        {
            return await _shoppingCartRepository.updateProduct(products);
        }
    }
}
