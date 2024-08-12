using Microsoft.AspNetCore.Mvc;
using NadinSoftProject.Models;

namespace NadinSoftProject.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public Product Get()
        {
            return new Product() { Id = 1, Name = "df" };
        }
    }
}
