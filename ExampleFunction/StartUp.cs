using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;

[assembly: FunctionsStartup(typeof(Functions.Startup))]

namespace Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<AppSettingsOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.Bind(settings);
                });
            builder.Services.AddLogging();
        }
    }
}
