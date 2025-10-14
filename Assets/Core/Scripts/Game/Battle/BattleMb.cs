using UnityEngine;

namespace Client.Game
{
    public class BattleMb : MonoBehaviour
    {
        public BattleContext BattleContext;
        private Character _player;
        private Enemy _enemy;

        public bool IsBattling { get; private set; }
        private bool IsStartedThisFrame;
        private const float _timeBtwRounds = 1.25f;
        private float _passedTime = 0f;

        public void StartBattle(Character character, Enemy enemy)
        {
            _player = character;
            _enemy = enemy;
            BattleContext = new BattleContext(character, enemy);
            IsBattling = true;
            IsStartedThisFrame = true;
            _passedTime = 0f;
        }

        public void EndBattle()
        {
            IsBattling = false;
            BattleContext = null;
        }

        private void Update()
        {
            if (IsStartedThisFrame)
            {
                IsStartedThisFrame = false;
                return;
            }
            if (!IsBattling || _player == null || _enemy == null) return;

            _passedTime += Time.deltaTime;

            if (_passedTime >= _timeBtwRounds)
            {
                BattleContext.DoRound();
                _passedTime -= _timeBtwRounds;
            }
        }
    }
}