using DgPays.Domain.ConfigurationModels;
using DgPays.Domain.Constants;
using DgPays.ProductConsumer.ConfigurationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Couchbase;
using Couchbase.Extensions.DependencyInjection;
using DgPays.ProductConsumer.Buckets;
using DgPays.ProductConsumer.Receivers;
using DgPays.ProductConsumer.Services;

namespace DgPays.ProductConsumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .ConfigureAppConfiguration((hostContext, config) =>
                    {
                        config.SetBasePath(Environment.CurrentDirectory)
                              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

                        config.AddEnvironmentVariables();
                    })
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.Configure<RabbitSettings>(hostContext.Configuration
                                                                      .GetSection(GlobalConstants.AppSettingsRabbitKey));
                        services.Configure<ConsumeQueueSettings>(hostContext.Configuration
                                                                            .GetSection(GlobalConstants.AppSettingsConsumeKey));

                        services.AddCouchbase(hostContext.Configuration
                                                         .GetSection(GlobalConstants.AppSettingsCouchbaseKey))
                                .AddCouchbaseBucket<IProductBucketProvider>("products");

                        services.AddHostedService<ProductConsumeReceiver>();
                        services.AddTransient<IProductService,ProductService>();

                    });
    }
}