using System;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.References
{
    public class TransformReferenceProvider : MonoProvider<TransformReference> { }

    [Serializable]
    public struct TransformReference
    {
        public Transform Transform;
    }
}