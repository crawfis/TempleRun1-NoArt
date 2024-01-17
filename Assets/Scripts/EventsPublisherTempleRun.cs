using CrawfisSoftware.AssetManagement;

namespace CrawfisSoftware.TempleRun
{
    /// <summary>
    /// Singleton event publisher that interfaces to the CrawfisSoftware.AssetManagement.EventsPublisherEnums singleton.
    /// Avoids the problem with strings and misspelling when dealing with the EventsPublisher. Several of these could be used with
    /// different enum types for more modularity.
    /// </summary>
    internal class EventsPublisherTempleRun : EventsPublisherEnums<KnownEvents>
    {
    }
}