using System;
using System.Threading;

namespace Fakemon
{
    class Game
    {
        const int textDelay = 1500;
        private uint m_rounds;
        private Duquack m_duquack; // player fakemon
        private Embear m_embear; // enemy

        public Game()
        {
            Start();
        }

        private void Start()
        {
            m_rounds = 1;
            m_duquack = new Duquack();
            m_embear = new Embear();
            Console.WriteLine("Você encontrou um Embear, prepare-se para a batalha!");
            Poll();
        }

        private void End(bool won)
        {
            uint choice;
            string result = won ? "Você ganhou! Deseja jogar novamente?" : "Você perdeu. Deseja tentar novamente?";
            Console.WriteLine("{0}\n1. Sim\n2. Não", result);
            if(!uint.TryParse(Console.ReadLine(), out choice) || choice != 1)
                Program.Terminate();
            else
                Start();
        }

        private void Poll()
        {
            Console.WriteLine("============ RODADA {0} ============", m_rounds);
            Console.WriteLine("Turno de {0}, escolha uma ação:", m_duquack.GetName());
            Console.WriteLine("1. Peck \t 2. Bouncy Bubble");
            Console.WriteLine("3. Headbutt \t 4. Confusion ");

            uint move;
            while(!uint.TryParse(Console.ReadLine(), out move) || move == 0 || move >= 5)
                Console.WriteLine("Ação inválida, escolha uma ação entre 1 - 4.");
            m_duquack.CastMove(move, m_embear);
            Thread.Sleep(textDelay);

            if(m_embear.GetHealth() <= 0)
            {
                End(true);
                return;
            }

            m_embear.CastMove(m_duquack);

            if(m_duquack.GetHealth() <= 0)
            {
                End(false);
                return;
            }

            Console.WriteLine("HP {0}: {1} || HP {2}: {3}",
                m_duquack.GetName(),
                m_duquack.GetHealth(),
                m_embear.GetName(),
                m_embear.GetHealth());
            Thread.Sleep(textDelay);

            m_rounds++;
            Poll();
        }
    }
}
