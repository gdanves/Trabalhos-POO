using System;
using System.Threading;

namespace MathMagic
{
    class Game
    {
        private uint m_rounds;
        private uint m_difficulty;

        public Game()
        {
            string str = "Bem-vindo ao mundo da MathMagic!\n" +
                         "Para sobreviver, você terá que resolver operações matemáticas básicas.\n" +
                         "Quanto mais precisão exigir o ataque, mais difícil será a operação.\n" +
                         "Boa sorte!";
            Console.WriteLine(str);
            ConsoleEx.WriteLine("Pressione qualquer tecla para continuar...", ConsoleColor.Gray);
            Console.ReadKey(true);
            Start();
        }

        private void Start()
        {
            ConsoleEx.AddSeparator();
            string str = "Escolha uma dificuldade.\n" +
                         "A dificuldade afeta o tempo de resposta e as operações.";
            Console.WriteLine(str);
            ConsoleEx.WriteLine("1 - Fácil\n2 - Normal\n3 - Difícil", ConsoleColor.Yellow);
            ConsoleEx.ParseUint(ref m_difficulty, 1, 3, "Dificuldade inválida, escolha entre 1 (fácil), 2 (médio) ou 3 (difícil).");

            m_rounds = 1;
            Poll();
        }

        private void Poll()
        {
            ConsoleEx.AddSeparator();
            Console.WriteLine("Rodada {0}, escolha onde deseja atacar:", m_rounds);
            ConsoleEx.WriteLine("1 - Torso\n2 - Perna\n3 - Cabeça", ConsoleColor.Yellow);
            uint choice = 0;
            ConsoleEx.ParseUint(ref choice, 1, 3, "Local inválido, escolha entre 1 (torso), 2 (perna) ou 3 (cabeça).");
            /*
                TODO:
                generate math operation, show timer (which is affected by difficulty)
                roll damage if succeeded, roll enemy attack
                check game state and poll again
            */
        }
    }
}
