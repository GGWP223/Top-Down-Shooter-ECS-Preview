using System;
using TMPro;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.References
{
    public class DisplayReferenceProvider : MonoProvider<DisplayReference> { }

    [Serializable]
    public struct DisplayReference
    {
        public GameObject Root;
        public TextMeshProUGUI Name;
    }
}