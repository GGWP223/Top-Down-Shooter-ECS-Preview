using System.Collections.Generic;
using ECS.Components;
using ECS.Events;
using ECS.References;
using ECS.Tags;
using Leopotam.Ecs;
using ModestTree;
using UnityEngine;

namespace ECS.Systems.Player
{
    public class InventoryHandlerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InventoryComponent> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var inventory = ref _filter.Get1(i);
                
                if(entity.Has<InventoryAddEvent>())
                {
                    Debug.Log(entity.Get<InventoryAddEvent>().Entity);
                    AddItem(entity, inventory);
                    ChangeToLastWeapon(entity, inventory);
                }
                
                if(entity.Has<InventoryRemoveEvent>())
                {
                    RemoveItem(entity, ref inventory);
                    ChangeToLastWeapon(entity, inventory);
                }
            
                if(entity.Has<InventoryChangeSlotEvent>())
                    ChangeItem(entity, ref inventory);
            }
        }

        private void ChangeItem(in EcsEntity player, ref InventoryComponent inventory)
        {
            var evnt = player.Get<InventoryChangeSlotEvent>();
            var len = inventory.Weapons.Count;
            
            Debug.Log("change item");
            
            if(len < evnt.Slot || inventory.Weapons[evnt.Slot - 1] == inventory.CurrentWeapon)
            {
                player.Del<InventoryChangeSlotEvent>();
                return;
            }
            
            Debug.Log("changed item");

            inventory.CurrentWeapon = inventory.Weapons[evnt.Slot - 1];
        }

        private void AddItem(in EcsEntity player, in InventoryComponent inventory)
        {
            ref var weapon = ref player.Get<InventoryAddEvent>().Entity;
            Debug.Log(weapon);
            
            if(!weapon.IsAlive())
            {
                player.Del<InventoryAddEvent>();
                return;
            }
            
            Debug.Log("added item");
            
            inventory.Weapons.Add(weapon);
            
            weapon.Get<InactiveTag>();
            weapon.Get<TransformReference>().Transform.gameObject.SetActive(false);
        }

        private void RemoveItem(in EcsEntity player, ref InventoryComponent inventory)
        {
            ref var evnt = ref player.Get<InventoryRemoveEvent>();
            var weapon = inventory.CurrentWeapon;
            
            if(!weapon.IsAlive() || inventory.Weapons.Count <= 1)
            {
                player.Del<InventoryRemoveEvent>();
                return;
            }
            
            weapon.Del<InactiveTag>();
            weapon.Get<TransformReference>().Transform.gameObject.SetActive(true);
            
            evnt.Entity = weapon;
            inventory.Weapons.Remove(weapon);
        }

        private void ChangeToLastWeapon(in EcsEntity player, in InventoryComponent inventory)
        {
            if(inventory.CurrentWeapon.IsAlive())
                player.Get<InventoryChangeSlotEvent>().Slot = inventory.Weapons.Count;
        }
    }
}