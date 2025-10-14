using Game;
using UnityEngine;

namespace Client.Game
{
    [System.Serializable]
    public class HiddenAttack : IAbility
    {
        public void OnAttack(Unit attacker, Unit target, AttackContext context)
        {
            if (attacker.Stats.Agility > target.Stats.Agility)
            {
                context.ExtraDamage += 1;
                DamageNumbers.Instance.SpawnNumber($"Hidden attack! +1", attacker.transform.position, DamageNumbers.DamageType.Buffs);
                Debug.Log($"Hidden Attack triggered! Extra damage 1 applied.");
            }
        }
    }

    // public class 
}