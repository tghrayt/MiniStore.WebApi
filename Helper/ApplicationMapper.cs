using AutoMapper;
using MiniStore.Dto;
using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<CategoryDto, Category>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<ProductDto, Product>().ReverseMap();
        }
    }
}
