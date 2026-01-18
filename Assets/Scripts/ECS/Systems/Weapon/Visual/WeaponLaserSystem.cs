using ECS.Components;
using ECS.References;
using ECS.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems.Weapon
{
    public class WeaponLaserSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CurrentWeaponTag, WeaponRaycastComponent, LineRendererReference> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var raycast = ref _filter.Get2(i);
                ref var laser = ref _filter.Get3(i);
                
                laser.Line.SetPosition(0, raycast.Start);
                
                laser.Line.SetPosition(1, raycast.End != Vector3.zero ? 
                    raycast.End : 
                    raycast.Start + raycast.Direction * 20
                );

                laser.Sphere.SetActive(raycast.End != Vector3.zero);
                laser.Sphere.transform.position = raycast.End;
            }
        }
    }
}