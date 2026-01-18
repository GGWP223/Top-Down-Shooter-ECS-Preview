using ECS.Components.Entity;
using ECS.Events;
using Leopotam.Ecs;
using UnityEngine.TextCore.Text;

namespace ECS.Systems.Mob
{
    public class CharacterDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilter<ChildComponent, DestroyEvent> _eventFilter;
        
        public void Run()
        {
            foreach (var i in _eventFilter)
            {
                ref var entity = ref _eventFilter.GetEntity(i);
                ref var component = ref _eventFilter.Get1(i);

                foreach (var child in component.Entities)
                    child.Destroy();
                
                entity.Destroy();
            }
        }
    }
}