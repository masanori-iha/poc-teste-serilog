
using Reo.Core.Serilog.Sinks.SlackWebHook;
using Serilog;
using Serilog.Core;
using Serilog.Events;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var levelSwitch = new LoggingLevelSwitch();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .WriteTo.File("log2.txt",
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")

    // Logando dados no slack
    .WriteTo.Slack("https://hooks.slack.com/services/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")

    // Logando dados no SEQ
    .MinimumLevel.ControlledBy(levelSwitch)
    .WriteTo.Seq("http://localhost:5341",
                 apiKey: "T2C6q9ArUw0pKQhfaFde",
                 controlLevelSwitch: levelSwitch)

    .CreateLogger();

try
{


    for (int i = 0; i < 100000; i++)
    {
        Thread.Sleep(5000);

        if( i % 2 == 0)
        {
            
            Log.Information($"This is a great development, Daniel {i}");
        }
        else
        {
            Log.Fatal($"This is a Fatal error, Masanori {i}");
        }
    }

    Log.Information("Application Starting Up");




    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllersWithViews();

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");

        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal("Ops, the application failed to start correctly");
}
finally
{
    Log.CloseAndFlush();
}


