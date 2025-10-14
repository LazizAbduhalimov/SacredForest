using System.Diagnostics;
using Client.Game.View;

namespace Client.Game
{
    // Presenter for a single Unit and its stats view
    public class UnitPresenter
    {
        private readonly Unit _unit;
        private readonly UnitStatsUI _view;
        private readonly string _unitName;

        public UnitPresenter(Unit unit, UnitStatsUI view, string unitName)
        {
            _unit = unit;
            _view = view;
            _unitName = unitName;
        }

        public void Bind()
        {
            // Initial render
            _view.SetUnitName(_unitName);
            UnityEngine.Debug.Log(_unit.Weapon.name);
            _view.SetWeaponDamage(_unit.Weapon ? _unit.Weapon.Damage : 0);
            _view.UpdateStats(_unit.Stats.Strength, _unit.Stats.Agility, _unit.Stats.Stamina);
            _view.UpdateHealth(_unit.Stats.CurrentHealth, _unit.Stats.MaxHealth);

            // Subscribe to model updates
            _unit.Stats.OnHealthChanged += OnHealthChanged;
            _unit.Stats.OnStatsChanged += OnStatsChanged;
        }

        public void Unbind()
        {
            _unit.Stats.OnHealthChanged -= OnHealthChanged;
            _unit.Stats.OnStatsChanged -= OnStatsChanged;
        }

        private void OnHealthChanged(int oldHp, int newHp)
        {
            _view.UpdateHealth(newHp, _unit.Stats.MaxHealth);
        }

        private void OnStatsChanged(int str, int agi, int sta)
        {
            _view.UpdateStats(str, agi, sta);
        }
    }
}
