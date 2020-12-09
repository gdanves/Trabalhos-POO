using System;

namespace MathMagic
{
    class Player : Creature
    {
        public Player()
        {
            SetHealth(50);
        }

        protected override void ShowDamage(int value)
        {
            ConsoleEx.WriteLine(String.Format("O Matemágico causou {0} de dano em você!", value), ConsoleColor.Red);
        }
    }
}
