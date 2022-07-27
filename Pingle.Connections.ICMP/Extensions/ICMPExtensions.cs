using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pingle.Shared.Abstractions;

namespace Pingle.Connections.ICMP.Extensions;

public static class ICMPExtensions
{
    public static IServiceCollection WithICMPSupport(this IServiceCollection serviceCollection, IConfiguration config)
    {
        // todo: figure out proper lifetime
        return serviceCollection.AddScoped<IConnector, ICMPConnection>();
    }
}
