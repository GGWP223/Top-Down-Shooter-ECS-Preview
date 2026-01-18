using DI.Views;
using ECS.Components;
using ECS.Events;
using ECS.References;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems.Visual
{
    public class ParticleSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TransformReference, ParticlesReference> _poolFilter;
        private readonly EcsFilter<PlayParticleEvent> _playFilter;
        private readonly EcsFilter<CreateAndPlayParticleEvent> _createFilter;
        
        public void Run()
        {
            if(_poolFilter.IsEmpty())
                return;
            
            ref var root = ref _poolFilter.Get1(0);
            ref var pool = ref _poolFilter.Get2(0);
            
            foreach (var i in _playFilter)
            {
                ref var evnt = ref _playFilter.Get1(i);
                PlayParticle(evnt.Data);
            }

            foreach (var i in _createFilter)
            {
                ref var evnt = ref _createFilter.Get1(i);
                var instance = GetInstance(ref pool, evnt, root.Transform);
                
                instance.transform.position = evnt.Position;
                instance.transform.rotation = evnt.Rotation;
                
                PlayParticle(instance);
            }
        }

        private void PlayParticle(in ParticleView particle)
        {
            foreach (var child in particle.Particles)
                child.Play();
        }

        private ParticleView GetInstance(ref ParticlesReference pool, in CreateAndPlayParticleEvent evnt, in Transform root)
        {
            foreach (var reference in pool.Data)
            {
                if(reference.Parent != evnt.Prefab)
                    continue;
                
                if(reference.IsPlaying())
                    continue;
                
                return reference;
            }
            
            return CreateInstance(ref pool, evnt, root);
        }

        private ParticleView CreateInstance(ref ParticlesReference pool, in CreateAndPlayParticleEvent evnt, in Transform root)
        {
            var instance = Object.Instantiate(evnt.Prefab, root);
            var view = instance.GetComponent<ParticleView>();
            
            pool.Data.Add(instance);
            view.Parent = evnt.Prefab;
            
            return instance;
        }
    }
}