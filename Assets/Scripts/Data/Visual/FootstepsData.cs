using System;
using UnityEngine;

namespace Data.Character
{
    [CreateAssetMenu(fileName = nameof(FootstepSoundsData), menuName = "Effects/" + nameof(FootstepSoundsData))]
    public class FootstepSoundsData : ScriptableObject
    {
        [field: SerializeField] public Data[] StepData { get; private set; }
        
        [Serializable]
        public struct Data
        {
            public AudioClip[] Clips;
            public LayerMask Layer;
        }
    }
}   