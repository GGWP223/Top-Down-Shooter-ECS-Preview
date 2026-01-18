using System;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.References
{
    public class AnimatorReferenceProvider : MonoProvider<AnimatorReference> { }

    [Serializable]
    public struct AnimatorReference
    {
        public Animator Animator;
    }
}