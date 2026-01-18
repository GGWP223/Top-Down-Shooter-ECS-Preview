using System;
using DI.Views;
using UnityEngine;

namespace Data.Weapon
{
    [CreateAssetMenu(fileName = nameof(ImpactData), menuName = "Effects/" + nameof(ImpactData))]
    public class ImpactData : ScriptableObject
    {
        [field: SerializeField] public Impact[] Impacts { get; private set; }

        [Serializable]
        public struct Impact
        {
            public ParticleView Prefab;
            public LayerMask Mask;
        }
    }
}