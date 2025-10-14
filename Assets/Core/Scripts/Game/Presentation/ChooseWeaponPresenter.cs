using System;
using Client.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ChooseWeaponPresenter
    {
        private Character _character;
        private Weapon _weapon;
        public Action<bool> OnChoose;
        private Button ChooseButton;

        public ChooseWeaponPresenter(Character character, Weapon weapon)
        {
            _character = character;
            _weapon = weapon;
        }

        public void ShowChooseButton()
        {
            UIInteractor.Instance.InteractionLayer = (int)InteractionLayerEnum.ChooseWeapon;
            var buttons = UIInteractor.Instance.Interactors[(int)InteractionLayerEnum.ChooseWeapon].Buttons;
            buttons[0].MainText.text = _weapon.name;
            buttons[0].LeftText.text = "Type: " + _weapon.DamageType;
            buttons[0].RightText.text = "Damage: " + _weapon.Damage;
            ChooseButton = buttons[0].GetComponent<Button>();
            ChooseButton.onClick.AddListener(() => { Choose(true);  });

            UIInteractor.Instance.CloseButton.onClick.AddListener(() => { Choose(false); UIInteractor.Instance.CloseButton.onClick.RemoveListener(() => { Choose(false); }); });
        }

        public void Choose(bool choose)
        {
            ChooseButton.onClick.RemoveAllListeners();
            if (choose)
            {
                _character.ChangeWeapon(_weapon);
            }

            OnChoose?.Invoke(choose);
            Debug.Log($"Player weapon: {_weapon.name}");
            UIInteractor.Instance.InteractionLayer = (int)InteractionLayerEnum.None;
        }
    }
}
