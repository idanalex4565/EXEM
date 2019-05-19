using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exem
{
    class StudentsDAO
    {
        public static void PrintStudents()
        {
            using (Exem5Entities entities = new Exem5Entities())
            {
                entities.Students.ToList().ForEach(r => Console.WriteLine(JsonConvert.SerializeObject(r)));
            }
        }

    }
}
