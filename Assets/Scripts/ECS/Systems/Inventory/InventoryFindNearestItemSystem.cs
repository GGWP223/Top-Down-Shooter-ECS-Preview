using Data.Static;
using DI.Views;
using ECS.Components;
using ECS.References;
using ECS.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems.Player
{
    public class InventoryFindNearestItemSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<NearestItemComponent, TransformReference> _filter;

        public void Init()
        {
            foreach (var i in _filter)
            {
                ref var component = ref _filter.Get1(i);
                component.Buffer = new Collider[10];
            }
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var component = ref _filter.Get1(i);
                ref var transform = ref _filter.Get2(i).Transform;
            
                var count = Physics.OverlapSphereNonAlloc
                (
                    transform.position,
                    3f,
                    component.Buffer,
                    LayerMask.GetMask(nameof(ELayers.Pickupable))
                );

                EcsEntity bestEntity = default;
                var bestDistance = float.MaxValue;

                for (var j = 0; j < count; j++)
                {
                    ref var item = ref component.Buffer[j];
                    ref var entity = ref item.GetComponent<EntityView>().Entity;
                
                    if(!entity.Has<PickupableTag>() || entity.Has<InactiveTag>())
                        continue;
                
                    var distance = (item.transform.position - transform.position).sqrMagnitude;
                
                    if(bestDistance <= distance)
                        continue;
                
                    bestDistance = distance;
                    bestEntity = entity;
                }
            
                component.Entity = bestEntity;
            }
        }
    }
}