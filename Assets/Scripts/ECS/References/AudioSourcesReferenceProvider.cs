using System;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.Components
{
    public class AudioSourcesReferenceProvider : MonoProvider<AudioSourcesReference> { }

    [Serializable]
    public struct AudioSourcesReference
    {
        public List<AudioSource> Sources;
    }
}