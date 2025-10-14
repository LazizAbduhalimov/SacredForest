using System;
using UnityEngine;

namespace Client.Game
{
    [Serializable]
    public class BonusStrength : IAbility
    {
        public void OnObtain(Unit character)
        {
            character.Stats.SetStats(character.Stats.Strength + 1, null, null);
            DamageNumbers.Instance.SpawnNumber("+Strength", character.transform.position, DamageNumbers.DamageType.Heal);
            Debug.Log($"Bonus Strength triggered! +1 Strength applied.");
        }
    }
}