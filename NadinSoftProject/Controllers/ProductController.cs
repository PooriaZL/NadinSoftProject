using Microsoft.AspNetCore.Mvc;
using NadinSoftProject.Data;
using NadinSoftProject.Models.Dto;
using NadinSoftProject.Models;
using NadinSoftProject.Services;
using System.Collections.Generic;
using AutoMapper;

namespace NadinSoftProject.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsService _productsService;
        public ProductController(IProductsService productsService)
        {
            _productsService = productsService;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            return Ok(_productsService.GetAllProducts());
        }
        [HttpGet("{id:int}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var product = _productsService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProductDto> CreateProduct([FromBody] ProductDto productDto)
        {
            if (_productsService.GetProductByName(productDto.ProductName) != null)
            {
                ModelState.AddModelError("CustomError", "Product Already Exists");
                return BadRequest(ModelState);
            }
            if (productDto == null)
            {
                return BadRequest(productDto);
            }
            if (productDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            _productsService.AddProduct(productDto);
            return CreatedAtRoute("GetById", new { id = productDto.Id }, productDto);
        }
        [HttpDelete("{id:int}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var product = _productsService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            _productsService.DeleteProduct(product);
            return NoContent();
        }
        [HttpPut("{id:int}", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            if (productDto == null || id != productDto.Id)
            {
                return BadRequest();
            }
            _productsService.UpdateProduct(productDto);
            return NoContent();
        }
    }

}
