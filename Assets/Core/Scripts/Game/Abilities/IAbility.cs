using Game;

namespace Client.Game
{
    public interface IAbility
    {
        public void OnAttack(Unit attacker, Unit target, AttackContext context) { }
        public void OnGetHit(Unit attacker, Unit target, AttackContext context) { }
        public void OnTurnStart(Unit character, int turnNumber, AttackContext context) { }
        public void OnObtain(Unit character) { }
    }
}