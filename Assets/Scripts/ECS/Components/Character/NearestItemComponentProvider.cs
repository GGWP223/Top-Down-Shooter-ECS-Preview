using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.Components
{
    public class NearestItemComponentProvider : MonoProvider<NearestItemComponent> { }

    public struct NearestItemComponent
    {
        public Collider[] Buffer { get; set; }
        public EcsEntity Entity;
    }
}