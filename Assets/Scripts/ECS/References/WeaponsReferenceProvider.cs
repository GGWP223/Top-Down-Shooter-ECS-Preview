using System;
using System.Collections.Generic;
using DI.Views;
using Voody.UniLeo;

namespace ECS.References
{
    public class WeaponsReferenceProvider : MonoProvider<WeaponsReference> { }

    [Serializable]
    public struct WeaponsReference
    {
        public List<EntityView> Weapons;
    }
}