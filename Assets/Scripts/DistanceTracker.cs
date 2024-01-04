using UnityEngine;

namespace CrawfisSoftware.TempleRun
{
    internal class DistanceTracker : MonoBehaviour
    {
        public float DistanceTravelled { get; private set; }
        public void UpdateDistance(float deltaDistance)
        {
            DistanceTravelled += deltaDistance;
        }
    }
}