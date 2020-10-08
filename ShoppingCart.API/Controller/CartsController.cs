using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Business.Abstract;
using ShoppingCart.Business.Concrete;
using ShoppingCart.Entities;

namespace ShoppingCart.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private IShoppingCartService _shoppingCartService;
        public CartsController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        /// <summary>
        /// get shopping cart
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCart")]
        public async Task<IActionResult> getCart()
        {
            return Ok(await _shoppingCartService.getCart());
        }
        
        /// <summary>
        /// show the total price of cart
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTotal")]
        public async Task<IActionResult> totalCart()
        {
            return Ok(await _shoppingCartService.totalCart());
        }
        /// <summary>
        /// add product to cart
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddProductToCart/{id}/{quantity}")]
        public async Task<IActionResult> productToCart(int id,int quantity)
        {
            //checking id and add. Dont check quantity because it will check on ShoppingCartRepository.
            if (_shoppingCartService.getProductById(id) != null)
            {
                return Ok(await _shoppingCartService.productToCart(id, quantity));
            }
            return NotFound();
            
        }
        /// <summary>
        /// to delete product in cart, if exists
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("deleteProductInCart/{id}")]
        public async Task deleteProductInCart(int id)
        {
            //dont use Iactionresult because we check shopping cart repository.
            await _shoppingCartService.deleteProductInCart(id);
        }
        /// <summary>
        /// buy the chart NOTE: delete all product in cart and stock
        /// </summary>
        [HttpPost]
        [Route("Buy")]
        public async Task buy()
        {
            await _shoppingCartService.buy();
        }
    }
}