[![](https://img.shields.io/nuget/v/soenneker.azure.notificationhubs.installations.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.azure.notificationhubs.installations/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.azure.notificationhubs.installations/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.azure.notificationhubs.installations/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.azure.notificationhubs.installations.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.azure.notificationhubs.installations/)

# Soenneker.Azure.NotificationHubs.Installations

Installation registration and lifecycle helpers for Azure Notification Hubs.

## Install

```bash
dotnet add package Soenneker.Azure.NotificationHubs.Installations
```

## Quick start

```csharp
using Soenneker.Azure.NotificationHubs.Installations.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddAzureNotificationHubInstallationServiceAsSingleton();
```

Adds `IAzureNotificationHubInstallationService` as a singleton service.

## What you get

- `IAzureNotificationHubInstallationService` — Installation registration and lifecycle helpers for Azure Notification Hubs.
- `AzureNotificationHubInstallationServiceRegistrar` — Installation registration and lifecycle helpers for Azure Notification Hubs.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `IAzureNotificationHubInstallationService.CreateOrUpdate(installation, cancellationToken)` | Creates or updates a device installation. | A task that completes when the or update creation is complete. |
| `IAzureNotificationHubInstallationService.CreateOrUpdate(installationId, platform, pushChannel, tags, userId, templates, pushVariables, cancellationToken)` | Creates or updates a device installation from the supplied installation details. | A task that completes when the or update creation is complete. |
| `IAzureNotificationHubInstallationService.Get(installationId, cancellationToken)` | Gets a device installation. | A task whose result is the requested installation. |
| `IAzureNotificationHubInstallationService.Patch(installationId, operations, cancellationToken)` | Patches a device installation. | A task that completes when the patch operation is complete. |
| `IAzureNotificationHubInstallationService.Delete(installationId, cancellationToken)` | Deletes a device installation. | Completes when the requested deletion has finished. |
| `AzureNotificationHubInstallationServiceRegistrar.AddAzureNotificationHubInstallationServiceAsSingleton(services)` | Adds `IAzureNotificationHubInstallationService` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `AzureNotificationHubInstallationServiceRegistrar.AddAzureNotificationHubInstallationServiceAsScoped(services)` | Adds `IAzureNotificationHubInstallationService` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
