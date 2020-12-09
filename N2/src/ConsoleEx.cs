using System;

namespace MathMagic
{
    class ConsoleEx
    {
        public static void WriteLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void AddSeparator()
        {
            Console.WriteLine("▬\t▬\t▬\t▬\t▬");
        }

        public static void ParseUint(ref uint value, uint min, uint max, string errorMessage)
        {
            while(!uint.TryParse(Console.ReadLine(), out value) || value < min || (max > 0 && value > max))
                Console.WriteLine(errorMessage);
        }
    }
}
