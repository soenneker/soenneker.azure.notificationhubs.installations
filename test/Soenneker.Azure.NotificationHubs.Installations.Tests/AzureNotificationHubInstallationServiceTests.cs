using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Azure.NotificationHubs;
using Soenneker.Azure.NotificationHubs.Installations.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Azure.NotificationHubs.Installations.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class AzureNotificationHubInstallationServiceTests : HostedUnitTest
{
    private readonly IAzureNotificationHubInstallationService _util;

    public AzureNotificationHubInstallationServiceTests(Host host) : base(host)
    {
        _util = Resolve<IAzureNotificationHubInstallationService>(true);
    }

    [Test]
    public async Task Rejects_blank_installation_id(CancellationToken cancellationToken)
    {
        Func<Task> act = async () => await _util.CreateOrUpdate(
            " ",
            NotificationPlatform.FcmV1,
            "push-channel",
            cancellationToken: cancellationToken);

        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Test]
    public async Task Rejects_empty_patch_set(CancellationToken cancellationToken)
    {
        Func<Task> act = async () => await _util.Patch(
            "installation-id",
            new List<PartialUpdateOperation>(),
            cancellationToken);

        await act.Should().ThrowAsync<ArgumentException>();
    }
}
