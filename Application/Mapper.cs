using Application.UseCases.Client.Dtos;
using Application.UseCases.Employe.Dtos;
using Application.UseCases.Guest.Dtos;
using AutoMapper;
using Domain;
using Infrastructure.Ef.DbEntities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using DtoOutputArticle = Application.UseCases.Administrator.Article.Dtos.DtoOutputArticle;

namespace Application;

public static class Mapper
{
    private static AutoMapper.Mapper _instance;

    public static AutoMapper.Mapper GetInstance()
    {
        return _instance ??= CreateMapper();
    }

    private static AutoMapper.Mapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            // Source, Destination
            // User
            cfg.CreateMap<User, DtoOutputUser>();
            cfg.CreateMap<DbUser, DtoOutputUser>();
            cfg.CreateMap<DbUser, User>();
            
            // Order
            cfg.CreateMap<DbOrders, DtoOutputOrder>();
            cfg.CreateMap<OrderContent, DtoOutputOrder.OrderContent>();
            cfg.CreateMap<DbOrderContent, OrderContent>()
                .ForMember(dest => dest.Prepared,
                    act => act.MapFrom(src => 
                        Convert.ToBoolean(src.prepared)));
            cfg.CreateMap<DbOrders, Order>();
            cfg.CreateMap<Order, DtoOutputOrder>()
                .ForMember(dest => dest.OrderContents,
                    act => act.MapFrom(src => src.Entries()));
            cfg.CreateMap<DtoInputOrder, DbOrders>();
            cfg.CreateMap<DbOrders, DtoInputOrder>();
            
            // Article
            cfg.CreateMap<DbArticle,Article>();
            cfg.CreateMap<DbArticle, DtoOutputOrder.Article>();
            cfg.CreateMap<Article,DtoOutputOrder.Article>();
            cfg.CreateMap<DbArticle,DtoOutputArticle>();
            
            // Brand
            cfg.CreateMap<DbBrand, Brand>();
            cfg.CreateMap<DbBrand, DtoOutputOrder.Brand>();
            cfg.CreateMap<Brand, DtoOutputOrder.Brand>();            
            cfg.CreateMap<DbBrand, DtoOutputBrands>();
            cfg.CreateMap<Brand, DtoOutputBrands>();

            // Category
            cfg.CreateMap<DbCategory,Category>();
            cfg.CreateMap<DbCategory,DtoOutputOrder.Category>();
            cfg.CreateMap<Category,DtoOutputOrder.Category>();
            cfg.CreateMap<DbCategory, DtoOutputCategory>();

        });
        return new AutoMapper.Mapper(config);
    }
}