using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Azure.NotificationHubs.Installations.Abstract;

namespace Soenneker.Azure.NotificationHubs.Installations.Registrars;

/// <summary>
/// Installation registration and lifecycle helpers for Azure Notification Hubs.
/// </summary>
public static class AzureNotificationHubInstallationServiceRegistrar
{
    /// <summary>
    /// Adds <see cref="IAzureNotificationHubInstallationService"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddAzureNotificationHubInstallationServiceAsSingleton(this IServiceCollection services)
    {
        services.TryAddSingleton<IAzureNotificationHubInstallationService, AzureNotificationHubInstallationService>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IAzureNotificationHubInstallationService"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddAzureNotificationHubInstallationServiceAsScoped(this IServiceCollection services)
    {
        services.TryAddScoped<IAzureNotificationHubInstallationService, AzureNotificationHubInstallationService>();

        return services;
    }
}
