using UnityEngine;
using Voody.UniLeo;

namespace ECS.Components
{
    public class PlayerInputComponentProvider : MonoProvider<InputComponent> { }

    public struct InputComponent
    {
        public Vector2 AxisDirection { get; set; }
        public Vector3 MousePosition { get; set; }
    }
}