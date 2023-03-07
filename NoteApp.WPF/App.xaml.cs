using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NoteApp.BL.Controller.NoteController;
using NoteApp.BL.Controller.UserController;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NoteApp.WPF;

public partial class App : Application
{
    public static IHost? AppHost { get; private set; }

    public App()
    {
        var builder = new ConfigurationBuilder();
        BuildConfig(builder);

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Build())
            .Enrich.FromLogContext()
            .WriteTo.File(/*Assembly.GetExecutingAssembly().Location +*/ "log.txt", retainedFileCountLimit: 1)
            .CreateLogger();

        Log.Logger.Information("Application Starting");

        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((hostcontext, services) =>
            {
                services.AddSingleton<MainWindow>();
                services.AddTransient<IUserController, UserController>();
                services.AddTransient<INoteController, NoteController>();
            })
            .UseSerilog()
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var startUpForm = AppHost.Services.GetRequiredService<MainWindow>();
        startUpForm.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }

    static void BuildConfig(IConfigurationBuilder builder)
    {
        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Prodaction"}.json", optional: true)
            .AddEnvironmentVariables();
    }
}
