using ECS.Components;
using ECS.References;
using ECS.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems.Weapon
{
    public class WeaponRaycastSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CurrentWeaponTag, WeaponReferences, WeaponRaycastComponent> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var references = ref _filter.Get2(i);
                ref var component = ref _filter.Get3(i);

                Physics.Raycast(references.FirePoint.position, references.FirePoint.forward, out var hit, 20,component.CollisionMask);
                
                component.Target = hit.collider?.gameObject;
                component.Start = references.FirePoint.position;
                component.End = hit.point;
                component.Normal = hit.normal;
                component.Direction = references.FirePoint.forward;
            }
        }
    }
}