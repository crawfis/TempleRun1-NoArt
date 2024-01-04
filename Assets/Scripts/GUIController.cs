﻿using CrawfisSoftware.AssetManagement;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace CrawfisSoftware.TempleRun
{
    internal class GUIController : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;
        [SerializeField] private DistanceTracker _distanceTracker;

        private float _trackDistance;

        private Label _leftDeathDistanceLabel;
        private Label _rightDeathDistanceLabel;
        private Label _totalDistanceLabel;
        private Direction _nextTrackDirection;
        private void Awake()
        {
            var root = _uiDocument.rootVisualElement;
            _leftDeathDistanceLabel = root.Q<Label>("_leftDeathDistance");
            _rightDeathDistanceLabel = root.Q<Label>("_rightDeathDistance");
            _totalDistanceLabel = root.Q<Label>("_totalDistanceLabel" +
                "");
            EventsPublisherTempleRun.Instance.SubscribeToEvent(KnownEvents.ActiveTrackChanged, OnTrackChanged);
        }

        private void Update()
        {
            int displayDistance = (int)(_distanceTracker.DistanceTravelled + 0.5f);
            _totalDistanceLabel.text = displayDistance.ToString() + "m";
            int _distanceUntilDeath = (int)(_trackDistance - _distanceTracker.DistanceTravelled);

            _leftDeathDistanceLabel.text = (_nextTrackDirection == Direction.Right) ? "" : _distanceUntilDeath.ToString();
            _rightDeathDistanceLabel.text = (_nextTrackDirection == Direction.Left) ? "" : _distanceUntilDeath.ToString();
        }

        private void OnTrackChanged(object sender, object data)
        {
            var tuple = (ValueTuple<Direction, float>)data;
            _nextTrackDirection = tuple.Item1;
            _trackDistance += tuple.Item2;
        }
    }
}