using Microsoft.Azure.NotificationHubs;
using Microsoft.Extensions.Logging;
using Soenneker.Azure.NotificationHubs.Service.Abstract;
using Soenneker.Azure.NotificationHubs.Installations.Abstract;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Extensions.Task;
using Soenneker.Extensions.ValueTask;

namespace Soenneker.Azure.NotificationHubs.Installations;

/// <inheritdoc cref="IAzureNotificationHubInstallationService"/>
public sealed class AzureNotificationHubInstallationService : IAzureNotificationHubInstallationService
{
    private readonly IAzureNotificationHubService _notificationHubService;
    private readonly ILogger<AzureNotificationHubInstallationService> _logger;

    public AzureNotificationHubInstallationService(IAzureNotificationHubService notificationHubService,
        ILogger<AzureNotificationHubInstallationService> logger)
    {
        _notificationHubService = notificationHubService;
        _logger = logger;
    }

    public async ValueTask CreateOrUpdate(Installation installation, CancellationToken cancellationToken = default)
    {
        NotificationHubClient client = await _notificationHubService.Get(cancellationToken).NoSync();

        _logger.LogDebug("Creating or updating Azure Notification Hubs installation ({installationId})...",
            installation.InstallationId);

        await client.CreateOrUpdateInstallationAsync(installation, cancellationToken).NoSync();
    }

    public ValueTask CreateOrUpdate(string installationId, NotificationPlatform platform, string pushChannel,
        IReadOnlyCollection<string>? tags = null, string? userId = null,
        IDictionary<string, InstallationTemplate>? templates = null, IDictionary<string, string>? pushVariables = null,
        CancellationToken cancellationToken = default)
    {
        var installation = new Installation
        {
            InstallationId = installationId,
            Platform = platform,
            PushChannel = pushChannel,
            UserId = userId,
            Tags = tags is null ? null : new List<string>(tags),
            Templates = templates,
            PushVariables = pushVariables
        };

        return CreateOrUpdate(installation, cancellationToken);
    }

    public async ValueTask<Installation> Get(string installationId, CancellationToken cancellationToken = default)
    {
        NotificationHubClient client = await _notificationHubService.Get(cancellationToken).NoSync();

        _logger.LogDebug("Getting Azure Notification Hubs installation ({installationId})...", installationId);

        return await client.GetInstallationAsync(installationId, cancellationToken).NoSync();
    }

    public async ValueTask Patch(string installationId, IList<PartialUpdateOperation> operations,
        CancellationToken cancellationToken = default)
    {
        NotificationHubClient client = await _notificationHubService.Get(cancellationToken).NoSync();

        _logger.LogDebug("Patching Azure Notification Hubs installation ({installationId})...", installationId);

        await client.PatchInstallationAsync(installationId, operations, cancellationToken).NoSync();
    }

    public async ValueTask Delete(string installationId, CancellationToken cancellationToken = default)
    {
        NotificationHubClient client = await _notificationHubService.Get(cancellationToken).NoSync();

        _logger.LogDebug("Deleting Azure Notification Hubs installation ({installationId})...", installationId);

        await client.DeleteInstallationAsync(installationId, cancellationToken).NoSync();
    }
}