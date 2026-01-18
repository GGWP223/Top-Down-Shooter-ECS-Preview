using System;
using ECS.Components;
using ECS.Events;
using ECS.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems.Input
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag, InputComponent> _playerFilter;
        private readonly EcsFilter<PlayerTag, CurrentWeaponTag> _weaponFilter;
        private readonly EcsFilter<PlayerTag, InventoryComponent> _inventoryFilter;
        
        public void Run()
        {
            Character();
            Weapon();
            Inventory();
        }

        private void Character()
        {
            if(_playerFilter.IsEmpty())
                return;
            
            ref var input = ref _playerFilter.Get2(0);
            
            input.AxisDirection = new Vector2
            (
                UnityEngine.Input.GetAxis("Horizontal"), 
                UnityEngine.Input.GetAxis("Vertical")
            );

            input.MousePosition = UnityEngine.Input.mousePosition;
        }

        private void Weapon()
        {
            if(_weaponFilter.IsEmpty())
                return;
            
            ref var entity = ref _weaponFilter.GetEntity(0);

            if (!UnityEngine.Input.GetMouseButton(0))
                return;
                
            if(entity.Has<WeaponShootEvent>())
                return;
            
            entity.Get<WeaponShootEvent>();
        }

        private void Inventory()
        {
            if(_inventoryFilter.IsEmpty())
                return;
            
            ref var entity = ref _inventoryFilter.GetEntity(0);
            ref var inventory = ref _inventoryFilter.Get2(0);

            for (var i = 1; i <= inventory.MaxSlots; i++)
            {
                if (!UnityEngine.Input.GetKeyDown(KeyCode.Alpha0 + i)) 
                    continue;
                
                entity.Get<InventoryChangeSlotEvent>().Slot = i;
            }
            
            if(UnityEngine.Input.GetKeyDown(KeyCode.F))
                entity.Get<InventoryAddEvent>();
            
            if(UnityEngine.Input.GetKeyDown(KeyCode.G))
                entity.Get<InventoryRemoveEvent>();
        }
    }
}