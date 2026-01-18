using ECS.Components;
using ECS.Events;
using ECS.Systems.Entity;
using ECS.Systems.Input;
using ECS.Systems.Mob;
using ECS.Systems.Player;
using ECS.Systems.Visual;
using ECS.Systems.Weapon;
using Leopotam.Ecs;
using Voody.UniLeo;
using Zenject;

namespace DI.Installers
{
    public class SystemsInstaller : MonoInstaller
    {
        private EcsWorld _world;

        private EcsSystems _update;

        private bool _isInitialized;
        
        public override void InstallBindings()
        {
            _world = new EcsWorld();
            _update = new EcsSystems(_world);
            
            AddSystems();
            AddFrames();

            _update.ConvertScene();
            _update.Init();
            
            _isInitialized = true;
        }

        private void AddSystems()
        {
            _update
                .Add(Bind<EntityInjectionSystem>())
                .Add(Bind<ChildInjectionSystem>())
                
                .Add(Bind<MouseRaycastSystem>())
                
                .Add(Bind<PlayerInputSystem>())
                .Add(Bind<PlayerDirectionSystem>())
                .Add(Bind<PlayerRiggingSystem>())
                
                .Add(Bind<InventoryInitialSystem>())
                .Add(Bind<InventoryFindNearestItemSystem>())
                .Add(Bind<InventoryDisplaySystem>())
                .Add(Bind<InventoryPickupSystem>())
                .Add(Bind<InventoryHandlerSystem>())
                .Add(Bind<InventoryEquipSystem>())

                .Add(Bind<WeaponRaycastSystem>())
                .Add(Bind<WeaponLaserSystem>())
                .Add(Bind<WeaponShootSystem>())
                .Add(Bind<WeaponHitSystem>())
                .Add(Bind<WeaponParticleSystem>())
                .Add(Bind<WeaponSoundSystem>())
                .Add(Bind<WeaponAnimatorSystem>())
                
                .Add(Bind<CharacterMotionSystem>())
                .Add(Bind<CharacterFootstepSystem>())
                .Add(Bind<CharacterAnimatorSystem>())
                .Add(Bind<CharacterDamageSystem>())
                .Add(Bind<CharacterDestroySystem>())
                .Add(Bind<CharacterInjectWeaponSystem>())
            
                .Add(Bind<AudioSystem>())
                .Add(Bind<ParticleSystem>());
        }

        private void AddFrames()
        {
            _update
                .OneFrame<WeaponHitEvent>()
                .OneFrame<WeaponShootEvent>()
                .OneFrame<TakeDamageEvent>()
                .OneFrame<PlayAudioEvent>()
                .OneFrame<PlayParticleEvent>()
                .OneFrame<InventoryAddEvent>()
                .OneFrame<InventoryRemoveEvent>()
                .OneFrame<InventoryChangeSlotEvent>()
                .OneFrame<CreateAndPlayParticleEvent>();
        }

        private void Update()
        {
            if(!_isInitialized)
                return;
            
            _update.Run();
        }

        private T Bind<T>() where T : class
        {
            Container
                .Bind<T>()
                .AsSingle();
            
            return Container.Resolve<T>();
        }
    }
}