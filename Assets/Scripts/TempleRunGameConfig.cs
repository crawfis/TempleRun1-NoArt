using System;
using UnityEngine;

namespace CrawfisSoftware.TempleRun
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "CrawfisSoftware/TempleRun/GameConfig")]
    public class TempleRunGameConfig : ScriptableObject
    {
        [SerializeField] public float InitialSpeed = 5f;
        [SerializeField] public float MaxSpeed = 8f;
        [SerializeField] public float Acceleration = 0.2f;
        [SerializeField] public int StartRunway = 10;
        [SerializeField] public float SafeTurnDistance = 5f; 
        [SerializeField, Range(2, 92)] public int MinDistance = 3;
        [SerializeField, Range(2, 92)] public int MaxDistance = 9;
        [SerializeField] public float InputCoolDownForTurns = 1f;
    }
}