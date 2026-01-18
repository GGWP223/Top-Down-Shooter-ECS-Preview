using System;
using UnityEngine.Animations.Rigging;
using Voody.UniLeo;

namespace ECS.References
{
    public class TwoBoneIKReferenceProvider : MonoProvider<TwoBoneIKReference> { }

    [Serializable]
    public struct TwoBoneIKReference
    {
        public TwoBoneIKConstraint Constraint;
    }
}