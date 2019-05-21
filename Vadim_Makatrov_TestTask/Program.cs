using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vadim_Makatrov_TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            DataOperations dataOperations = new DataOperations();
            dataOperations.Menu();

            Console.ReadKey();
        }
    }
}
