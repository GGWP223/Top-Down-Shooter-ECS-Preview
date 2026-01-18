using ECS.Components;
using ECS.References;
using ECS.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems.Mob
{
    public class CharacterMotionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ControllerReference, MotionComponent> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var controller = ref _filter.Get1(i).Controller;
                ref var motion = ref _filter.Get2(i);

                if (controller.isGrounded)
                    motion.Velocity = 0;
                else
                    motion.Velocity -= 9.8f * Time.deltaTime;
                
                var direction = new Vector3(motion.LocalDirection.x, 0, motion.LocalDirection.y);
                var rotation = Quaternion.Euler(0, motion.Rotation, 0);
                
                var angle = motion.Rotation * Mathf.Deg2Rad;
                var cos = Mathf.Cos(angle);
                var sin = Mathf.Sin(angle);
            
                motion.RelativeDirection = new Vector2
                (
                    direction.x * cos - motion.LocalDirection.y * sin,
                    direction.x * sin + motion.LocalDirection.y * cos
                );
                
                direction.Normalize();
                motion.RelativeDirection.Normalize();
                
                direction.y = motion.Velocity;
                
                controller.Move(direction * motion.Speed * Time.deltaTime);
                controller.transform.rotation = rotation;
            }
        }
    }
}