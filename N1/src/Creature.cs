using System;

namespace Fakemon
{
    class Creature
    {
        private int m_health;
        private string m_name = "Unknown";
        private bool m_confused = false;

        public void AddHealth(int health)
        {
            m_health += health;
        }

        public void SetHealth(int health)
        {
            m_health = health;
        }

        public int GetHealth()
        {
            return m_health;
        }

        public void SetName(string name)
        {
            m_name = name;
        }

        public string GetName()
        {
            return m_name;
        }

        public void SetIsConfused(bool confused)
        {
            m_confused = confused;
        }

        public bool IsConfused()
        {
            return m_confused;
        }

        protected void ProcessMove(Creature target, int damage, int heal = 0)
        {
            string name = GetName();
            if(damage > 0)
            {
                Console.WriteLine("{0} causou {1} de dano a {2}.", name, damage, target.GetName());
                target.AddHealth(-damage);
            }
            if(heal > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("{0} se curou em {1} pontos.", name, heal);
                AddHealth(heal);
            }
            Console.ResetColor();
        }
    }
}
