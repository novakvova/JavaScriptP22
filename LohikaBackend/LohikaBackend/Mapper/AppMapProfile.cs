using AutoMapper;
using Data.LohikaBackend.Entities;
using Data.LohikaBackend.Entities.Identity;
using LohikaBackend.Models;
using System.Globalization;

namespace LohikaBackend.Mapper
{
    public class AppMapProfile : Profile
    {
        public AppMapProfile()
        {
            CreateMap<RegisterViewModel, AppUser>()
                .ForMember(x => x.Photo, opt => opt.Ignore())
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Email));

            CreateMap<ProductAddViewModel, ProductEntity>()
               .ForMember(x => x.Image, opt => opt.Ignore());

            CreateMap<ProductEntity, ProductItemViewModel>()
               .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.Category.Title))
               .ForMember(x => x.Images, opt => opt.MapFrom(x => x.ProductImages.Select(x => x.Name)));

            CreateMap<ProductEntity, ProductEditViewModel>()
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.Category.Title))
                .ForMember(x => x.Images, opt => opt.MapFrom(x => x.ProductImages
                    .Select(x => new ProductImageItemViewModel { Id = x.Id, Name = x.Name })));



            CreateMap<CartAddViewModel, CartEntity>();

            CreateMap<CartEntity, CartItemViewModel>()
               .ForMember(x => x.ProductId, opt => opt.MapFrom(x => x.Product.Id))
               .ForMember(x => x.ProductName, opt => opt.MapFrom(x => x.Product.Name))
               .ForMember(x => x.ProductImage, opt => opt.MapFrom(x => @"/images/" + x.Product.Image))
               .ForMember(x => x.Images, opt => opt.MapFrom(x => x.Product.ProductImages.Select(x => x.Name)))
               .ForMember(x => x.ProductPrice, opt => opt.MapFrom(x => x.Product.Price));



            CreateMap<AppUser, UserItemViewModel>()
               .ForMember(x => x.Photo, opt => opt.MapFrom(x => x.Photo));

            CreateMap<AppUser, UserEditViewModel>()
                .ForMember(x => x.Photo, opt => opt.MapFrom(x => x.Photo));

            CreateMap<UserSaveViewModel, AppUser>()
                .ForMember(x => x.Photo, opt => opt.Ignore())
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Email));

            CreateMap<AppUser, ProfileViewModel>()
                .ForMember(x => x.Photo, opt => opt.MapFrom(x => x.Photo));



            CreateMap<CategoryAddViewModel, CategoryEntity>()
             .ForMember(x => x.Image, opt => opt.Ignore());

            CreateMap<CategoryEntity, CategoryItemViewModel>()
               .ForMember(x => x.Image, opt => opt.MapFrom(x => x.Image));

            CreateMap<CategoryEntity, CategoryEditViewModel>()
                .ForMember(x => x.Image, opt => opt.MapFrom(x => x.Image));

            var cultureInfo = new CultureInfo("uk-UA");

            CreateMap<BlogAddViewModel, BlogEntity>()
                .ForMember(x => x.DateTimePublish, opt => opt.MapFrom(x =>
                    DateTime.SpecifyKind(DateTime.Parse(x.DateTimePublish, cultureInfo), DateTimeKind.Utc)))
                .ForMember(x => x.DateCreated, opt => opt.MapFrom(x =>
                    DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)))
                .ForMember(x => x.Image, opt => opt.Ignore());

            CreateMap<BlogEntity, BlogItemViewModel>()
                .ForMember(x => x.DateTimePublish, opt => opt.MapFrom(x => x.DateTimePublish.ToString("dd.MM.yyyy")))
                .ForMember(x => x.Image, opt => opt.MapFrom(x => x.Image));

            CreateMap<BlogEntity, BlogEditViewModel>()
                .ForMember(x => x.DateTimePublish, opt => opt.MapFrom(x => x.DateTimePublish.ToString("dd.MM.yyyy")))
                .ForMember(x => x.Image, opt => opt.MapFrom(x => x.Image));


            CreateMap<OrderStatusEntity, OrderStatusItemViewModel>();
            CreateMap<OrderAddViewModel, OrderEntity>()
                .ForMember(x => x.DateCreated, opt => opt.MapFrom(x =>
                    DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)))
                .ForMember(x => x.OrderItems, opt => opt.Ignore());

            CreateMap<OrderItemAddViewModel, OrderItemEntity>();

            CreateMap<OrderItemEntity, OrderItemViewModel>()
                .ForMember(x => x.ProductName, opt => opt.MapFrom(x => x.Product.Name))
                .ForMember(x => x.ProductImage, opt => opt.MapFrom(x => x.Product.ProductImages.Select(x => x.Name)));

            CreateMap<OrderEntity, OrderViewModel>()
                .ForMember(x => x.DateCreated, opt => opt.MapFrom(x => x.DateCreated.ToString("dd.MM.yyyy HH:mm:ss")))
                .ForMember(x => x.StatusName, opt => opt.MapFrom(x => x.OrderStatus.Name))
                .ForMember(x => x.Items, opt => opt.MapFrom(x => x.OrderItems));

            CreateMap<FeedbackAddViewModel, FeedbackEntity>()
                .ForMember(x => x.DateCreated, opt => opt.MapFrom(x =>
                         DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)));

            CreateMap<FeedbackEntity, FeedbackItemViewModel>()
                .ForMember(x => x.DateCreated, opt => opt.MapFrom(x => x.DateCreated.ToString("dd.MM.yyyy HH:mm:ss")));




        }
    }
}
