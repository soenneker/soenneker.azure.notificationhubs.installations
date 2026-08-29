[![](https://img.shields.io/nuget/v/soenneker.azure.notificationhubs.installations.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.azure.notificationhubs.installations/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.azure.notificationhubs.installations/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.azure.notificationhubs.installations/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.azure.notificationhubs.installations.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.azure.notificationhubs.installations/)

# Soenneker.Azure.NotificationHubs.Installations

Server-side helpers for creating, retrieving, patching, and deleting Azure Notification Hubs device installations.

## Installation

```bash
dotnet add package Soenneker.Azure.NotificationHubs.Installations
```

## Configuration and registration

```json
{
  "Azure": {
    "NotificationHubs": {
      "ConnectionString": "Endpoint=sb://...",
      "HubName": "notifications"
    }
  }
}
```

```csharp
using Soenneker.Azure.NotificationHubs.Installations.Registrars;

builder.Services.AddAzureNotificationHubInstallationServiceAsSingleton();
```

Use a connection string with only the permissions required by this backend and keep it in a secret provider.

## Create or update an installation

```csharp
using Microsoft.Azure.NotificationHubs;
using Soenneker.Azure.NotificationHubs.Installations.Abstract;

public sealed class PushRegistrationService(
    IAzureNotificationHubInstallationService installations)
{
    public ValueTask Register(
        string installationId,
        string fcmToken,
        string userId,
        CancellationToken cancellationToken) =>
        installations.CreateOrUpdate(
            installationId,
            NotificationPlatform.FcmV1,
            fcmToken,
            tags: [$"user:{userId}"],
            userId: userId,
            cancellationToken: cancellationToken);
}
```

`CreateOrUpdate` is an upsert: reusing an installation ID replaces its platform, channel, tags, user ID, templates, and push variables with the supplied installation state.

## Other operations

```csharp
Installation installation = await installations.Get(
    installationId,
    cancellationToken);

await installations.Patch(
    installationId,
    patchOperations,
    cancellationToken);

await installations.Delete(installationId, cancellationToken);
```

Identifiers and push channels must be non-blank, and patch calls require at least one operation. Azure SDK exceptions are allowed to propagate so callers can handle not-found, authorization, throttling, and service failures.

Installation IDs and push channels originate on devices, but tags used for authorization or audience selection should be derived and validated by the authenticated backend. Deleting an installation immediately removes it from subsequent sends; cancellation cannot undo a request Azure already accepted.
