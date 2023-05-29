using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

var host = Host.CreateDefaultBuilder(args)
// .ConfigureAppConfiguration(
//     (hostingContext, config) =>
//     {
//         var env = hostingContext.HostingEnvironment;
//         if (env.IsDevelopment() && !string.IsNullOrEmpty(env.ApplicationName))
//         {
//             var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
//             if (appAssembly != null)
//             {
//                config.AddUserSecrets(appAssembly, true);
//             }
//         }
//     })

.Build();

IConfiguration config = host.Services.GetRequiredService<IConfiguration>();
var env = config.GetValue<string>("App:Env");
var key = config.GetValue<long>("App:Apikey");
System.Console.WriteLine($"Environment:{env}\nApiKey:{key}");

await host.RunAsync();