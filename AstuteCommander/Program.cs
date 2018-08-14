using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog.Filters;
using Microsoft.Extensions.Logging;
using Serilog;
//using PostSharp.Patterns.Diagnostics;

namespace AstuteCommander
{
//    [Log(AttributeExclude = true)]
    public class Program
    {        
        public static int Main(string[] args)
        {
//            LoggingServices.DefaultBackend = new PostSharp.Patterns.Diagnostics.Backends.Serilog.SerilogLoggingBackend();

            //var isController = Matching.FromSource("AstutCommander.Controllers");
            //var isVsts = Matching.FromSource("CooperCommander.Classes.VSTS");

            //Log.Logger = new LoggerConfiguration()
            //        //                    .MinimumLevel.Verbose()
            //        //                    .WriteTo.RollingFile("logs/all-{Date}.log")
            //        .WriteTo.Logger(l => l
            //            .MinimumLevel.Verbose()
            //            .Filter.ByIncludingOnly(e => isController(e) || isVsts(e))
            //            //.Filter.ByIncludingOnly(isController)
            //            .WriteTo.RollingFile("c:/temp/logs/controllers-{Date}.log"))
            //.CreateLogger();

            try
            {
                Log.Information("Starting web host");
                BuildWebHost(args).Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        public static IWebHost BuildWebHost(string[] args) //=>
        {
            var builder = new WebHostBuilder()
                        .UseKestrel()
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .ConfigureAppConfiguration((builderContext, config) =>
                        {
                            IHostingEnvironment env = builderContext.HostingEnvironment;

                            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                        })
                        .UseIISIntegration()
                        .UseDefaultServiceProvider((context, options) =>
                        {
                            options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                        })
                        .UseStartup<Startup>()
                        .UseSerilog()
                        .Build();

            //var builder = WebHost.CreateDefaultBuilder(args)
            //                .UseStartup<Startup>()
            //                .Build();

            return builder;

        }

    }
}