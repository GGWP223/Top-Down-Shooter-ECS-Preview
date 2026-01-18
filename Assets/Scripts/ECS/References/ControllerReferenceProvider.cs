using System;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.References
{
    public class ControllerReferenceProvider : MonoProvider<ControllerReference> { }

    [Serializable]
    public struct ControllerReference
    {
        public CharacterController Controller;
    }
}