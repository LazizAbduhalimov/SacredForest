using System;
using Game;
using UnityEngine;

namespace Client.Game
{
    [Serializable]
    public class Shield : IAbility
    {
        public void OnGetHit(Unit attacker, Unit target, AttackContext context)
        {
            if (attacker.Stats.Strength >= target.Stats.Strength) return;
            var reducedDamage = 3;
            context.ExtraDamage -= reducedDamage;
            DamageNumbers.Instance.SpawnNumber($"Shield! -{reducedDamage}", target.transform.position, DamageNumbers.DamageType.Buffs);
            Debug.Log($"Shield triggered! Damage reduced by {reducedDamage}.");
        }
    }

    // public class 
}