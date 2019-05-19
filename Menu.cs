using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exem
{
    class Menu
    {
        static public int Manu()
        {
            Console.WriteLine("1) Print Students");
            Console.WriteLine("2) Print Grades");
            Console.WriteLine("3) Update DB");
            Console.WriteLine("4) EXIT");
            try
            {
                int TMP = Convert.ToInt32(Console.ReadLine());
                if (TMP < 1 || TMP > 4)
                    throw new MyIndexOutOfRangeException();
                switch (TMP)
                {
                    case 1:
                        StudentsDAO.PrintStudents();
                        Console.WriteLine("Press Enter to Continue");
                        Console.ReadLine();
                        return 1;
                    case 2:
                        GradesDAO.PrintGrades();
                        Console.WriteLine("Press Enter to Continue");
                        Console.ReadLine();
                        return 1;
                    case 3:

                        return 1;
                    case 4:
                        return 4;
                }
                
            }
            catch(FormatException)
            {
                Console.WriteLine("Format Exception Try A Number");
                Manu();
            }
            catch (MyIndexOutOfRangeException e)
            {
                Console.WriteLine("Index OUT Of Range");
                Manu();
            }
            return 1;


        }

    }
}
