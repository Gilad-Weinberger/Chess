using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Board Square = new Board();
            DrawBoard(Square);
        }

        public static void DrawBoard(Board Square)
        {
            for (int rank = 1; rank <= 8; rank++)
            {
                Console.WriteLine(string.Concat(System.Linq.Enumerable.Repeat("-", 33))); 
                for (int file = 1; file <= 8; file++)
                {
                    Console.Write("| " + "b" + " ");
                }
                Console.Write("|   ");
                Console.WriteLine();
            }
            Console.WriteLine(string.Concat(System.Linq.Enumerable.Repeat("-", 33)));

        }
    }
}
