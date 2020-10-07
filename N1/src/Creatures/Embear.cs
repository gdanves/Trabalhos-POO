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
            m_nextMove = 0;
        }

        public void CastMove(Creature target)
        {
            m_nextMove++;
            if(m_nextMove >= 4)
                m_nextMove = 1;

            int damage = 0;
            string moveName = "";
            Random rand = new Random();
            switch(m_nextMove)
            {
                case 3:
                    moveName = "Ember";
                    damage = rand.Next(20, 40);
                    break;
                default:
                    moveName = "Scratch";
                    damage = rand.Next(5, 10);
                    break;
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
