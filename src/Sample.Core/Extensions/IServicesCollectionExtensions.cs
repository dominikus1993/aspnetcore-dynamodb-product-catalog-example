using Microsoft.Extensions.DependencyInjection;
using Sample.Core.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Core.Extensions
{
    public static class IServicesCollectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddTransient<GetProductUseCase>();

            return services;
        }
    }
}
