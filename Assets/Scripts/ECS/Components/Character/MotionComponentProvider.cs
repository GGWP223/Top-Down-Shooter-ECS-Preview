using System;
using Unity.Mathematics;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.Components
{
    public class MotionComponentProvider : MonoProvider<MotionComponent> { }

    [Serializable]
    public struct MotionComponent
    {
        public Vector2 RelativeDirection { get; set; }
        public Vector2 LocalDirection { get; set; }

        public float Rotation { get; set; }
        public float Velocity { get; set; }
        
        public float Speed;
    }
}