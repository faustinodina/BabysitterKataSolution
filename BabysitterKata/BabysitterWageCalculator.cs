using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabysitterKata
{

    public class BabysitterWageCalculator
    {
        public const string MSG_BEDTIME_AFTER_MIDNIGHT = "Error: Bedtime should precede midnight.";

        private const int MIN_ALLOWED_START_TIME = 17 * 60; // 5pm, in minutes since beginning of the day
        private const int MAX_ALLOWED_END_TIME = (24 + 4) * 60; // 4am next day, in minutes since beginning of the check in day
        private const int WAGE_START_TO_BED = 12;
        private const int WAGE_AFTER_BED = 8;
        private const int WAGE_AFTER_MIDNIGHT = 16;

        /// <summary>
        /// Calculates babysitter salary based on the following rules
        /// - starts no earlier than 5:00PM
        /// - leaves no later than 4:00AM
        /// - gets paid $12/hour from start-time to bedtime
        /// - gets paid $8/hour from bedtime to midnight
        /// - gets paid $16/hour from midnight to end of job
        /// - gets paid for full hours (no fractional hours)
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="bedTime">Is supposed to be before midnight. Can be equal to startTime or endTime</param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public decimal calculate(DateTime startTime, DateTime bedTime, DateTime endTime)
        {
            DateTime minCheckIn = startTime.Date.AddMinutes(MIN_ALLOWED_START_TIME);
            DateTime maxCheckOut = startTime.Date.AddMinutes(MAX_ALLOWED_END_TIME);
            DateTime midnight = startTime.Date.AddHours(24);

            if (startTime < minCheckIn || endTime > maxCheckOut
                || startTime > endTime
                || bedTime < startTime || bedTime > endTime
                )
                throw new ArgumentOutOfRangeException();

            // !special case bedTime after midnight not defined in specs
            if (midnight < bedTime)
                throw new NotSupportedException(MSG_BEDTIME_AFTER_MIDNIGHT);

            var partial = calculate(startTime, bedTime, WAGE_START_TO_BED, 0);
            Decimal salary = partial.Salary;

            if (endTime <= midnight)
            {
                // trivial timeline: start, bed, end, midnight 
                partial = calculate(bedTime, endTime, WAGE_AFTER_BED, partial.Carry1HourFraction);
                salary += partial.Salary;
                
                return salary;
            }
            else
            {
                // trivial timeline: start, bed, midnight, end
                partial = calculate(bedTime, midnight, WAGE_AFTER_BED, partial.Carry1HourFraction);
                salary += partial.Salary;

                partial = calculate(midnight, endTime, WAGE_AFTER_MIDNIGHT, partial.Carry1HourFraction);
                salary += partial.Salary;

                return salary;
            }
        }

        private class PartialSalary
        {
            public readonly Decimal Salary;
            public readonly double Carry1HourFraction;

            public PartialSalary(Decimal salary, double carry1HourFraction)
            {
                Salary = salary;
                Carry1HourFraction = carry1HourFraction;
            }
        }

        private PartialSalary calculate(DateTime start, DateTime end, int wage, double carry1HourFraction)
        {
            double hours = end.Subtract(start).TotalHours + carry1HourFraction;
            double fullHours = Math.Floor(hours);
            Decimal salary = (int)fullHours * wage;
            carry1HourFraction = hours - fullHours;      // truncated minutes are to be added to the next segment
            return new PartialSalary(salary, carry1HourFraction);
        }
    }
}
