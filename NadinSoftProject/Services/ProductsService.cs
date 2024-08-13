using AutoMapper;
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
        void AddProduct(ProductDto productDto);
        void DeleteProduct(Product product);
        void UpdateProduct(ProductDto productDto);
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
        public void AddProduct(ProductDto productDto)
        {
            _db.Products.Add(_mapper.Map<Product>(productDto));
            _db.SaveChanges();
        }
        public void DeleteProduct(Product product)
        {
            _db.Products.Remove(product);
            _db.SaveChanges();
        }
        public void UpdateProduct(ProductDto productDto)
        {
            _db.Products.Update(_mapper.Map<Product>(productDto));
            _db.SaveChanges();
        }
    }
}
