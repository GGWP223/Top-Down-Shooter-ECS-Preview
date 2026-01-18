using Data.Character;
using ECS.Components;
using ECS.Events;
using ECS.References;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace ECS.Systems.Mob
{
    public class CharacterFootstepSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MotionComponent, FootstepComponent, TransformReference> _filter;
        
        private readonly FootstepSoundsData _data;

        public CharacterFootstepSystem
        (
            FootstepSoundsData data
        )
        {
            _data = data;
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var motion = ref _filter.Get1(i);
                ref var footstep = ref _filter.Get2(i);
                ref var transform = ref _filter.Get3(i).Transform;

                var position = transform.position + Vector3.up * 0.1f;
                var velocity = motion.RelativeDirection.magnitude;
                
                footstep.Timing += Time.deltaTime;

                if (Mathf.Abs(velocity) < 0.5f)
                    continue;
                
                footstep.Delay = footstep.StepDelay;
                
                if (footstep.Timing < footstep.Delay)
                    continue;
                
                footstep.Timing = 0;
                
                if(!Physics.Raycast(position, Vector3.down, out var hit, 0.5f))
                    continue;
            
                foreach (var data in _data.StepData)
                {
                    if ((data.Layer.value & (1 << hit.collider.gameObject.layer)) == 0)
                        continue;
                
                    ref var step = ref entity.Get<PlayAudioEvent>();

                    step.Clip = data.Clips[Random.Range(0, data.Clips.Length)];
                    step.Position = position;
                    step.Pitch = Random.Range(0.9f, 1.1f);
                    
                    break;
                }
            }
        }
    }
}