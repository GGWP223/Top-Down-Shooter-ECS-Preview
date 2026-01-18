using DG.Tweening;
using ECS.Components;
using ECS.Events;
using ECS.References;
using ECS.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems.Weapon
{
    public class WeaponAnimatorSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CurrentWeaponTag, WeaponParamsComponent, TransformReference>.Exclude<PlayTweenEvent> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref  _filter.GetEntity(i);
                ref var data = ref _filter.Get2(i);
                ref var transform = ref _filter.Get3(i).Transform;
                
                if(entity.Has<WeaponShootEvent>())
                    OnFire(entity, data, transform);
            }
        }

        private void OnFire(EcsEntity entity, in WeaponParamsComponent data, in Transform transform)
        {
            var duration = data.AnimationShootDuration;
            
            entity.Get<PlayTweenEvent>().Tween = transform.DOLocalMoveZ(transform.localPosition.z - 0.1f, duration)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.OutSine)
                .OnComplete(() => entity.Del<PlayTweenEvent>());
        }
    }
}