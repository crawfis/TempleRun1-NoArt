﻿using UnityEngine;

namespace CrawfisSoftware.TempleRun
{
    internal class DistanceController : MonoBehaviour
    {
        [SerializeField] private DistanceTracker _distanceTracker;
        
        private float _initialSpeed;    
        private float _maxSpeed;
        private float _acceleration;

        private float _speed;
        private void Awake()
        {
            _initialSpeed = Blackboard.Instance.GameConfig.InitialSpeed;
            _maxSpeed = Blackboard.Instance.GameConfig.MaxSpeed;
            _acceleration = Blackboard.Instance.GameConfig.Acceleration;
            _speed = _initialSpeed;
        }

        private void Update()
        {
            _distanceTracker.UpdateDistance(_speed * Time.deltaTime);
            _speed += _acceleration * Time.deltaTime;
            _speed = Mathf.Clamp(_speed, _initialSpeed, _maxSpeed);
        }

        public void Reset()
        {
            _speed = _initialSpeed;
        }
    }
}