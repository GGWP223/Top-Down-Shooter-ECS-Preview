using System;
using System.Collections.Generic;
using DI.Views;
using Voody.UniLeo;

namespace ECS.Components
{
    public class ParticlesReferenceProvider : MonoProvider<ParticlesReference> { }

    [Serializable]
    public struct ParticlesReference
    {
        public List<ParticleView> Data;
    }
}