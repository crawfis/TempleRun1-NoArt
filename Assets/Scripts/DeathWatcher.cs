using CrawfisSoftware.AssetManagement;
using System;
using UnityEngine;

namespace CrawfisSoftware.TempleRun
{
    internal class DeathWatcher : MonoBehaviour
    {
        [SerializeField] private DistanceTracker _distanceTracker;

        private float _currentSegmentDistance = 0f;
        private bool _isRunning = false;

        private void Awake()
        {
            EventsPublisherTempleRun.Instance.SubscribeToEvent(KnownEvents.ActiveTrackChanged, OnTrackChanged);
        }

        private void Update()
        {
            if (_isRunning && _distanceTracker.DistanceTravelled >= _currentSegmentDistance)
            {
                _isRunning = false;
                Debug.Log(string.Format("Player Died at Distance: {0}", (int)_currentSegmentDistance));
                EventsPublisherTempleRun.Instance.PublishEvent(KnownEvents.PlayerFailed, this, _distanceTracker.DistanceTravelled);
            }
        }

        private void OnTrackChanged(object sender, object data)
        {
            _isRunning = true;
            var tuple = (ValueTuple<Direction, float>)data;
            _currentSegmentDistance += tuple.Item2;
        }
    }
}