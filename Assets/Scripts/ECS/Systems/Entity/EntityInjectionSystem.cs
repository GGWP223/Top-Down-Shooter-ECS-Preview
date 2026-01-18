using ECS.References;
using Leopotam.Ecs;

namespace ECS.Systems.Entity
{
    public class EntityInjectionSystem : IEcsInitSystem
    {
        private readonly EcsFilter<EntityReference> _filter;
        
        public void Init()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var reference = ref _filter.Get1(i);
                
                reference.View.Entity = entity;
            }
        }
    }
}