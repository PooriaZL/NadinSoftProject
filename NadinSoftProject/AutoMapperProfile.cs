using AutoMapper;
using NadinSoftProject.Models;
using NadinSoftProject.Models.Dto;

namespace NadinSoftProject
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
        }
    }
}
