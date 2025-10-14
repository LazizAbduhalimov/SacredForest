using System;
using Game;
using UnityEngine;

namespace Client.Game
{
    [Serializable]
    public class RockSkin : IAbility
    {
        public void OnGetHit(Unit attacker, Unit target, AttackContext context)
        {
            var reducedDamage = target.Stats.Stamina;
            context.ExtraDamage -= reducedDamage;
            DamageNumbers.Instance.SpawnNumber($"Rock skin! -{reducedDamage}", target.transform.position, DamageNumbers.DamageType.Buffs);
            Debug.Log($"Rock Skin triggered! Damage reduced by {reducedDamage}.");
        }
    }

    // public class 
}