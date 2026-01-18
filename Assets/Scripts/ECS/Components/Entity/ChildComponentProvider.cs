using System.Collections.Generic;
using System.ComponentModel;
using Leopotam.Ecs;
using Voody.UniLeo;

namespace ECS.Components.Entity
{
    public class ChildComponentProvider : MonoProvider<ChildComponent> { }

    public struct ChildComponent
    {
        public List<EcsEntity> Entities;
    }
}