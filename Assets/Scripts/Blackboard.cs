using CrawfisSoftware.Unity3D.Utility;
using UnityEngine;

namespace CrawfisSoftware.TempleRun
{
    public class Blackboard : MonoBehaviour
    {
        public static Blackboard Instance { get; private set; }
        [field: SerializeField] public RandomProviderFromList MasterRandom { get; set; }
        public TempleRunGameConfig GameConfig { get; set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
        }
    }
}