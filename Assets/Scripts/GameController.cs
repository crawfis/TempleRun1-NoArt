using System.Collections;
using UnityEngine;

namespace CrawfisSoftware.TempleRun
{
    /// <summary>
    /// Overall game control handling pausing, resuming and player failing.
    ///    Dependency: PlayerLifeController, EventsPublisherTempleRun
    ///    Subscribes: PlayerDied - Publishes a GameOver event
    ///    Subscribes: GameOver - abruptly ends the game
    ///    Subscribes: Pause - pauses be setting time scale to zero
    ///    Subscribes: Resume - resets the time scale to one
    /// </summary>
    internal class GameController : MonoBehaviour
    {
        private PlayerLifeController _playerLifeController; // Example of using a non-MonoBehaviour class

        private void Awake()
        {
            Time.timeScale = 0.0f;
            EventsPublisherTempleRun.Instance.SubscribeToEvent(KnownEvents.PlayerDied, OnPlayerDied);
            EventsPublisherTempleRun.Instance.SubscribeToEvent(KnownEvents.GameOver, OnGameOver);
            EventsPublisherTempleRun.Instance.SubscribeToEvent(KnownEvents.Pause, OnPause);
            EventsPublisherTempleRun.Instance.SubscribeToEvent(KnownEvents.Resume, OnResume);
        }

        private void Start()
        {
            //_playerLifeController = new PlayerLifeController(int.MaxValue, 0);
            _playerLifeController = new PlayerLifeController(3, 0);
            _ = StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            Time.timeScale = 0.0f;
            yield return new WaitForSecondsRealtime(GameConstants.ResumeDelay);
            EventsPublisherTempleRun.Instance.PublishEvent(KnownEvents.GameStarted, this, null);
            Time.timeScale = 1.0f;
        }

        private void OnPlayerDied(object sender, object data)
        {
            EventsPublisherTempleRun.Instance.PublishEvent(KnownEvents.GameOver, this, null);
        }

        private void OnPause(object sender, object data)
        {
            Time.timeScale = 0.0f;
        }

        private void OnResume(object sender, object data)
        {
            Time.timeScale = 1.0f;
        }

        private void OnGameOver(object sender, object data)
        {
            StartCoroutine(Quit());
        }

        private IEnumerator Quit()
        {
            UnsubscribeToEvents();
            // Since we are the first subscriber, let's wait for anyone else wanting to handle this event.
            // High score can log, etc. In general though, this would only be on a quit button.
            yield return new WaitForEndOfFrame();

            // For a more complex game with many scenes, we may want to access the EventsPublisher and flush all events from it (Pop).

            // This shows the proper way to quit a game both in Editor and with a build
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }

        private void UnsubscribeToEvents()
        {
            EventsPublisherTempleRun.Instance.UnsubscribeToEvent(KnownEvents.PlayerDied, OnPlayerDied);
            EventsPublisherTempleRun.Instance.UnsubscribeToEvent(KnownEvents.GameOver, OnGameOver);
            EventsPublisherTempleRun.Instance.UnsubscribeToEvent(KnownEvents.Pause, OnPause);
            EventsPublisherTempleRun.Instance.UnsubscribeToEvent(KnownEvents.Resume, OnResume);
        }
    }
}