using Application.UseCases.Administrator.Article.Dtos;
using Application.UseCases.Administrator.Dtos;
using Application.UseCases.Client.Dtos;
using Application.UseCases.dtosGlobal.DtoEntities;
using Application.UseCases.Employe.Dtos;
using Application.UseCases.Guest.Dtos;
using AutoMapper;
using Domain;
using Infrastructure.Ef.DbEntities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            cfg.CreateMap<DbUser, DtoOutputUsername>();
            
            // Order
            cfg.CreateMap<DbOrders, DtoOutputOrder>();
            cfg.CreateMap<OrderContent, DtoOutputOrder.OrderContent>();
            cfg.CreateMap<DbOrders, Order>();
            cfg.CreateMap<Order, DtoOutputOrder>()
                .ForMember(dest => dest.OrderContents,
                    act => act.MapFrom(src => src.Entries()));
            cfg.CreateMap<DtoInputOrder, DbOrders>();
            cfg.CreateMap<DbOrders, DtoInputOrder>();
            
            // OrderHistory 
            cfg.CreateMap<DbOrdersHistory, DtoOutputOrderHistory>();
            cfg.CreateMap<OrderHistoryContent, DtoOutputOrderHistory.OrderHistoryContent>();
            cfg.CreateMap<DbOrdersHistory, OrderHistory>();
            cfg.CreateMap<OrderHistory, DtoOutputOrderHistory>()
                .ForMember(dest => dest.OrderHistoryContents,
                    act => act.MapFrom(src => src.Entries()));
            
            // Article
            cfg.CreateMap<DbArticle,Article>();
            cfg.CreateMap<Article,DtoOutputArticle>();
            cfg.CreateMap<DbArticle,DtoOutputArticle>();
            
            // Brand
            cfg.CreateMap<DbBrand, Brand>();
            cfg.CreateMap<DbBrand, DtoOutputBrands>();
            cfg.CreateMap<Brand, DtoOutputBrands>();

            // Category
            cfg.CreateMap<DbCategory,Category>();
            cfg.CreateMap<Category,DtoOutputCategory>();
            cfg.CreateMap<DbCategory, DtoOutputCategory>();
            
            //Family
            cfg.CreateMap<DbFamily, DtoOutputFamily>();
            
            //ArticleFamily
            cfg.CreateMap<DbArticleFamilies, DtoOutputArticleFamily>();

        });
        return new AutoMapper.Mapper(config);
    }
}