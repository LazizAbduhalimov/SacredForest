using UnityEngine;

namespace Client.Game
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Game/Weapon")]
    public class Weapon : ScriptableObject
    {
        public int Damage;
        public DamageType DamageType;
        public WeaponMb weaponMb;
        public AttackTypeAnimation AttackAnimation;
    }
}