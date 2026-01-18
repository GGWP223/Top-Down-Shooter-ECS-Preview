using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using Voody.UniLeo;

namespace ECS.Components
{
    public class InventoryComponentProvider : MonoProvider<InventoryComponent> { }

    [Serializable]
    public struct InventoryComponent
    {
        public EcsEntity CurrentWeapon { get; set; }
        public List<EcsEntity> Weapons { get; set; }
        
        public int MaxSlots;
    }
}