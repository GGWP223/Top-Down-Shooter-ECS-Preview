using System;
using System.Collections.Generic;
using DI.Views;
using Voody.UniLeo;

namespace ECS.References
{
    public class ChildReferenceProvider : MonoProvider<ChildReference> { }

    [Serializable]
    public struct ChildReference
    {
        public List<EntityView> Children;
    }
}