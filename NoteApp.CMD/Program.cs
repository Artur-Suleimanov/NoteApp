using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using NoteApp.BL.Model.User;
using NoteApp.BL.Model.Note;
using NoteApp.BL.Controller.UserController;
using NoteApp.BL.Controller.NoteController;
using System;
using System.Reflection;
using NoteApp.BL;

namespace NoteApp.CMD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.File(/*Assembly.GetExecutingAssembly().Location +*/ "log.txt", retainedFileCountLimit: 1)
                .CreateLogger();

            Log.Logger.Information("Application Starting");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IGreetingSevice , GreetingSevice>();
                    services.AddTransient<IUserController, UserController>();
                    services.AddTransient<INoteController, NoteController>();
                })
                .UseSerilog()
                .Build();

            var svc = ActivatorUtilities.CreateInstance<GreetingSevice>(host.Services);

            svc.Run();
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Prodaction"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}