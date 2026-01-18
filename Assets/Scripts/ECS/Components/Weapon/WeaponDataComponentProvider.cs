using System;
using Data.Weapon;
using Voody.UniLeo;

namespace ECS.Components
{
    public class WeaponDataComponentProvider : MonoProvider<WeaponDataComponent> { }

    [Serializable]
    public struct WeaponDataComponent
    {
        public WeaponData Data;
    }
}