using System;
using System.Collections.Generic;
using UnityEngine;

namespace Client.Game
{
    public class UnitSO : ScriptableObject
    {
        public string Name;
        public int Health;
        public int Strength;
        public int Agility;
        public int Stamina;
        public Weapon DefaultWeapon;
        [SerializeReference] public List<IAbility> Abilities;

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = name;
            }
        }
    }
}