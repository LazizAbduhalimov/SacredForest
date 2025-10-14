using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Client.Game.View;
using Game;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Client.Game
{
    public class GamePlayPresenter
    {
        private CharacterSO _playerConfig;
        private EnemySO _currentEnemyConfig;

        private readonly EnemySO[] _enemiesConfig;
        private readonly CharacterSO[] _playerConfigs;

        private readonly UnitStatsUI _playerView;
        private readonly UnitStatsUI _enemyView;

        private UnitPresenter _playerPresenter;
        private UnitPresenter _enemyPresenter;
        private BattleMb _battleMb;
        
        private Character _player;
        private Enemy _enemy;

        private UIInteractor _uiInteractor;
        private int _enemiesDefeated = 0;
        private List<IAbility> _pendingAbilities = new();

        private Dictionary<CharacterSO, int> _classLevels = new();

        public GamePlayPresenter(
            CharacterSO[] playerConfigs,
            EnemySO[] enemiesConfig,
            UnitStatsUI playerView,
            UnitStatsUI enemyView)
        {
            _playerConfigs = playerConfigs;
            _enemiesConfig = enemiesConfig;
            _playerView = playerView;
            _enemyView = enemyView;
            _battleMb = new GameObject("BattleContext").AddComponent<BattleMb>();
            _uiInteractor = Object.FindFirstObjectByType<UIInteractor>();
        }

        public void Start()
        {
            var p = new ChooseClassPresenter(_playerConfigs);
            p.Show(_classLevels);
            p.OnChoose += (index) =>
            {
                _playerConfig = _playerConfigs[index];
                p.OnChoose = null;

                AddClassLevel(_playerConfig);
                StartInternal();
            };
        }

        private void AddClassLevel(CharacterSO config)
        {
            if (!_classLevels.TryAdd(config, 1))
                _classLevels[config]++;

            _pendingAbilities.Add(config.Abilities[_classLevels[config] - 1]);
            
            if (_player)
                LearAbilities(_player);
            Debug.Log(_pendingAbilities.Count);
        }

        private void LearAbilities(Character character)
        {
            if (_pendingAbilities == null || _pendingAbilities.Count == 0) return;
            foreach (var ability in _pendingAbilities)
            {
                character.Abilities.Add(ability);
                ability.OnObtain(character);
            }
            _pendingAbilities.Clear();
        }

        public void StartInternal()
        {
            _player = CreatePlayer();
            _enemy = CreateEnemy();
            _player.ChangeWeapon(_player.Weapon);
            BindViews();
            StartBattle();
        }

        private void BindViews()
        {
            _playerPresenter = new UnitPresenter(_player, _playerView, _playerConfig.Name);
            _enemyPresenter = new UnitPresenter(_enemy, _enemyView, _currentEnemyConfig.Name);
            _playerPresenter.Bind();
            _enemyPresenter.Bind();
        }

        private void UnbindViews()
        {
            _playerPresenter.Unbind();
            _enemyPresenter.Unbind();
        }

        private void StartBattle()
        {
            LearAbilities(_player);
            _player.Stats.OnDeath += OnPlayerDead;
            _enemy.Stats.OnDeath += OnEnemyDeath;
            _player.transform.rotation = Quaternion.LookRotation(_enemy.transform.position - _player.transform.position);
            _enemy.transform.rotation = Quaternion.LookRotation(_player.transform.position - _enemy.transform.position);
            // Start battle context
            _battleMb.StartBattle(_player, _enemy);
        }

        private void StopBattle()
        {
            _player.Stats.OnDeath -= OnPlayerDead;
            _enemy.Stats.OnDeath -= OnEnemyDeath;
            _battleMb.EndBattle();
            UnbindViews();
        }

        private void OnEnemyDeath()
        {
            _enemiesDefeated++;
            StopBattle();
            _player.StartCoroutine(ShowChoices());
        }

        private IEnumerator ShowChoices()
        {
            yield return new WaitForSeconds(1.5f); 
            _enemy.gameObject.SetActive(false);
            if (_enemiesDefeated < 5)
            {
                if (_classLevels.Values.Sum() < 3)
                    ShowUpgradeChoice();
                else
                    ShowWeaponChoice();
            }
            else
            {
                ShowWinButton();
            }
        }

        public void ShowUpgradeChoice()
        {
            var upgrade = new ChooseClassPresenter(_playerConfigs);
            upgrade.Show(_classLevels);
            upgrade.OnChoose += (index) =>
            {
                AddClassLevel(_playerConfigs[index]);
                var upgradeLvl = _classLevels[_playerConfigs[index]];
                Debug.Log($"Player class: {_playerConfigs[index].Name} upgraded to level {upgradeLvl}");
                _player.Stats.AddMaxHealth(_player.Stats.Stamina);
                upgrade.OnChoose = null;
                ShowWeaponChoice();
            };
        }

        private void OnPlayerDead()
        {
            StopBattle();
            _player.AnimationComponent.Death();
            _player.StartCoroutine(ShowRestartButton());
        }

        private void ShowWeaponChoice()
        {
            var show = new ChooseWeaponPresenter(_player, _currentEnemyConfig.RewardWeapon);
            show.ShowChooseButton();
            show.OnChoose += (chosen) =>
            {
                DamageNumbers.Instance.SpawnNumber("Healed!", _player.transform.position, DamageNumbers.DamageType.Heal);
                _player.Stats.FullHeal();
                _enemy = CreateEnemy();
                BindViews();
                StartBattle();
                show.OnChoose = null;
            };
        }

        private IEnumerator ShowRestartButton()
        {
            yield return new WaitForSeconds(1f);
            _uiInteractor.InteractionLayer = 2;
        }
        private void ShowWinButton()
        {
            _uiInteractor.InteractionLayer = 3;
        }

        private Enemy CreateEnemy()
        {
            // чтобы голем и дракон сразу не выпадали
            _currentEnemyConfig = _enemiesDefeated < 2 ? _enemiesConfig[..^2].GetRandomElement() : _enemiesConfig.GetRandomElement();
            var enemy = Object.Instantiate(_currentEnemyConfig.Prefab, _battleMb.transform);
            enemy.name = "Enemy";
            enemy.transform.position = Vector3.right * 1.5f;
            enemy.Stats = new UnitStats(_currentEnemyConfig.Health,
                                        _currentEnemyConfig.Strength,
                                        _currentEnemyConfig.Agility,
                                        _currentEnemyConfig.Stamina);

            enemy.Weapon = _currentEnemyConfig.DefaultWeapon;
            enemy.Init();

            foreach (var ability in _currentEnemyConfig.Abilities)
            {
                enemy.Abilities.Add(ability);
            }

            return enemy;
        }

        private Character CreatePlayer()
        {
            var player = Object.Instantiate(_playerConfig.Pregab, _battleMb.transform);
            player.name = "Player";
            player.transform.position = Vector3.left * 1.5f;
            player.Stats = new UnitStats(
                _playerConfig.Health,
                Random.Range(1, 4),
                Random.Range(1, 4),
                Random.Range(1, 4));
            player.Weapon = _playerConfig.DefaultWeapon;
            player.Init();

            return player;
        }
    }
}
