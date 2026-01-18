using ECS.Components;
using ECS.Events;
using ECS.References;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems.Mob
{
    public class CharacterAnimatorSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MotionComponent, AnimatorReference> _filter;
        
        private readonly int Y = Animator.StringToHash("Y");
        private readonly int X = Animator.StringToHash("X");
        
        private readonly int Death = Animator.StringToHash("Death");
        
        public void Run()
        {
            if(_filter.IsEmpty())
                return;
            
            ref var entity = ref _filter.GetEntity(0);
            ref var motion = ref _filter.Get1(0);
            ref var animator = ref _filter.Get2(0).Animator;
            
            OnDeath(entity, animator);
            Motion(entity, animator, motion);
        }
        
        private void OnDeath(EcsEntity entity, in Animator animator)
        {
            if(!entity.Has<DestroyEvent>())   
                return;
            
            animator.applyRootMotion = true;
            animator.SetTrigger(Death);
        }
        
        private void Motion(EcsEntity entity, in Animator animator, in MotionComponent motion)
        {
            if(entity.Has<DestroyEvent>())   
                return;
            
            animator.SetFloat(X, motion.RelativeDirection.x);
            animator.SetFloat(Y, motion.RelativeDirection.y);
        }
    }
}