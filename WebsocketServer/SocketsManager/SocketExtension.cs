using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace WebsocketServer.SocketsManager
{
    public static class SocketExtension
    {
        public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
        {
            services.AddSingleton<ConnectionManager>();

            foreach (var type in Assembly.GetEntryAssembly().ExportedTypes)
            {
                if (type.GetTypeInfo().BaseType == typeof(SocketHandler))
                {
                    services.AddScoped(type);
                }
            }

            return services;
        }
    }
}