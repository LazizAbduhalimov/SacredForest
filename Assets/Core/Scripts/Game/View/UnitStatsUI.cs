using System;
using TMPro;
using UnityEngine;

namespace Client.Game.View
{
    public class UnitStatsUI : MonoBehaviour
    {
        public TMP_Text UnitName;
        public TMP_Text WeaponDamage;
        
        public TMP_Text Strength;
        public TMP_Text Agility;
        public TMP_Text Stamina;
        public HPBar HPBar;

        public void SetUnitName(string unitName)
        {
            UnitName.text = unitName;
        }

        public void UpdateStats(int strength, int agility, int stamina)
        {
            Strength.text = strength.ToString();
            Agility.text = agility.ToString();
            Stamina.text = stamina.ToString();
        }

        public void UpdateHealth(int currentHealth, int maxHealth)
        {
            HPBar.Set(currentHealth, maxHealth);
        }

        public void SetWeaponDamage(int damage)
        {
            WeaponDamage.text = damage.ToString();
        }
    }
}