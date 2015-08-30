using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabysitterKata
{
    class Program
    {
        static void Main(string[] args)
        {
            BabysitterKataCli cliHelper = new BabysitterKataCli();
            Console.WriteLine(cliHelper.execute(args));
            Console.ReadLine();
        }
    }
}
