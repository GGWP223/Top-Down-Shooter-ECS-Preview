using Data.Weapon;
using ECS.Components;
using ECS.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems.Weapon
{
    public class WeaponHitSystem : IEcsRunSystem
    {
        private readonly EcsFilter<WeaponHitEvent> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var evnt = ref _filter.Get1(i);

                if(evnt.Entity.IsAlive() && evnt.Entity.Has<HealthComponent>())
                    evnt.Entity.Get<TakeDamageEvent>().Damage = evnt.Damage;
            }
        }
    }
}