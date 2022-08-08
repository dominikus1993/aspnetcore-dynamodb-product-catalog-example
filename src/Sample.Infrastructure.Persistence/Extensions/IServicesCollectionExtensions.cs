using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Core.Repositories;
using Sample.Infrastructure.Persistence.Repositories;

namespace Sample.Core.Extensions
{
    public static class IServicesCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var aws = configuration.GetAWSOptions();
            services.AddDefaultAWSOptions(configuration.GetAWSOptions());
            services.AddAWSService<IAmazonDynamoDB>();
            services.AddScoped<IDynamoDBContext, DynamoDBContext>();
            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
            services.AddTransient<IProductRepository, DynamoDbProductRepository>();

            return services;
        }
    }
}
