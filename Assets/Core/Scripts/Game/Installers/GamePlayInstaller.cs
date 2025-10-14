using Client.Game.View;
using UnityEngine;

namespace Client.Game
{
    public class GamePlayInstaller : MonoBehaviour
    {
        public UnitStatsUI PlayerStats;
        public UnitStatsUI EnemyStats;
        
        public CharacterSO WarriorConfig;
        public CharacterSO BarbarianConfig;
        public CharacterSO BanditConfig;
        
        public  EnemySO[] EnemiesConfig;

        public void Start()
        {
            var presenter = new GamePlayPresenter(
                new CharacterSO[] { BanditConfig, WarriorConfig, BarbarianConfig,  },
                EnemiesConfig,
                PlayerStats,
                EnemyStats
            );

            presenter.Start();
        }
    }
}