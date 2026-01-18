using System;
using DI.Views;
using Voody.UniLeo;

namespace ECS.References
{
    public class EntityReferenceProvider : MonoProvider<EntityReference> { }

    [Serializable]
    public struct EntityReference
    {
        public EntityView View;
    }
}