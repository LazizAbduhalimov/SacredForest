using UnityEngine;
using Client.Game;

namespace Game
{
    public class AttackContext
    {
        public Unit Attacker;
        public Unit Target;
        public int BaseDamage;
        public int ExtraDamage;
        public float DamageMultiplier = 1f;
        public int TotalDamage => Mathf.RoundToInt((BaseDamage + ExtraDamage) * DamageMultiplier);
        public bool IsMissed;
        public DamageType DamageType;

        public AttackContext(Unit attacker, Unit target, int baseDamage, DamageType damageType)
        {
            Attacker = attacker;
            Target = target;
            BaseDamage = baseDamage;
            DamageType = damageType;
            var agilitySum = attacker.Stats.Agility + target.Stats.Agility;
            // если не доабвлять 1 то возмодно бесконенчое промахивание
            IsMissed = Random.Range(1, agilitySum + 1) <= target.Stats.Agility;
        }

        public void ApplyDamage()
        {
            if (IsMissed)
            {
                Debug.Log($"Unit {Attacker.name} missed {Target.name}");
                DamageNumbers.Instance.SpawnNumber("Miss!", Target.transform.position, DamageNumbers.DamageType.Missed);
                return;
            }

            Target.Stats.TakeDamage(TotalDamage);
            DamageNumbers.Instance.SpawnNumber(TotalDamage, Target.transform.position);
        }

        public bool WillDie()
        {
            return !IsMissed && TotalDamage >= Target.Stats.CurrentHealth;
        }
    }
}
