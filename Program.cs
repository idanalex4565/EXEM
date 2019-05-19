using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exem
{
    class Program
    {
        static void Main(string[] args)
        {
            int Option;
            Console.WriteLine("Welcome To Menu");
            do
            {
                Option = Menu.Manu();
                Console.Clear();
            }
            while (Option != 4);
            Console.WriteLine("Good Bye");
        }
    }
}
