﻿using Microsoft.AspNetCore.Mvc;
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
            }
            catch (Exception ex)
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
                return NotFound($"El producto con el ID: {id} no fue encontrado"); 
            }

            return Ok(product);
        }


        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDto productDto)
        {
            if (productDto.Name == null || productDto.Price <= 0)
            {
                return BadRequest("Producto no creado, por favor completar los campos");
            }
            try
            {
                var product = new Product()
                {
                    Name = productDto.Name,
                    Price = productDto.Price,
                    Stock = productDto.Stock
                };
   
                int id = _productService.CreateProduct(product);

                return Ok($"Producto creado exitosamente con id: {id}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpDelete("{id}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            try
            {
                var existingProduct = _productService.GetProductById(id);

                if (existingProduct == null)
                {
                    return NotFound($"No se encontró ningún producto con el ID: {id}");
                }

                _productService.DeleteProduct(id);
                return Ok($"Producto con ID: {id} eliminado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public IActionResult UpdateProduct([FromRoute] int id, [FromBody] ProductPutDto product)
        {
            var productToUpdate = _productService.GetProductById(id);
            if (productToUpdate == null)
            {
                return NotFound($"Producto con ID {id} no encontrado");
            }

            try
            {
                productToUpdate.Price = product.Price;
                productToUpdate.Stock = product.Stock;

                productToUpdate = _productService.UpdateProduct(productToUpdate);
                return Ok($"Producto actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el producto: {ex.Message}");
            }
        }
    }
}