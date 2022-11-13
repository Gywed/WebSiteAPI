using Application.UseCases.Employe.Dtos;
using Application.UseCases.Guest.Dtos;
using AutoMapper;
using Domain;
using Infrastructure.Ef.DbEntities;
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
            cfg.CreateMap<DbOrders, Order>();
            cfg.CreateMap<Order, DtoOutputOrder>()
                .ForMember(dest => dest.OrderContents,
                    act => act.MapFrom(src => src.Entries()));
            cfg.CreateMap<DbArticle, DtoOutputOrder.Article>();
            
            // Article
            cfg.CreateMap<Article,DbArticle>();
        });
        return new AutoMapper.Mapper(config);
    }
}