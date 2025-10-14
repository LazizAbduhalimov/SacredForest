using System;
using Game;
using UnityEngine;

namespace Client.Game
{
    [Serializable]
    public class Rush : IAbility
    {
        public void OnTurnStart(Unit character, int turnNumber, AttackContext context)
        {
            if (turnNumber == 1)
            {
                context.ExtraDamage += character.Weapon.Damage;
                DamageNumbers.Instance.SpawnNumber($"Rush! +{character.Weapon.Damage}", character.transform.position, DamageNumbers.DamageType.Buffs);
                Debug.Log($"Rush triggered! Extra damage {character.Weapon.Damage} applied.");
            }
        }
    }

    // public class 
}