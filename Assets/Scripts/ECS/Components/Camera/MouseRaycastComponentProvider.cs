using System;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.Components
{
    public class MouseRaycastComponentProvider : MonoProvider<MouseRaycastComponent> { }
    
    [Serializable]
    public struct MouseRaycastComponent
    {
        public Vector3 Position { get; set; }
        public LayerMask CollisionLayer;
    }
}