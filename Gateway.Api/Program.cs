using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Reflection;

namespace Gateway.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureWebHostDefaults(webBuilder =>
              {
                  webBuilder.UseStartup<Startup>();
              })
          .ConfigureAppConfiguration((hostingContext, config) =>
          {
              config.AddJsonFile($"ocelot.json");

                // for local test use below one, multi env json file not worked
                //config.AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
                // https://github.com/ThreeMammals/Ocelot/issues/249
            });



        //private static void ConfigureLogging()
        //{
        //    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        //    var configuration = new ConfigurationBuilder()
        //        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //        .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
        //        .AddJsonFile(
        //            $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
        //            optional: true)
        //        .Build();

        //    Log.Logger = new LoggerConfiguration()
        //        .Enrich.FromLogContext()
        //        .Enrich.WithMachineName()
        //        .WriteTo.Debug()
        //        .WriteTo.Console()
        //        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
        //        .Enrich.WithProperty("Environment", environment)
        //        .ReadFrom.Configuration(configuration)
        //        .CreateLogger();
        //}

        //private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
        //{
        //    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
        //    {
        //        AutoRegisterTemplate = true,
        //        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
        //    };
        //}

        //public static void Main(string[] args)
        //{
        //    //configure logging first
        //    ConfigureLogging();

        //    //then create the host, so that if the host fails we can log errors
        //    CreateHost(args);
        //}
        //private static void CreateHost(string[] args)
        //{
        //    try
        //    {
        //        CreateHostBuilder(args).Build().Run();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Log.Fatal($"Failed to start {Assembly.GetExecutingAssembly().GetName().Name}", ex);
        //        throw;
        //    }
        //}

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        })
        //        .ConfigureAppConfiguration(configuration =>
        //        {
        //            configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        //            configuration.AddJsonFile(
        //                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
        //                optional: true);
        //        })
        //        .UseSerilog();
    }
}
