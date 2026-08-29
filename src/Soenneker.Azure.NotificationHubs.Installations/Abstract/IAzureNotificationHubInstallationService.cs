using Microsoft.Azure.NotificationHubs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Azure.NotificationHubs.Installations.Abstract;

/// <summary>
/// Installation registration and lifecycle helpers for Azure Notification Hubs.
/// </summary>
public interface IAzureNotificationHubInstallationService
{
    /// <summary>
    /// Creates or updates a device installation.
    /// </summary>
    /// <param name="installation">Installation for the create or update operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the or update creation is complete.</returns>
    ValueTask CreateOrUpdate(Installation installation, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates or updates a device installation from the supplied installation details.
    /// </summary>
    /// <param name="installationId">Identifier of the Notification Hub installation.</param>
    /// <param name="platform">Platform for the create or update operation.</param>
    /// <param name="pushChannel">The platform push channel, token, or registration id.</param>
    /// <param name="tags">Optional installation tags.</param>
    /// <param name="userId">Optional user id associated with the installation.</param>
    /// <param name="templates">Optional installation templates.</param>
    /// <param name="pushVariables">Optional push variables.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the or update creation is complete.</returns>
    ValueTask CreateOrUpdate(string installationId, NotificationPlatform platform, string pushChannel, IReadOnlyCollection<string>? tags = null, string? userId = null,
        IDictionary<string, InstallationTemplate>? templates = null, IDictionary<string, string>? pushVariables = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a device installation.
    /// </summary>
    /// <param name="installationId">Identifier of the Notification Hub installation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested installation.</returns>
    ValueTask<Installation> Get(string installationId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Patches a device installation.
    /// </summary>
    /// <param name="installationId">Identifier of the Notification Hub installation.</param>
    /// <param name="operations">Operations to execute, in order.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the patch operation is complete.</returns>
    ValueTask Patch(string installationId, IList<PartialUpdateOperation> operations, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a device installation.
    /// </summary>
    /// <param name="installationId">Identifier of the Notification Hub installation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes after the targeted files have been deleted.</returns>
    ValueTask Delete(string installationId, CancellationToken cancellationToken = default);
}
