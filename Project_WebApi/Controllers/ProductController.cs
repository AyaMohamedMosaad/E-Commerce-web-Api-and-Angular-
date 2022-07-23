using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_WebApi.DTO;
using Project_WebApi.Models;
using Project_WebApi.Repositories;
using System;

namespace Project_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        IProductRepository productRepository;

        public ProductController(IProductRepository _productRepository)
        {
            this.productRepository = _productRepository;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(productRepository.DisplyAppProducts());
        }

        [HttpGet("{id:int}", Name = "GetOneProductRoute")]
        public IActionResult GetProductById([FromRoute]int id)
        {
            return Ok(productRepository.DisplayProductByID(id));
            
        }


        [HttpPost]
        public IActionResult AddNewProduct([FromBody] ProductDTO Newproduct)
        {
            if (ModelState.IsValid == true)
            {
                productRepository.AddProduct(Newproduct);
                string url = Url.Link("GetOneProductRoute", new { id = Newproduct.productId });
                return Created(url, Newproduct);
            }
            else return BadRequest(ModelState);


        }




        //[HttpPost]
        //public IActionResult AddNewProduct([FromBody] Product Newproduct)
        //{
        //    if (ModelState.IsValid == true)
        //    {
        //        productRepository.AddProduct(Newproduct);
        //        string url = Url.Link("GetOneProductRoute", new { id = Newproduct.ProductID });
        //        return Created(url, Newproduct);
        //    }
        //    else return BadRequest(ModelState);


        //}






        [HttpPut("{id:int}")]
        public IActionResult UpdateProduct([FromRoute]int id,[FromBody]Product NewProduct)
        {
            if(ModelState.IsValid)
            {
                productRepository.EditProduct(id, NewProduct);
                return StatusCode(204, "the data Updated");

            }
            return BadRequest("Id Not Valid");

        }

      [HttpDelete("{id:int}")]
      public IActionResult DeleteOneProduct(int id)
        {

            try
            {
                 productRepository.DeleteProduct(id);
                return StatusCode(204, "Record Removed");
            }
             catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
               
        }

    }
}
