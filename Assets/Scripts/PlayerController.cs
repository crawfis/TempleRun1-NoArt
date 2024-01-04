using System;
using UnityEngine;

namespace CrawfisSoftware.TempleRun
{
    internal class PlayerController : MonoBehaviour
    {
        [SerializeField] private DistanceTracker _distanceTracker;
        
        private float _safeTurnDistance = 1f;
        private float _trackDistance = 0;
        float _turnAvailableDistance;
        private Direction _nextTrackDirection;

        // Handle Turn request / Advance to next segment
        // Handle Collision event
        private void Awake()
        {
            EventsPublisherTempleRun.Instance.SubscribeToEvent(KnownEvents.LeftTurnRequested, OnLeftTurnRequested);
            EventsPublisherTempleRun.Instance.SubscribeToEvent(KnownEvents.RightTurnRequested, OnRightTurnRequested);
            EventsPublisherTempleRun.Instance.SubscribeToEvent(KnownEvents.ActiveTrackChanged, OnTrackChanged);
            _safeTurnDistance = Blackboard.Instance.GameConfig.SafeTurnDistance;
        }

        private void OnLeftTurnRequested(object sender, object data)
        {
            if ((_nextTrackDirection != Direction.Right) && _distanceTracker.DistanceTravelled > _turnAvailableDistance)
            {
                float distance = _distanceTracker.DistanceTravelled;
                _distanceTracker.UpdateDistance(_trackDistance - distance);
                EventsPublisherTempleRun.Instance.PublishEvent(KnownEvents.LeftTurnSucceeded, this, distance);
            }
        }

        private void OnRightTurnRequested(object sender, object data)
        {
            if ((_nextTrackDirection != Direction.Left) && _distanceTracker.DistanceTravelled > _turnAvailableDistance)
            {
                float distance = _distanceTracker.DistanceTravelled;
                _distanceTracker.UpdateDistance(_trackDistance - distance);
                EventsPublisherTempleRun.Instance.PublishEvent(KnownEvents.RightTurnSucceeded, this, distance);
            }
        }

        private void OnTrackChanged(object sender, object data)
        {
            var tuple = ((Direction direction, float segmentDistance)) data;
            _nextTrackDirection = tuple.direction;
            _trackDistance += tuple.segmentDistance;
            _turnAvailableDistance = _trackDistance - _safeTurnDistance;
        }
    }
}