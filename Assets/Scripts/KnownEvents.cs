namespace CrawfisSoftware.TempleRun
{
    public enum KnownEvents
    {
        LeftTurnRequested, LeftTurnSucceeded,
        RightTurnRequested, RightTurnSucceeded,
        ActiveTrackChanged, TrackSegmentCreated,
        PlayerFailed, PlayerDied,
        GameStarted, GameOver, Pause, Resume,
        CountdownStarted, CountdownTick
    };
}