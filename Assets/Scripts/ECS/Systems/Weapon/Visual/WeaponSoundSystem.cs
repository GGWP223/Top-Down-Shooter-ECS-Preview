using ECS.Components;
using ECS.Events;
using ECS.References;
using ECS.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems.Weapon
{
    public class WeaponSoundSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CurrentWeaponTag, WeaponReferences, WeaponDataComponent, WeaponShootEvent> _weaponFilter;
        
        public void Run()
        {
            foreach (var i in _weaponFilter)
            {
                ref var entity = ref _weaponFilter.GetEntity(i);
                ref var reference = ref _weaponFilter.Get2(i);
                ref var data = ref _weaponFilter.Get3(i).Data;
                
                PlaySound(entity, data.ShootSounds, reference.FirePoint.position);
            }    
        }
        
        private void PlaySound(in EcsEntity entity, in AudioClip[] sounds, in Vector3 position)
        {
            if(entity.Has<PlayAudioEvent>())
                return;
            
            ref var audio = ref entity.Get<PlayAudioEvent>();
            
            audio.Clip = sounds[Random.Range(0, sounds.Length)];
            audio.Pitch = Random.Range(0.95f, 1.05f);
            audio.Position = position;
        }
    }
}