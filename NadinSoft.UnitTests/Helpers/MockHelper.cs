using Microsoft.EntityFrameworkCore;
using Moq;
using NadinSoftProject.Models;
using NadinSoftProject.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadinSoft.UnitTests.Helpers
{
    internal class MockHelper
    {
        public static List<Product> GetFakeProductList()
        {
            return new List<Product>()
            {
                new Product
                {
                    Id = 1,
                    ProductName = "Banana",
                },
                new Product
                {
                    Id = 2,
                    ProductName = "Apple"
                }
            };
        }
    }
}
