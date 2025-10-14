using System;
using UnityEngine;

namespace Client.Game
{
    [Serializable]
    public class BonusStamina : IAbility
    {
        public void OnObtain(Unit character)
        {
            character.Stats.SetStats(null, null, character.Stats.Stamina + 1);
            DamageNumbers.Instance.SpawnNumber("+Stamina", character.transform.position, DamageNumbers.DamageType.Heal);
            Debug.Log($"Bonus Stamina triggered! +1 Stamina applied.");
        }
    }

    // public class 
}