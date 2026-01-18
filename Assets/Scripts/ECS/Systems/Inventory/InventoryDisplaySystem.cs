using DG.Tweening;
using ECS.Components;
using ECS.Events;
using ECS.References;
using ECS.Tags;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace ECS.Systems.Player
{
    public class InventoryDisplaySystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag, NearestItemComponent, CameraReference> _playerFilter;
        private readonly EcsFilter<ItemDisplayTag, DisplayReference> _displayFilter;
        
        public void Run()
        {
            if(_playerFilter.IsEmpty())
                return;
            
            if(_displayFilter.IsEmpty())
                return;
            
            ref var weapon = ref _playerFilter.Get2(0).Entity;
            ref var camera = ref _playerFilter.Get3(0).Camera;
            ref var display = ref _displayFilter.Get2(0);
            
            if(display.Root.activeSelf != weapon.IsAlive())
                ActiveDisplay(display.Root, weapon.IsAlive());
            
            if(weapon.IsAlive())
                UpdateDisplay(weapon, camera, display);;
        }

        private void UpdateDisplay(in EcsEntity weapon, in Camera camera, in DisplayReference display)
        {
            ref var transform = ref weapon.Get<TransformReference>().Transform;
            
            display.Root.transform.LookAt(camera.transform);
            display.Root.transform.position = transform.position + Vector3.up;
            
            var name = GetName(weapon);
            
            if (display.Name.text != name)
                display.Name.text = name;
        }

        private void ActiveDisplay(GameObject root, bool show)
        {
            var scale = root.transform.localScale;
            
            if(DOTween.IsTweening(root.transform))
                return;
            
            if(show)
                root.SetActive(true);
            
            root.transform.localScale = new Vector3(scale.x, show ? 0f : 1f, scale.z);
            root.transform.DOKill();
            
            root.transform
                .DOScaleY(show ? 1f : 0f, 0.3f)
                .OnComplete(() =>
                {
                    if (!show)
                        root.SetActive(false);
                });
        }

        private string GetName(in EcsEntity entity)
        {
            if(!entity.Has<WeaponDataComponent>())
                return "???";

            ref var data = ref entity.Get<WeaponDataComponent>();
            return data.Data.name;
        }
    }
}