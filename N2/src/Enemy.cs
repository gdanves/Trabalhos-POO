using System;

namespace MathMagic
{
    class Enemy : Creature
    {
        public Enemy()
        {
            SetHealth(50);
        }

        public int GetDamage()
        {
            Random rand = new Random();
            return rand.Next(4, 7);
        }

        protected override void ShowDamage(int value)
        {
            ConsoleEx.WriteLine(String.Format("Você causou {0} de dano ao Matemágico!", value), ConsoleColor.Green);
        }
    }
}
