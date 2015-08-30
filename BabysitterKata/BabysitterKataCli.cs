using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabysitterKata
{
    public class BabysitterKataCli
    {
        public string execute(string[] args)
        {
            StringBuilder result = new StringBuilder();

            DateTime startTime, bedTime, endTime;

            if (args.Length != 3
                || !DateTime.TryParse(args[0], out startTime)
                || !DateTime.TryParse(args[1], out bedTime)
                || !DateTime.TryParse(args[2], out endTime)
                )
            {
                result.AppendLine("Bad arguments.")
                    .AppendLine("Usage:")
                    .AppendLine("    BabysitterKata <startDateTime> <bedDateTime> <endDateTime>")
                    .AppendLine().AppendLine("Dates should be enclosed between double quotation marks. Example:")
                    .AppendLine("BabysitterSalary \"8/29/2015 22:30\" \"8/29/2015 23:30\" \"8/30/2015 1:30\"");
                return result.ToString();
            }

            var calculator = new BabysitterWageCalculator();
            var salary = calculator.calculate(startTime, bedTime, endTime);
            result.Append(string.Format("Your salary is {0}", salary));
            return result.ToString();
        }
    }
}
