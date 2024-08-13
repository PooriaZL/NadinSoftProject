using Moq;
using NadinSoftProject.Controllers;
using NadinSoftProject.Models;
using NadinSoft.UnitTests.Helpers;
using NadinSoftProject.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NadinSoftProject.Services;
using AutoMapper;
using NadinSoftProject.Models.Dto;
using System.Configuration;
namespace NadinSoft.UnitTests
{
    public class TestProductController
    {
        public TestProductController()
        {
        }
        [Fact]
        public void GetAll_OnSuccess_ReturnsStatusCode200()
        {
            var mockProductService = new Mock<IProductsService>();
            mockProductService.Setup(service => service.GetAllProducts()).Returns(MockHelper.GetFakeProductList());
            var pc = new ProductController(mockProductService.Object);
            var result = (OkObjectResult)pc.GetAll();
            result.StatusCode.Should().Be(200);
        }
        [Theory]
        [InlineData(1)]
        public void GetbyId_OnSuccess_ReturnsStatusCode200(int id)
        {
            var mockProductService = new Mock<IProductsService>();
            mockProductService.Setup(service => service.GetProductById(id)).Returns(new Product() { Id = 1, ProductName = "Apple" });
            var pc = new ProductController(mockProductService.Object);
            var result = (OkObjectResult)pc.GetById(id);
            result.StatusCode.Should().Be(200);
        }
    }
}