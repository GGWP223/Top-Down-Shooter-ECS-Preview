using System;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.Components
{
    public class WeaponRaycastComponentProvider : MonoProvider<WeaponRaycastComponent> { }

    [Serializable]
    public struct WeaponRaycastComponent
    {
        public GameObject Target { get; set; }
        
        public Vector3 Start { get; set; }
        public Vector3 End { get; set; }
        public Vector3 Normal { get; set; }
        public Vector3 Direction { get; set; }
        
        public float Distance { get; set; }
        
        public LayerMask CollisionMask;
    }
}