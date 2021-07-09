using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sample.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cocona;
using Cocona.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using Sample.Core.UseCase;

namespace Sample.Importer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args)
                 .UseCocona(args, new[] { typeof(Program) })
                 .Build()
                 .RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder =>
                {
                    builder.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddCore();
                    services.AddInfrastructure(hostContext.Configuration);
                });

        [PrimaryCommand]
        public async Task RemoveFiles([FromService] ImportProductsUseCase useCase)
        {
            await useCase.Execute();
        }
    }


}
