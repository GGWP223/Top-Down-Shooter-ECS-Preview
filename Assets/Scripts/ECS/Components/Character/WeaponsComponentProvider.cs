using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using Voody.UniLeo;

namespace ECS.Components
{
    public class WeaponsComponentProvider : MonoProvider<WeaponsComponent> { }

    [Serializable]
    public struct WeaponsComponent
    {
        public List<EcsEntity> Weapons;
    }
}