using System;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.Components
{
    public class WeaponParamsComponentProvider : MonoProvider<WeaponParamsComponent> { }

    [Serializable]
    public struct WeaponParamsComponent
    {
        public float NextFireTime { set; get; }
        
        public float StartDamage;
        public float StartRate;
        
        public float AnimationShootDuration;

        public float Falloff;
    }
}