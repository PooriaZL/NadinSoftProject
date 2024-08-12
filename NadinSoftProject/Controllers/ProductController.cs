using Microsoft.AspNetCore.Mvc;
using NadinSoftProject.Models.Dto;
using System.Collections.Generic;

namespace NadinSoftProject.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetAll()
        {
            return Ok(new List<ProductDto>
            {
                new ProductDto() { ProductName = "df" },
                new ProductDto() { ProductName = "df" }
            });
        }
        [HttpGet("Id:int")]
        public ActionResult<ProductDto> GetById(int id)
        {
            return Ok(new ProductDto() { ProductName = "df" });
        }
    }
}
