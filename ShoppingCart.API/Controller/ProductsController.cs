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
    public class ProductsController : ControllerBase
    {
        private IShoppingCartService _shoppingCartService;

        public ProductsController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        /// <summary>
        /// get all products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllProduct")]
        public async Task<IActionResult> get()
        {
            var products = await _shoppingCartService.getAllProducts();
            return  Ok(products);   //return 200 + data
        }
        /// <summary>
        /// get products by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> getById(int id)
        {
            var product = await _shoppingCartService.getProductById(id);
            if (product != null)
            {
                return Ok(product); //return 200 + data
            }
            else
            {
                return NotFound();  //404 not found
            }
            
        }

        /// <summary>
        /// creating product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createProduct")]
        public async Task<IActionResult> createPrdct([FromBody]Products product)
        {

            //we dont need modelstate.isvalid query because [ApiController] does for us.
            var createdProduct =await _shoppingCartService.createProduct(product);
            return CreatedAtAction("getById", new { id = createdProduct.productId }, createdProduct);   //return 201 + data
        }


        /// <summary>
        /// update product
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateProduct")]
        public async Task<IActionResult> updatePrdct([FromBody]Products products)
        {
            //check product is exists
            if (_shoppingCartService.getProductById(products.productId) != null)
            {
                return Ok(await _shoppingCartService.updateProduct(products));    //return 200 + data
            }
            return NotFound();      //return 404
        }
        /// <summary>
        /// delete product by id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("deleteProduct/{id}")]
        public async Task<IActionResult> deletePrdct(int id)
        {

            //check product is exits
            if (_shoppingCartService.getProductById(id) != null)
            {
                await _shoppingCartService.deleteProduct(id);
               return Ok();
            }
            else
            {
                return NotFound();
            }
        }


    }
}