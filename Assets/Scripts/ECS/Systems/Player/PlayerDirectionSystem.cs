using ECS.Components;
using ECS.References;
using ECS.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems.Player
{
    public class PlayerDirectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag, MotionComponent, MouseRaycastComponent, InputComponent, TransformReference> _filter;
        
        public void Run()
        {
            if(_filter.IsEmpty())
                return;
            
            ref var motion = ref _filter.Get2(0);
            ref var mouse = ref _filter.Get3(0);
            ref var input = ref _filter.Get4(0);
            ref var transform = ref _filter.Get5(0).Transform;
            
            var direction = mouse.Position - transform.position;

            motion.LocalDirection = input.AxisDirection;
            
            motion.Rotation = Mathf.LerpAngle
            (
                motion.Rotation, 
                Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg, 10 * Time.deltaTime
            );
        }
    }
}