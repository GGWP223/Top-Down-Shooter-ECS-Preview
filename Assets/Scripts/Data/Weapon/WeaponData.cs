using TetraCreations.Attributes;
using UnityEngine;

namespace Data.Weapon
{
    [CreateAssetMenu(fileName = nameof(WeaponData), menuName = "Weapon/" + nameof(WeaponData))]
    public class WeaponData : ScriptableObject
    {
        [field: Title("Sounds", CustomColor.Yellow, CustomColor.Orange)]
        [field: SerializeField] public AudioClip[] ShootSounds;
    }
}