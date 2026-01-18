using ECS.Components;
using ECS.References;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems.Input
{
    public class MouseRaycastSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MouseRaycastComponent, CameraReference, InputComponent> _filter; 
        
        public void Run()
        {
            if (_filter.IsEmpty())
                return;

            ref var component = ref _filter.Get1(0);
            ref var camera = ref _filter.Get2(0).Camera;
            ref var input = ref _filter.Get3(0);

            var ray = camera.ScreenPointToRay(input.MousePosition);
            var plane = new Plane(Vector3.up, Vector3.zero);

            if (!plane.Raycast(ray, out var enter)) 
                return;
            
            var point = ray.GetPoint(enter);
            
            component.Position = new Vector3(point.x, 0, point.z);
        }

    }
}