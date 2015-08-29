using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabysitterKata
{
    public class BabysitterWageCalculator
    {
        private const int MIN_ALLOWED_START_TIME = 17 * 60; // 5pm, in minutes since beginning of the day
        private const int MAX_ALLOWED_END_TIME = (24 + 4) * 60; // 4am next day, in minutes since beginning of the check in day

        public decimal calculate(DateTime startTime, DateTime bedTime, DateTime endTime)
        {
            DateTime minCheckIn = startTime.Date.AddMinutes(MIN_ALLOWED_START_TIME);
            DateTime maxCheckOut = startTime.Date.AddMinutes(MAX_ALLOWED_END_TIME);

            if (startTime < minCheckIn || endTime > maxCheckOut)
                throw new ArgumentOutOfRangeException();

            return 0;
        }
    }
}
