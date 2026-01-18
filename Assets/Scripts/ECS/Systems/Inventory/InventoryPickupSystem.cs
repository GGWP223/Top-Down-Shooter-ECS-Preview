using ECS.Components;
using ECS.Events;
using ECS.Tags;
using Leopotam.Ecs;

namespace ECS.Systems.Player
{
    public class InventoryPickupSystem : IEcsRunSystem
    {
        private readonly EcsFilter<NearestItemComponent, InventoryAddEvent> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var item = ref _filter.Get1(i).Entity;
                ref var evnt = ref _filter.Get2(i);
            
                evnt.Entity = item;
            }
        }
    }
}