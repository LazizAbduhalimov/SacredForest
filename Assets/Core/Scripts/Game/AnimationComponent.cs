using System;
using UnityEngine;

namespace Client.Game
{
    public class AnimationComponent
    {
        public Animator Animator;
        public AttackTypeAnimation AttackTypeAnimation { get; private set; }

        public AnimationComponent(Animator animator)
        {
            Animator = animator;
        }

        public void ChangeAttack(AttackTypeAnimation attackType)
        {
            AttackTypeAnimation = attackType;
            Animator.SetFloat("AttackType", (int)AttackTypeAnimation);
        }

        public void Attack()
        {
            Animator.SetTrigger("Attack");
        }

        public void Hit()
        {
            Animator.SetTrigger("Hit");
        }

        public void Death()
        {
            Animator.SetBool("IsDead",  true);
        }

        public void Dodge()
        {
            Animator.SetTrigger("Dodge");
        }

        public void Run()
        {
            Animator.SetTrigger("Run");
        }

    }

    public enum AttackTypeAnimation
    {
        Slash = 0,
        Stab = 1,
        Chop = 2,
        Stab2Hands = 3
    }
}
