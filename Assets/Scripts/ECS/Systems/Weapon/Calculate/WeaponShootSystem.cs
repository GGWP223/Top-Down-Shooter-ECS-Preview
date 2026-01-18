using DI.Views;
using ECS.Components;
using ECS.Events;
using ECS.References;
using ECS.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems.Weapon
{
    public class WeaponShootSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CurrentWeaponTag, WeaponParamsComponent, WeaponRaycastComponent, WeaponStateComponent, WeaponShootEvent> _weaponFilter;
        
        public void Run()
        {
            foreach (var i in _weaponFilter)
            {
                ref var entity = ref _weaponFilter.GetEntity(i);
                
                ref var data = ref _weaponFilter.Get2(i);
                ref var raycast = ref _weaponFilter.Get3(i);
                ref var state = ref _weaponFilter.Get4(i);
                
                if(state.CurrentState != WeaponStateComponent.State.Idle)
                    continue;

                var time = Time.time;

                if (time < data.NextFireTime)
                {
                    entity.Del<WeaponShootEvent>();
                    continue;
                }
                
                TryHit(entity, raycast, data);
                data.NextFireTime = time + 1f / data.StartRate;
            }
        }

        private void TryHit(in EcsEntity entity, in WeaponRaycastComponent raycast, in WeaponParamsComponent data)
        {
            if(raycast.Target is null)
                return;
            
            ref var hit = ref entity.Get<WeaponHitEvent>();
                    
            hit.Damage = data.StartDamage / (1f + data.Falloff * raycast.Distance);
            
            if(raycast.Target is null)
                return;
            
            if(raycast.Target.TryGetComponent<EntityView>(out var view))
                hit.Entity = view.Entity;
        }
    }
}