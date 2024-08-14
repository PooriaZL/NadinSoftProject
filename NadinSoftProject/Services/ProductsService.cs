using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using NadinSoftProject.Data;
using NadinSoftProject.Models;
using NadinSoftProject.Models.Dto;

namespace NadinSoftProject.Services
{
    public interface IProductsService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        Product GetProductByName(string productName);
        void AddProduct(ProductDto productDto, int userId );
        void DeleteProduct(Product product, int userId);
        void UpdateProduct(ProductDto productDto, int userId);
    }
    public class ProductsService : IProductsService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public ProductsService(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }
        public Product GetProductById(int id)
        {
            return _db.Products.FirstOrDefault(product => product.Id == id);
        }
        public Product GetProductByName(string productName)
        {
            return _db.Products.FirstOrDefault(product => product.ProductName == productName);
        }
        public void AddProduct(ProductDto productDto, int userId)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);
            var product = _mapper.Map<Product>(productDto);
            product.User = user;
            _db.Products.Add(product);
            _db.SaveChanges();
        }
        public void DeleteProduct(Product product, int userId)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);
            if(user.Id != product.User.Id)
            {
                throw new UnauthorizedAccessException();
            }
            _db.Products.Remove(product);
            _db.SaveChanges();
        }
        public void UpdateProduct(ProductDto productDto, int userId)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);
            var product = _mapper.Map<Product>(productDto);
            if (user.Id != product.User.Id)
            {
                throw new UnauthorizedAccessException();
            }
            _db.Products.Update(product);
            _db.SaveChanges();
        }
    }
}
