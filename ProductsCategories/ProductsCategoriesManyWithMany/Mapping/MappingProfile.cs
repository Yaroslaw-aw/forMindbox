using AutoMapper;
using ProductsCategoriesManyWithMany.Db;
using ProductsCategoriesManyWithMany.Dto;

namespace ProductsCategoriesManyWithMany.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<PostProductCategoryDto, ProductCategory>().ReverseMap();

            CreateMap<GetCategoryDto, Category>().ReverseMap();

            CreateMap<GetProductDto, Product>().ReverseMap();
        }
    }
}
