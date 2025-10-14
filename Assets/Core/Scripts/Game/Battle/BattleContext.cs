using System.Collections;
using Game;
using UnityEngine;

namespace Client.Game
{
    public class BattleContext
    {
        public Character Character;
        public Enemy Enemy;

        private int _round;
        private bool _isPlayerturn;

        public BattleContext(Character character, Enemy enemy)
        {
            Character = character;
            Enemy = enemy;
            _isPlayerturn = Character.Stats.Agility >= Enemy.Stats.Agility;
            Debug.Log($"Battle started! Is player: {_isPlayerturn}");
        }

        public void DoRound()
        {
            _round++;
            Unit firstUnit = _isPlayerturn ? Character : Enemy;
            Unit secondUnit = _isPlayerturn ? Enemy :  Character;
            firstUnit.StartCoroutine(Attack(firstUnit, secondUnit));
            _isPlayerturn = !_isPlayerturn;
        }

        private IEnumerator Attack(Unit attacker, Unit defender)
        {
            var damage = attacker.Weapon.Damage + attacker.Stats.Strength;
            var damageType = attacker.Weapon.DamageType;
            var attackContext = new AttackContext(attacker, defender, damage, damageType);
            var wholeRound = Mathf.CeilToInt(_round / 2f);

            // если будут эффеты по типу восстановление от промаха надо передумать логику
            if (!attackContext.IsMissed)
            {
                foreach (var ability in attacker.Abilities) ability.OnTurnStart(attacker, wholeRound, attackContext);
                foreach (var ability in attacker.Abilities) ability.OnAttack(attacker, defender, attackContext);
                foreach (var ability in defender.Abilities) ability.OnGetHit(attacker, defender, attackContext);
            }

            attacker.AnimationComponent.Attack();
            if (attackContext.WillDie())
                defender.AnimationComponent.Death();
            else
                defender.AnimationComponent.Hit();
            
            yield return new WaitForSeconds(0.5f);
            attackContext.ApplyDamage();
        }
    }

    public enum Turn
    {
        Player,
        Enemy
    }
}