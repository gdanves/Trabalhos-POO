using System;

namespace Fakemon
{
    class Embear : Creature
    {
        private uint m_nextMove;

        public Embear()
        {
            SetHealth(250);
            SetName("Embear");
            m_nextMove = 1;
        }

        public void CastMove(Creature target)
        {
            int damage = 0;
            string moveName = "";
            Random rand = new Random();
            if(m_nextMove == 3)
            {
                moveName = "Ember";
                damage = rand.Next(20, 40);
                m_nextMove = 1;
            }
            else
            {
                moveName = "Scratch";
                damage = rand.Next(5, 10);
                m_nextMove++;
            }

            Console.WriteLine("Embear usou {0}!", moveName);
            if(IsConfused())
            {
               SetIsConfused(false);
               Console.ForegroundColor = ConsoleColor.Cyan;
               ProcessMove(this, damage);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                ProcessMove(target, damage);
            }
        }
    }
}
