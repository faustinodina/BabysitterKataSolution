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
        private const int WAGE_START_TO_BED = 12;
        private const int WAGE_AFTER_BED = 8;
        private const int WAGE_AFTER_MIDNIGHT = 16;

        public decimal calculate(DateTime startTime, DateTime bedTime, DateTime endTime)
        {
            Decimal salary = new Decimal(0);

            DateTime minCheckIn = startTime.Date.AddMinutes(MIN_ALLOWED_START_TIME);
            DateTime maxCheckOut = startTime.Date.AddMinutes(MAX_ALLOWED_END_TIME);
            DateTime midnight = startTime.Date.AddHours(24);

            if (startTime < minCheckIn || endTime > maxCheckOut
                || startTime > endTime
                || bedTime < startTime || bedTime > endTime
                )
                throw new ArgumentOutOfRangeException();

            // trivial timeline: start, bed, midnight, end
            salary += (int)(Math.Ceiling(bedTime.Subtract(startTime).TotalHours)) * WAGE_START_TO_BED;
            salary += (int)(Math.Ceiling(midnight.Subtract(bedTime).TotalHours)) * WAGE_AFTER_BED;
            salary += (int)(Math.Ceiling(endTime.Subtract(midnight).TotalHours)) * WAGE_AFTER_MIDNIGHT;
           
            return salary;
        }
    }
}
