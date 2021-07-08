using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Microsoft.Extensions.DependencyInjection;
using Sample.Core.Repositories;
using Sample.Core.UseCase;
using Sample.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Core.Extensions
{
    public static class IServicesCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            var credentials = new BasicAWSCredentials("<ACCESS_KEY>", "<SECRET_KEY>");
            var config = new AmazonDynamoDBConfig()
            {
                RegionEndpoint = RegionEndpoint.APSoutheast2
            };
            var client = new AmazonDynamoDBClient(credentials, config);
            services.AddSingleton<IAmazonDynamoDB>(client);
            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
            services.AddTransient<IProductRepository, DynamoDbProductRepository>();

            return services;
        }
    }
}
