using System;

namespace MathMagic
{
    abstract class Creature
    {
        private int m_health;

        public void SetHealth(int health)
        {
            m_health = health;
        }

        public int GetHealth()
        {
            return m_health;
        }

        public bool IsDead()
        {
            return m_health <= 0;
        }

        public void Hit(int value)
        {
            m_health -= value;
            ShowDamage(value);
        }

        protected abstract void ShowDamage(int value);
    }
}
