using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace StartupBasic 
{
    public class Startup
    {
        IConfigurationRoot _config;

        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
            //This is the most basic configuration you can have
            var builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables();
            _config = builder.Build();

        }

        public void ConfigureServices(IServiceCollection services)
        {
            //This is the only service available at ConfigureServices
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            app.Run(async context =>
            {
                foreach(var e in _config.AsEnumerable())
                {
                    await context.Response.WriteAsync($"{e.Key} = {e.Value}\n");
                }
            });
        }
    }
    
   public class Program
    {
        public static void Main(string[] args)
        {
              var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}