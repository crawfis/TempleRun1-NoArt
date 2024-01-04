using CrawfisSoftware.AssetManagement;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrawfisSoftware.TempleRun
{
    public enum KnownEvents { LeftTurnRequested, LeftTurnSucceeded, RightTurnRequested, RightTurnSucceeded, ActiveTrackChanged, PlayerFailed, GameStarted, GameOver, Pause, Resume, CountdownStarted, CountdownTick };
    internal class EventsPublisherTempleRun : MonoBehaviour
    {
        public static EventsPublisherTempleRun Instance { get; private set; }
        public static Dictionary<KnownEvents, string> KnownEventsMap { get; private set; } = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
            foreach (KnownEvents eventEnum in Enum.GetValues(typeof(KnownEvents)))
            {
                string eventName = eventEnum.ToString();
                KnownEventsMap.Add(eventEnum, eventName);
                EventsPublisher.Instance.RegisterEvent(eventName);
            }

        }

        public void PublishEvent(KnownEvents eventEnum, object sender, object data)
        {
            EventsPublisher.Instance.PublishEvent(eventEnum.ToString(), sender, data);
        }

        public void SubscribeToEvent(KnownEvents eventEnum, Action<object, object> callback)
        {
            EventsPublisher.Instance.SubscribeToEvent(eventEnum.ToString(), callback);
        }
    }
}