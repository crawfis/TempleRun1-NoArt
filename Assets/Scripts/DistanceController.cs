﻿using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace CrawfisSoftware.TempleRun
{
    /// <summary>
    /// Speed controller that updates a DistanceTracker.
    ///    Dependencies: DistanceTracker and GameConfig (from Blackboard)
    ///    Subscribes: GameStarted
    ///    Subscribes: GameOver
    /// </summary>
    internal class DistanceController : MonoBehaviour
    {
        [SerializeField] private DistanceTracker _distanceTracker;
        
        private float _initialSpeed;    
        private float _maxSpeed;
        private float _acceleration;
        private float _speed;
        private Coroutine _coroutine;

        private void Awake()
        {
            _initialSpeed = Blackboard.Instance.GameConfig.InitialSpeed;
            _maxSpeed = Blackboard.Instance.GameConfig.MaxSpeed;
            _acceleration = Blackboard.Instance.GameConfig.Acceleration;
            _speed = _initialSpeed;
            EventsPublisherTempleRun.Instance.SubscribeToEvent(KnownEvents.GameStarted, OnGameStarted);
            EventsPublisherTempleRun.Instance.SubscribeToEvent(KnownEvents.GameOver, OnGameOver);
        }

        private void OnGameStarted(object sender, object data)
        {
            _coroutine = StartCoroutine(UpdateAfterGameStart());
        }

        private void OnGameOver(object sender, object data)
        {
            DeleteCoroutine();
        }

        IEnumerator UpdateAfterGameStart()
        {
            while (true)
            {
                _distanceTracker.UpdateDistance(_speed * Time.deltaTime);
                _speed += _acceleration * Time.deltaTime;
                _speed = Mathf.Clamp(_speed, _initialSpeed, _maxSpeed);
                yield return new WaitForEndOfFrame();
            }
        }

        // Typically called when a player loses a life.
        public void Reset()
        {
            _speed = _initialSpeed;
        }

        private void OnDestroy()
        {
            EventsPublisherTempleRun.Instance.UnsubscribeToEvent(KnownEvents.GameStarted, OnGameStarted);
            EventsPublisherTempleRun.Instance.UnsubscribeToEvent(KnownEvents.GameOver, OnGameOver);
            DeleteCoroutine();
        }

        private void DeleteCoroutine()
        {
            if(_coroutine != null) StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}