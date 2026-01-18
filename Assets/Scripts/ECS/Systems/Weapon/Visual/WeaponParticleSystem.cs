using Data.Weapon;
using DI.Views;
using ECS.Components;
using ECS.Events;
using ECS.References;
using ECS.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems.Weapon
{
    public class WeaponParticleSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CurrentWeaponTag, WeaponRaycastComponent, WeaponReferences, WeaponShootEvent> _weaponFilter;
        
        private readonly ImpactData _data;
        
        public WeaponParticleSystem
        (
            ImpactData data
        )
        {
            _data = data;
        }
        
        public void Run()
        {
            foreach (var i in _weaponFilter)
            {
                ref var entity = ref _weaponFilter.GetEntity(i);
                ref var raycast = ref _weaponFilter.Get2(i);
                ref var references = ref _weaponFilter.Get3(i);
                
                PlayParticle(entity, references.MuzzleFlash);
                CreateImpact(raycast, entity);
            }
        }
        
        private void PlayParticle(in EcsEntity entity, in ParticleView particle)
        {
            if(entity.Has<PlayParticleEvent>())
                return;
            
            ref var evnt = ref entity.Get<PlayParticleEvent>();
            evnt.Data = particle;
        }
        
        private void CreateImpact(in WeaponRaycastComponent raycast, in EcsEntity entity)
        {
            foreach (var data in _data.Impacts)
            {
                if ((data.Mask.value & (1 << raycast.Target.gameObject.layer)) == 0)
                    continue;

                ref var particle = ref entity.Get<CreateAndPlayParticleEvent>();

                particle.Prefab = data.Prefab;
                
                particle.Position = raycast.End;
                particle.Rotation = Quaternion.LookRotation(raycast.Normal);
            }
        }
    }
}