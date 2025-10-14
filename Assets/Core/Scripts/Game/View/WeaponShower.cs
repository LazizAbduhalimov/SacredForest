using Client.Game;
using NUnit.Framework;
using UnityEngine;

public class WeaponShower : MonoBehaviour
{
    public WeaponMb[] Weapons;

    public void Show(Weapon weapon)
    {
        WeaponMb mb = null;
        foreach (var t in Weapons)
        {
            Debug.Log("Comparing " + t.name + " with " + weapon.weaponMb.name);
            var isActive = t.name == weapon.weaponMb.name;
            if (isActive)
            {
                mb = t;
            }
            t.gameObject.SetActive(false);
        }
        mb?.gameObject.SetActive(true);
    }
}