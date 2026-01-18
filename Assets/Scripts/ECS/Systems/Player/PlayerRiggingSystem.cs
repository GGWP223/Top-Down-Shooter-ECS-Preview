using ECS.Components;
using ECS.References;
using ECS.Tags;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems.Player
{
    public class PlayerRiggingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag, HandTag, TwoBoneIKReference> _handFilter;
        private readonly EcsFilter<PlayerTag, CurrentWeaponTag, WeaponReferences> _weaponFilter;
        private readonly EcsFilter<PlayerTag, MouseRaycastComponent> _raycastFilter;

        public void Run()
        {
            if(_weaponFilter.IsEmpty())
                return;
            
            ref var references = ref _weaponFilter.Get3(0);

            AttachHands(references);
            RotateWeapon(references);
        }

        private void RotateWeapon(in WeaponReferences references)
        {
            if(_raycastFilter.IsEmpty())
                return;
    
            ref var raycast = ref _raycastFilter.Get2(0);

            var direction = raycast.Position - references.Weapon.transform.position;

            var distance = Mathf.Sqrt(direction.x * direction.x + direction.z * direction.z);
            var angle = Mathf.Atan2(direction.y, distance) * Mathf.Rad2Deg;

            references.Weapon.localRotation = Quaternion.Euler(-angle, 0, 0);
        }

        private void AttachHands(in WeaponReferences references)
        {
            foreach (var i in _handFilter)
            {
                ref var entity = ref _handFilter.GetEntity(i);
                ref var constraint = ref _handFilter.Get3(i).Constraint;

                var position = Vector3.zero;
                var rotation = Quaternion.identity;

                if (entity.Has<RightTag>())
                {
                    position = references.PrimaryGrip.position;
                    rotation = references.PrimaryGrip.rotation;
                }
                else if (entity.Has<LeftTag>())
                {
                    position = references.SecondaryGrip.position;
                    rotation = references.SecondaryGrip.rotation;
                }
            
                constraint.data.target.transform.position = position;
                constraint.data.target.transform.rotation = rotation;
            }
        }
    }
}