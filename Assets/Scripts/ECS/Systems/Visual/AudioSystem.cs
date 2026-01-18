using System.Collections.Generic;
using ECS.Components;
using ECS.Events;
using ECS.References;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems.Visual
{
    public class AudioSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<TransformReference, AudioSourcesReference> _poolFilter;
        private readonly EcsFilter<PlayAudioEvent> _eventFilter;

        public void Init()
        {
            if(_poolFilter.IsEmpty())
                return;

            ref var root = ref _poolFilter.Get1(0).Transform;
            ref var pool = ref _poolFilter.Get2(0);
            
            for (var i = 0; i < 10; i++)
                CreateSource(ref pool.Sources, root);
        }

        public void Run()
        {
            if(_poolFilter.IsEmpty())
                return;

            ref var root = ref _poolFilter.Get1(0).Transform;
            ref var pool = ref _poolFilter.Get2(0);

            foreach (var i in _eventFilter)
            {
                ref var audio = ref _eventFilter.Get1(i);

                var source = GetSource(ref pool, root);
                
                source.clip = audio.Clip;
                    
                if(audio.Volume != 0)
                    source.volume = audio.Volume;
                    
                if(audio.Pitch != 0)
                    source.pitch = audio.Pitch;
                    
                if(audio.MaxDistance != 0)
                    source.maxDistance = audio.MaxDistance;

                source.transform.position = audio.Position;
                    
                source.Play();
            }
        }

        private AudioSource GetSource(ref AudioSourcesReference reference, in Transform root)
        {
            foreach (var source in reference.Sources)
            {
                if(source.isPlaying)
                    continue;
                
                return source;
            }

            return CreateSource(ref reference.Sources, root);
        }

        private AudioSource CreateSource(ref List<AudioSource> sources, in Transform root)
        {
            var instance = new GameObject("Audio Source");
            var source = instance.AddComponent<AudioSource>();

            source.rolloffMode = AudioRolloffMode.Logarithmic;
            source.playOnAwake = false;
            source.spatialBlend = 1;
            source.minDistance = 1;
            source.maxDistance = 20;
            
            instance.transform.SetParent(root);
            sources.Add(source);
            
            return source;
        }
    }
}