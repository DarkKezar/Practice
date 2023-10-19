using Cafe.Web.StartUps;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Cafe.Web.BackgroundServices;

public class GrpcListener : BackgroundService
{
    public GrpcListener()
    { }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(builder =>
            {
                builder
                    .ConfigureKestrel(options =>
                    {
                        options.ListenAnyIP(5268, listenOptions =>
                        {
                            listenOptions.Protocols = HttpProtocols.Http2;
                        });
                    })
                    .UseKestrel()
                    .UseStartup<GrpcServerStartup>();
            })
            .Build()
            .StartAsync(stoppingToken);
    }
}