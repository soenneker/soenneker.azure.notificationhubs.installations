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
    public void Default()
    {

    }
}
