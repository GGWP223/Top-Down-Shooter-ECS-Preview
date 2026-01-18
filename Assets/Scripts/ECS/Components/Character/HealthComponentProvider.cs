using System;
using System.ComponentModel;
using Voody.UniLeo;

namespace ECS.Components
{
    public class HealthComponentProvider : MonoProvider<HealthComponent> { }

    [Serializable]
    public struct HealthComponent
    {
        public float Health;
    }
}