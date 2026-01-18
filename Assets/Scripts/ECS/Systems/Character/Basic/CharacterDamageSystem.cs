using ECS.Components;
using ECS.Events;
using ECS.References;
using Leopotam.Ecs;

namespace ECS.Systems.Mob
{
    public class CharacterDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TransformReference, HealthComponent, TakeDamageEvent> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                
                ref var health = ref _filter.Get2(i);
                ref var damage = ref _filter.Get3(i);
                
                health.Health -= damage.Damage;
        
                if(health.Health > 0)
                    return;
                
                entity.Get<DestroyEvent>();
            }
        }
    }
}