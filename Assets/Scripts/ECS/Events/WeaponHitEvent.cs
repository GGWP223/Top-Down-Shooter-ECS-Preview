using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Events
{
    public struct WeaponHitEvent
    {
        public EcsEntity Entity;
        public float Damage;
    }
}