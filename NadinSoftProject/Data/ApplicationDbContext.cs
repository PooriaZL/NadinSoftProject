using Microsoft.EntityFrameworkCore;
using NadinSoftProject.Models;

namespace NadinSoftProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
           .UseLazyLoadingProxies()
           .UseSqlServer(_configuration.GetConnectionString("DefaultSQLConnection"));
    }
}
