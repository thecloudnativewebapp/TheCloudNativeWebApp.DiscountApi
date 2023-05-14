using AutoMapper;
using TheCloudNativeWebApp.Discounts.Infrastructure.Sql.DataModel.Mapping;

namespace TheCloudNativeWebApp.Discounts.Infrastructure.Sql.Tests;

public static class MapperFactory
{
    public static IMapper Create() => new MapperConfiguration(x => x.AddProfile<AutoMapperProfile>())
        .CreateMapper();
}