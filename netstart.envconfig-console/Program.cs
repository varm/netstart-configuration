using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#if DEBUG
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", Environments.Staging);
        var envName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? Environments.Production;
        Console.WriteLine($"Env name:{envName}");
#endif

Console.WriteLine("Start service...");
IHostEnvironment CurrentEnvironment = null;
var builder = Host.CreateDefaultBuilder(args)
.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddEnvironmentVariables();
    var env = hostingContext.HostingEnvironment;
    CurrentEnvironment = env;
});

// .ConfigureAppConfiguration((hostingContext, config) =>
// {
//     config
//     .AddJsonFile("appsettings.json", true, true)
//     .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json");
// })

//builder.UseEnvironment(Environments.Development);

var host = builder.Build();

IConfiguration config = host.Services.GetRequiredService<IConfiguration>();
var name = config.GetValue<string>("App:Name");
var version = config.GetValue<double>("App:Version");
System.Console.WriteLine("*************************************");
System.Console.WriteLine($"Config-name:{name}\nConfig-version:{version}");
System.Console.WriteLine("*************************************");

System.Console.WriteLine("*****Judge environment*****");
if (CurrentEnvironment.IsDevelopment())
{
    System.Console.WriteLine("Development environment");
}
else if (CurrentEnvironment.IsProduction())
{
    System.Console.WriteLine("Production environment");
}
else if (CurrentEnvironment.IsStaging())
{
    System.Console.WriteLine("Staging environment");
}

await host.RunAsync();

