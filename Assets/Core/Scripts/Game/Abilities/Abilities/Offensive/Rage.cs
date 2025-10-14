using System;
using Game;
using UnityEngine;

namespace Client.Game
{
    [Serializable]
    public class Rage : IAbility
    {
        public void OnTurnStart(Unit character, int turnNumber, AttackContext context)
        {
            var extraDamage = turnNumber < 4 ? 2 : -1;
            context.ExtraDamage += extraDamage;
            DamageNumbers.Instance.SpawnNumber($"Rage! {extraDamage}", character.transform.position, DamageNumbers.DamageType.Buffs);
            Debug.Log($"Rage triggered! Extra damage {extraDamage} applied.");
        }
    }

    // public class 
}