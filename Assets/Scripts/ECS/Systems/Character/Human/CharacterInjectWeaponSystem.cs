using System.Collections.Generic;
using ECS.Components;
using ECS.References;
using Leopotam.Ecs;

namespace ECS.Systems.Mob
{
    public class CharacterInjectWeaponSystem : IEcsInitSystem
    {
        private readonly EcsFilter<WeaponsComponent, WeaponsReference> _filter;
        
        public void Init()
        {
            foreach (var entity in _filter)
            {
                ref var component = ref _filter.Get1(entity);
                ref var reference = ref _filter.Get2(entity);
                
                component.Weapons = new List<EcsEntity>();

                foreach (var view in reference.Weapons)
                    component.Weapons.Add(view.Entity);
            }
        }
    }
}