using System.Collections.Generic;
using UnityEngine;

namespace Client.Game
{
    public abstract class Unit : MonoBehaviour
    {
        public AnimationComponent AnimationComponent;
        public UnitStats Stats;
        public Weapon Weapon;
        public List<IAbility> Abilities = new ();

        public int Level;

        public virtual void Init()
        {
            if (Stats != null)
            {
                Stats.OnDeath += Disable;
            }
            AnimationComponent = new AnimationComponent(GetComponentInChildren<Animator>());
        }

        protected virtual void OnDisable()
        {
            if (Stats != null)
            {
                Stats.OnDeath -= Disable;
            }
        }

        private void Disable()
        {
            // gameObject.SetActive(false);
        }
    }
}