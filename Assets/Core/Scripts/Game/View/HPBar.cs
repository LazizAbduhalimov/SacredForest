using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Game.View
{
    public class HPBar : MonoBehaviour
    {
        public Image Bar;
        public TMP_Text HpAmount;

        public void Set(int currentHealth, int maximumHealth)
        {
            Bar.fillAmount = (float)currentHealth / maximumHealth;
            HpAmount.text = $"{currentHealth}/{maximumHealth}";
        }
    }
}