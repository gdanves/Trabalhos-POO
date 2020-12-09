using System;
using System.Threading;
using System.Timers;

namespace MathMagic
{
    class Game
    {
        enum BodyTarget
        {
            Torso = 1,
            Legs  = 2,
            Head  = 3
        }
        enum Difficulty
        {
            Easy    = 1,
            Normal  = 2,
            Hard    = 3
        }

        private Player m_player;
        private Enemy m_enemy;
        private uint m_rounds;
        private uint m_difficulty;
        private static System.Timers.Timer aTimer;

        public Game()
        {
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += OnTimerEnd;

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
            // reset game states
            m_rounds = 1;
            m_player = new Player();
            m_enemy = new Enemy();

            ConsoleEx.AddSeparator();
            string str = "Escolha uma dificuldade.\n" +
                         "A dificuldade afeta o tempo de resposta e as operações.";
            Console.WriteLine(str);
            ConsoleEx.WriteLine("1. Fácil\n2. Normal\n3. Difícil", ConsoleColor.Yellow);
            ConsoleEx.ParseUint(ref m_difficulty, 1, 3, "Dificuldade inválida, escolha entre 1 (fácil), 2 (médio) ou 3 (difícil).");

            // set the answer interval by difficulty
            if(m_difficulty == (uint)Difficulty.Easy)
                aTimer.Interval = 10000;
            else if(m_difficulty == (uint)Difficulty.Normal)
                aTimer.Interval = 7000;
            else
                aTimer.Interval = 5000;
            Console.WriteLine("Você terá {0} segundos para resolver cada operação.", aTimer.Interval / 1000);
            Thread.Sleep(1000);

            Poll();
        }

        private void End(bool victory)
        {
            string result = victory ? "Você ganhou! Deseja jogar novamente?" : "Você perdeu. Deseja tentar novamente?";
            ConsoleEx.WriteLine(String.Format("{0}\n1. Sim\n2. Não", result), ConsoleColor.Yellow);
            uint choice;
            if(!uint.TryParse(Console.ReadLine(), out choice) || choice != 1)
                Program.Terminate();
            else
                Start();
        }

        private void Poll()
        {
            // player turn, get body target
            ConsoleEx.AddSeparator();
            Console.WriteLine("Rodada {0}\nSeu HP: {1}\nHP do Matemágico: {2}",
                              m_rounds, m_player.GetHealth(), m_enemy.GetHealth());
            ConsoleEx.WriteLine("Escolha onde deseja atacar:\n" +
                                "1. Torso\n2. Pernas\n3. Cabeça", ConsoleColor.Yellow);
            uint bodyTarget = 0;
            ConsoleEx.ParseUint(ref bodyTarget, 1, 3, "Local inválido, escolha entre 1 (torso), 2 (perna) ou 3 (cabeça).");

            if(m_rounds == 0) {
                Console.WriteLine("Faça o cálculo necessário para acertar a flecha!");
                Thread.Sleep(1000);
            }


            // reset timer
            aTimer.Stop();
            aTimer.Enabled = true;

            // gen math operation
            (string operation, int result) = GenOperation(bodyTarget);
            ConsoleEx.WriteLine(operation, ConsoleColor.Yellow);
            uint answer = 0;
            while(aTimer.Enabled && (!uint.TryParse(Console.ReadLine(), out answer))) {
                if(aTimer.Enabled)
                    Console.WriteLine("Número inválido, tente novamente.");
            }

            // show results
            if(!aTimer.Enabled)
                ConsoleEx.WriteLine("Você perdeu o turno.", ConsoleColor.Red);
            else if(answer == result) {
                ConsoleEx.WriteLine("Você acertou a flecha!", ConsoleColor.Green);
                Random rand = new Random();
                int damage;
                if(bodyTarget == (uint)BodyTarget.Torso)
                    damage = rand.Next(3, 6);
                else if(bodyTarget == (uint)BodyTarget.Legs)
                    damage = rand.Next(4, 7);
                else
                    damage = rand.Next(7, 11);
                m_enemy.Hit(damage);
            } else
                ConsoleEx.WriteLine(String.Format("Você errou a flecha. A resposta certa era {0}!", result), ConsoleColor.Red);

            aTimer.Stop();
            Thread.Sleep(1000);

            // victory?
            if(m_enemy.IsDead()) {
                End(true);
                return;
            }

            m_player.Hit(m_enemy.GetDamage());

            Thread.Sleep(1000);

            // defeat?
            if(m_player.IsDead()) {
                End(false);
                return;
            }

            // continue
            m_rounds++;
            Poll();
        }

        private (string operation, int result) GenOperation(uint bodyTarget)
        {
            int min = 0, max = 0, result = 0;
            int[] operands = new int[2];
            char[] operators = new char[3] {'+', '-', '*'};
            char operatorChar = operators[0];
            bool easy = m_difficulty == (uint)Difficulty.Easy;
            Random rand = new Random();

            switch(bodyTarget)
            {
                // get min, max and operator
                case (uint)BodyTarget.Torso:
                    if(!easy)
                        operatorChar = operators[rand.Next(0, 2)];
                    min = 6;
                    max = 9;
                    break;
                case (uint)BodyTarget.Legs:
                    if(!easy)
                        operatorChar = operators[rand.Next(0, 2)];
                    min = 11;
                    max = 19;
                    break;
                case (uint)BodyTarget.Head:
                    if(!easy) {
                        operatorChar = operators[2];
                        min = 6;
                        max = 9;
                    } else {
                        min = 21;
                        max = 39;
                    }
                    break;
                default:
                    break;
            }

            // apply difficulty and randomize operands
            bool multiply = operatorChar == '*';
            if(m_difficulty == (uint)Difficulty.Normal) {
                min *= !multiply ? 9 : 2;
                max *= !multiply ? 9 : 2;
            } else if(m_difficulty == (uint)Difficulty.Hard) {
                min *= !multiply ? 17 : 4;
                max *= !multiply ? 17 : 4;
            }
            operands[0] = rand.Next(min, max + 1);
            operands[1] = rand.Next(!multiply ? min : min / 2, !multiply ? max + 1 : max / 2 + 1);

            // get the result
            if(operatorChar == '-') {
                if(operands[1] > operands[0]) {
                    int aux = operands[0];
                    operands[0] = operands[1];
                    operands[1] = aux;
                }
                result = operands[0] - operands[1];
            } else if(multiply)
                result = operands[0] * operands[1];
            else
                result = operands[0] + operands[1];

            string operation = String.Format("{0} {1} {2} = ?", operands[0], operatorChar, operands[1]);
            return(operation, result);
        }

        private static void OnTimerEnd(Object source, System.Timers.ElapsedEventArgs e)
        {
            aTimer.Enabled = false;
            ConsoleEx.WriteLine("Tempo esgotado!", ConsoleColor.Red);
            ConsoleEx.WriteLine("Pressione <Enter> para continuar...", ConsoleColor.Gray);
        }
    }
}
