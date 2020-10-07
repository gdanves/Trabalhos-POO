using System;
using System.Threading;

namespace Fakemon
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("┌\t\t\t  ┐\n\tFakemon 1.0\n└ \t\t\t  ┘");
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
