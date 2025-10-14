using System;
using UnityEngine;

namespace Client.Game
{
    [Serializable]
    public class BonusAgility : IAbility
    {
        public void OnObtain(Unit character)
        {
            character.Stats.SetStats(null, character.Stats.Agility + 1, null);
            DamageNumbers.Instance.SpawnNumber("+Agility", character.transform.position, DamageNumbers.DamageType.Heal);
            Debug.Log($"Bonus Agility triggered! +1 Agility applied.");
        }
    }

    // public class 
}