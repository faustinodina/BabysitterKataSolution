using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabysitterKata
{
    public class BabysitterKataCli
    {
        public const string MSG_BAD_TIMELINE = "Error: Bad order on date/time parameters.";

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
                    .AppendLine(help());
                return result.ToString();
            }

            Decimal salary = new Decimal(0);
            var calculator = new BabysitterWageCalculator();
            try
            {
                salary = calculator.calculate(startTime, bedTime, endTime);
                result.Append(string.Format("Your salary is {0}", salary));
            }
            catch (ArgumentOutOfRangeException )
            {
                result.AppendLine(MSG_BAD_TIMELINE).AppendLine(help());
            }
            catch (NotSupportedException e)
            {
                result.AppendLine(e.Message).AppendLine(help());
            }
            catch
            {
                result.AppendLine("Error.").AppendLine(help());
            }

            return result.ToString();
        }

        private string help()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("Usage:")
                .AppendLine("    BabysitterKata <startDateTime> <bedDateTime> <endDateTime>")
                .AppendLine().AppendLine("Dates should be enclosed between double quotation marks. Example:")
                .AppendLine("BabysitterSalary \"8/29/2015 22:30\" \"8/29/2015 23:30\" \"8/30/2015 1:30\"");
            return result.ToString();
        }
    }
}
