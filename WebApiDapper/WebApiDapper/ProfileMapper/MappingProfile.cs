using AutoMapper;
using WebAPICoreDapper.Models;
using WebApiDapper.DTOs.CategoryDTO;
using WebApiDapper.DTOs.ExtendAttribute;
using WebApiDapper.DTOs.ProductDTO;
using WebApiDapper.DTOs.UserDTO;
using WebApiDapper.Entities;

namespace WebApiDapper.ProfileMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category
            CreateMap<CategoryRequestDTO, Category>();
            CreateMap<Category, CategoryResponseDTO>();
            // ExtendAttribute
            CreateMap<ExtendAttributeCreateRequestDTO, ExtendAttribute>();
            // Product
            CreateMap<ProductCreateRequestDTO, Product>();
            CreateMap<Product,ProductCreateResponseDTO>();
            CreateMap<ProductUpdateRequestDTO, Product>();
            // User
            CreateMap<UserRequestDTO, AppUser>();
            CreateMap<UserUpdateDTO, AppUser>();
            CreateMap<AppUser, UserResponseDTO>();
        }
    }
}
