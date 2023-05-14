using AutoMapper;

namespace TheCloudNativeWebApp.Discounts.Api.Configuration;

public static class AutoMapperConfiguration
{
    public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection serviceCollection)
    {
        var mapper = new MapperConfiguration(x =>
            {
                x.AddProfile<AutoMapperProfile>();
                x.AddProfile<global::TheCloudNativeWebApp.Discounts.Infrastructure.Sql.DataModel.Mapping.AutoMapperProfile>();
            })
            .CreateMapper();

        serviceCollection.AddSingleton(mapper);

        return serviceCollection;
    }
}