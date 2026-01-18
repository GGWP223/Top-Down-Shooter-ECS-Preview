using ECS.Components;
using ECS.Events;
using ECS.References;
using ECS.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems.Player
{
    public class InventoryEquipSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<InventoryComponent, WeaponsComponent, TransformReference> _inventoryFilter;
        private readonly EcsFilter<WeaponReferences> _weaponFilter;
        
        public void Init()
        {
            foreach (var i in _weaponFilter)
            {
                ref var entity = ref _weaponFilter.GetEntity(i);
                ref var reference = ref _weaponFilter.Get1(i);
          
                if(entity.Has<CurrentWeaponTag>())
                    continue;
                
                reference.Weapon.gameObject.SetActive(false);
            }
        }

        public void Run()
        {
            foreach (var i in _inventoryFilter)
            {
                ref var entity = ref _inventoryFilter.GetEntity(i);
                ref var inventory = ref _inventoryFilter.Get1(i);
                ref var weapons = ref _inventoryFilter.Get2(i);
                ref var transform = ref _inventoryFilter.Get3(i).Transform;
                
                if(entity.Has<InventoryRemoveEvent>())
                    DropWeapon(entity.Get<InventoryRemoveEvent>().Entity, weapons, transform);
                
                if (entity.Has<InventoryChangeSlotEvent>())
                    ChangeWeapon(inventory.CurrentWeapon, weapons);
            }
        }

        private void ChangeWeapon(in EcsEntity weapon, in WeaponsComponent weapons)
        {
            UnequipCurrentWeapon(weapons);
            EquipWeapon(weapon, weapons);
        }

        private void DropWeapon(in EcsEntity weapon, in WeaponsComponent weapons, in Transform transform)
        {
            UnequipCurrentWeapon(weapons);

            ref var references = ref weapon.Get<TransformReference>();
            references.Transform.position = transform.position;  
        }

        private void EquipWeapon(in EcsEntity weapon, in WeaponsComponent weapons)
        {
            var pickupable = weapon.Get<WeaponDataComponent>();
            
            foreach (var entity in weapons.Weapons)
            {
                ref var reference = ref entity.Get<WeaponReferences>();
                ref var data = ref entity.Get<WeaponDataComponent>().Data;
                
                if(data != pickupable.Data)
                    continue;
      
                reference.Weapon.gameObject.SetActive(true);
                entity.Get<CurrentWeaponTag>();
            }
        }

        private void UnequipCurrentWeapon(in WeaponsComponent weapons)
        {
            foreach (var entity in weapons.Weapons)
            {
                ref var reference = ref entity.Get<WeaponReferences>();
                
                if(!entity.Has<CurrentWeaponTag>())
                    continue;
                
                reference.Weapon.gameObject.SetActive(false);
                entity.Del<CurrentWeaponTag>();
            }
        }
    }
}