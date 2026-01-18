using System.Collections.Generic;
using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems.Player
{
    public class InventoryInitialSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InventoryComponent> _inventoryFilter;
        
        public void Run()
        {
            foreach (var i in _inventoryFilter)
            {
                ref var inventory = ref _inventoryFilter.Get1(i);
                inventory.Weapons = new List<EcsEntity>();
            }
        }
    }
}