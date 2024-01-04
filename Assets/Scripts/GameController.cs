using CrawfisSoftware.AssetManagement;
using System.Collections;
using UnityEngine;

namespace CrawfisSoftware.TempleRun
{
    internal class GameController : MonoBehaviour
    {
        [SerializeField] private TrackManager _trackManager;

        private void Awake()
        {
            Time.timeScale = 0.0f;
            EventsPublisherTempleRun.Instance.SubscribeToEvent(KnownEvents.PlayerFailed, OnPlayerFailed);
            EventsPublisherTempleRun.Instance.SubscribeToEvent(KnownEvents.Pause, OnPause);
            EventsPublisherTempleRun.Instance.SubscribeToEvent(KnownEvents.Resume, OnResume);
        }

        private void Start()
        {
            _ = StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            Time.timeScale = 0.0f;
            yield return new WaitForSecondsRealtime(GameConstants.ResumeDelay);
            Time.timeScale = 1.0f;
        }

        private void OnPlayerFailed(object sender, object data)
        {
            _ = StartCoroutine(StartGame());
            StartCoroutine(AdvanceTrack());
        }
        private IEnumerator AdvanceTrack()
        {
            // Wait until pause is almost over before advancing the player to the next track segment.
            yield return new WaitForSecondsRealtime(GameConstants.ZeroHoldForResumeDelay);
            _trackManager.AdvanceToNextSegment();
        }

        private void OnPause(object sender, object data)
        {
            Time.timeScale = 1.0f;
        }

        private void OnResume(object sender, object data)
        {
            Time.timeScale = 0.0f;
        }
    }
}