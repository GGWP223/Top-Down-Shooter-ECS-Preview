using DI.Views;
using UnityEngine;

namespace ECS.Events
{
    public struct CreateAndPlayParticleEvent
    {
        public ParticleView Prefab;
        
        public Vector3 Position;
        public Quaternion Rotation;
    }
}