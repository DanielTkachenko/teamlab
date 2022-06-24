using Downloader;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.InteropServices;
using TeamlabServer;
using Vk.Provider;

IHost hostBuilder = CreateHostBuilder(args);
await hostBuilder.RunAsync();

static IHost CreateHostBuilder(string[] args)
{
IHostBuilder? defaultBuilder = Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
{
services.AddServer();
});

return RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
    ? defaultBuilder.UseWindowsService(options => { options.ServiceName = "TestServer"; }).Build()
    : defaultBuilder.Build();
}

public static class AddServerExtension
{
    public static IServiceCollection AddServer(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHostedService<MyServer>();
        return serviceCollection;
    }
}


