using System;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.Components
{
    public class FootstepComponentProvider : MonoProvider<FootstepComponent> { }

    [Serializable]
    public struct FootstepComponent
    {
        public float Timing { get; set; }
        public float Delay { get; set; }
        
        public float StepDelay;
    }
}