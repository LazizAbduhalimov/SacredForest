using UnityEngine;

namespace Client.Game
{
    [CreateAssetMenu(fileName = "New Enemy Config", menuName = "Game/Unit/EnemySO")]
    public class EnemySO : UnitSO
    {
        public Enemy Prefab;
        public Weapon RewardWeapon;
    }
}