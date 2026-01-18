using System;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.References
{
    public class CameraReferenceProvider : MonoProvider<CameraReference> { }

    [Serializable]
    public struct CameraReference
    {
        public Camera Camera;
    }
}