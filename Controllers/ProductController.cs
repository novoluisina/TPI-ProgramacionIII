using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TPI_ProgramacionIII.Data.Entities;
using TPI_ProgramacionIII.Data.Models;
using TPI_ProgramacionIII.Services.Interfaces;

namespace TPI_ProgramacionIII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productService.GetProducts();
            try
            {
                return Ok(products);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("{id}")]

        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound(); // 404
            }

            return Ok(product);
        }


        //[HttpDelete("{id}")]
        //public IActionResult DeleteProduct(int id)
        //{

        //    var deletedProduct = _productService.DeleteProduct(id);

        //    if (deletedProduct==null)
        //    {
        //        return NotFound(); 
        //    }

        //    return Ok(deletedProduct); 
        //}

        //[HttpPost]
        //public IActionResult AddProduct([FromBody] ProductDto productDto)
        //{

        //    var addedProduct = _productService.AddProduct(productDto);

        //    try
        //    {
        //        return Ok(addedProduct);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }


        //}

        //[HttpPut]
        //{

        //}
    }
}
