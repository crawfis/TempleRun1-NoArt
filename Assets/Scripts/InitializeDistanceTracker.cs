using UnityEngine;

namespace CrawfisSoftware.TempleRun
{
    /// <summary>
    /// Simple script to create a distance tracker and assign it to the Blackboard.
    /// </summary>
    internal class InitializeDistanceTracker : MonoBehaviour
    {
        private void Start()
        {
            var distanceTracker = new DistanceTracker();
            Blackboard.Instance.DistanceTracker = distanceTracker;
        }
    }
}