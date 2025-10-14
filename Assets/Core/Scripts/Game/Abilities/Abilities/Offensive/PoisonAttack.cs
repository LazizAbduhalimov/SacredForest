using System;
using Game;
using UnityEngine;

namespace Client.Game
{
    [Serializable]
    public class PoisonAttack : IAbility
    {
        public void OnTurnStart(Unit attacker, int turnNumber, AttackContext context)
        {
            if (turnNumber < 2) return;
            var extraDamage = turnNumber - 1;   
            context.ExtraDamage += extraDamage;
            DamageNumbers.Instance.SpawnNumber($"Poison! +{extraDamage}", attacker.transform.position, DamageNumbers.DamageType.Buffs);
            Debug.Log($"Poison Attack triggered! Extra damage {extraDamage} applied.");
        }
    }

    // public class 
}