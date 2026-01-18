using System;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.References
{
    public class LaserReferenceProvider : MonoProvider<LineRendererReference> { }

    [Serializable]
    public struct LineRendererReference
    {
        public LineRenderer Line;
        public GameObject Sphere;
    }
}