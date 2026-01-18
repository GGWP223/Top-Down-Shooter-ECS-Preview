using System;
using Voody.UniLeo;

namespace ECS.Components
{
    public class WeaponStateComponentProvider : MonoProvider<WeaponStateComponent> { }

    [Serializable]
    public struct WeaponStateComponent
    {
        public State CurrentState { get; set; }
        
        public enum State
        {
            Idle,
            Equipped,
            Unequipped,
        }
    }
}