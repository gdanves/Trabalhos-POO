using System;

namespace Fakemon
{
    class Duquack : Creature
    {
        public Duquack()
        {
            SetHealth(200);
            SetName("Duquack");
        }

        public void CastMove(uint move, Creature target)
        {
            int damage = 0, heal = 0;
            string moveName = "", result = "";
            ConsoleColor color = ConsoleColor.Cyan;
            Random rand = new Random();
            switch(move)
            {
                case 1:
                    moveName = "Peck";
                    damage = rand.Next(10, 15);
                    break;
                case 2:
                    moveName = "Bouncy Bubble";
                    damage = rand.Next(5, 8);
                    heal = damage;
                    break;
                case 3:
                    moveName = "Headbutt";
                    if(rand.Next(1, 4) == 1) {
                        color = ConsoleColor.Red;
                        result = string.Format("Duquack errou.", moveName);
                    }
                    else
                        damage = rand.Next(15, 22);
                    break;
                default:
                    moveName = "Confusion";
                    if(rand.Next(1, 3) == 1) {
                        color = ConsoleColor.Red;
                        result = string.Format("O {0} falhou.", moveName);
                    }
                    else
                    {
                        result = string.Format("{0} agora está confuso (1 turno)!", target.GetName());
                        target.SetIsConfused(true);
                    }
                    break;
            }

            Console.WriteLine("Duquack, use {0}!", moveName);
            Console.ForegroundColor = color;
            if(result != "") {
                Console.WriteLine(result);
                Console.ResetColor();
            }
            else
                ProcessMove(target, damage, heal);
        }
    }
}
