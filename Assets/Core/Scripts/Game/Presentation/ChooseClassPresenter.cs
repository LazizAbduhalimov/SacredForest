using System;
using System.Collections.Generic;
using Client.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ChooseClassPresenter
    {
        public Action<int> OnChoose;
        private readonly Character _player;
        private readonly CharacterSO[] _playerConfigs;

        public ChooseClassPresenter(CharacterSO[] playerConfigs)
        {
            _playerConfigs = playerConfigs;
        }

        public void Show(Dictionary<CharacterSO, int> classLevels)
        {
            UIInteractor.Instance.InteractionLayer = (int)InteractionLayerEnum.Upgrade;
            var buttons = UIInteractor.Instance.Interactors[(int)InteractionLayerEnum.Upgrade].Buttons;
            var anyLearned = classLevels.Count != 0;
            for (int i = 0; i < _playerConfigs.Length; i++)
            {
                int index = i;
                buttons[i].MainText.text = _playerConfigs[i].Name;
                if (classLevels.TryGetValue(_playerConfigs[i], out var level))
                {
                    buttons[i].RightText.text = $"lvl: {level}";
                }
                else
                {
                    buttons[i].RightText.text = "Not learned yet";
                }
                buttons[i].LeftText.text = !anyLearned ? $"HP: {_playerConfigs[i].Health}" : "";
                var btn = buttons[i].GetComponent<Button>();
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() => Choose(index));
            }
        }

        public void Choose(int index)
        {
            Debug.Log($"Player class: {_playerConfigs[index].Name}");
            UIInteractor.Instance.InteractionLayer = (int)InteractionLayerEnum.None;
            OnChoose?.Invoke(index);
        }
    }
}
