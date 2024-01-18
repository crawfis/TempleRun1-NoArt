using UnityEngine;

namespace CrawfisSoftware.TempleRun
{
    /// <summary>
    /// Simple script to load a game config asset (TempleRunGameConfig) and assign it to the Blackboard.
    /// </summary>
    internal class LoadGameConfig : MonoBehaviour
    {
        [SerializeField] private TempleRunGameConfig _gameConfig;

        private void Start()
        {
            Blackboard.Instance.GameConfig = _gameConfig;
        }
    }
}