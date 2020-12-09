using System;
using System.Threading;

namespace MathMagic
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleEx.WriteLine("┌\t\t\t  ┐\n\tMathMagic 1.0\n└ \t\t\t  ┘", ConsoleColor.Blue);
            Thread.Sleep(1000);
            new Game();
        }

        public static void Terminate()
        {
            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}
