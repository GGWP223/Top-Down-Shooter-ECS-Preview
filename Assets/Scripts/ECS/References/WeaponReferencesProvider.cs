using System;
using DI.Views;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.References
{
    public class WeaponReferencesProvider : MonoProvider<WeaponReferences> { }

    [Serializable]
    public struct WeaponReferences
    {
        public Transform Weapon;
        public Transform FirePoint;
        public Transform PrimaryGrip;
        public Transform SecondaryGrip;
        public ParticleView MuzzleFlash;
    }
}