using System;
using System.Collections.Generic;
using ECS.Components.Entity;
using ECS.References;
using Leopotam.Ecs;

namespace ECS.Systems.Entity
{
    public class ChildInjectionSystem : IEcsInitSystem
    {
        private readonly EcsFilter<ChildComponent, ChildReference> _filter;
        
        public void Init()
        {
            foreach (var i in _filter)
            {
                ref var component = ref _filter.Get1(i);
                ref var reference = ref _filter.Get2(i);

                component.Entities = new List<EcsEntity>();

                foreach (var view in reference.Children)
                    component.Entities.Add(view.Entity);
            }
        }
    }
}