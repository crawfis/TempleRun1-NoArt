using CrawfisSoftware.AssetManagement;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrawfisSoftware.TempleRun
{
    public class TrackManager : MonoBehaviour
    {
        const int TrackLength = 5;
        private readonly Queue<float> _trackSegments = new(TrackLength);
        private float _startDistance = 10f;
        private float _minDistance = 3;
        private float _maxDistance = 9;

        private void Awake()
        {
            EventsPublisherTempleRun.Instance.SubscribeToEvent(KnownEvents.LeftTurnSucceeded, OnTurnSucceeded);
            EventsPublisherTempleRun.Instance.SubscribeToEvent(KnownEvents.RightTurnSucceeded, OnTurnSucceeded);
            _startDistance = Blackboard.Instance.GameConfig.StartRunway;
            _minDistance = Blackboard.Instance.GameConfig.MinDistance;
            _maxDistance = Blackboard.Instance.GameConfig.MaxDistance;
        }

        private void OnTurnSucceeded(object sender, object data)
        {
            AdvanceToNextSegment();
        }

        public void AdvanceToNextSegment()
        {
            _ = _trackSegments.Dequeue();
            AddTrackSegment();
            EventsPublisherTempleRun.Instance.PublishEvent(KnownEvents.ActiveTrackChanged, this, (GetNewDirection(), _trackSegments.Peek()));
        }

        private Direction GetNewDirection()
        {
            var random = Blackboard.Instance.MasterRandom.RandomGenerator;
            float randomValue = (float) random.NextDouble();
            switch (randomValue)
            {
                case < 0.4f:
                    return Direction.Left;
                case < 0.8f:
                    return Direction.Right;
                default:
                    return Direction.Both;
            }
        }

        private void Start()
        {
            _maxDistance = Mathf.Max(_minDistance, _maxDistance);
            _trackSegments.Enqueue(_startDistance);
            for (int i = 1; i < TrackLength; i++)
            {
                AddTrackSegment();
            }
            EventsPublisherTempleRun.Instance.PublishEvent(KnownEvents.ActiveTrackChanged, this, (Direction.Left, _trackSegments.Peek()));
        }

        private void AddTrackSegment()
        {
            float segmentLength = (float) Blackboard.Instance.MasterRandom.RandomGenerator.NextDouble() * (_maxDistance - _minDistance) + _minDistance;
            _trackSegments.Enqueue(segmentLength);
        }
    }
}