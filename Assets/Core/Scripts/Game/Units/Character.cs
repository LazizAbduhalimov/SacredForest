namespace Client.Game
{
    public class Character : Unit
    {
        public WeaponShower WeaponHolder;

        public void ChangeWeapon(Weapon newWeapon)
        {
            WeaponHolder ??= GetComponentInChildren<WeaponShower>();
            WeaponHolder.Show(newWeapon);
            AnimationComponent.ChangeAttack(newWeapon.AttackAnimation);
            Weapon = newWeapon;
        }
    }
}