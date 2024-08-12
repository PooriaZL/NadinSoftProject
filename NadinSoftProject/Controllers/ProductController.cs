using Microsoft.AspNetCore.Mvc;
using NadinSoftProject.Data;
using NadinSoftProject.Models.Dto;
using NadinSoftProject.Models;
using System.Collections.Generic;

namespace NadinSoftProject.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductDto>> GetAll()
        {
            return Ok(_db.Products.ToList());
        }
        [HttpGet("{id:int}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProductDto> GetById(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var product = _db.Products.FirstOrDefault(product => product.Id == id);
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
        public ActionResult<ProductDto> CreateProduct([FromBody]ProductDto productDto)
        {
            if(_db.Products.FirstOrDefault(product => product.ProductName == productDto.ProductName) != null)
            {
                ModelState.AddModelError("CustomError", "Product Already Exists");
                return BadRequest(ModelState);
            }
            if(productDto == null)
            {
                return BadRequest(productDto);
            }
            if(productDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            Product model = new()
            {
                Id = productDto.Id,
                ProductName = productDto.ProductName,
                ManufacturePhone = productDto.ManufacturePhone,
                ManufactureEmail = productDto.ManufactureEmail,
                IsAvailable = productDto.IsAvailable
            };
            _db.Products.Add(model);
            _db.SaveChanges();
            return CreatedAtRoute("GetById", new { id = productDto.Id }, productDto);
        }
    }
}
