using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BabysitterKata;

namespace BabysitterKataTest
{
    [TestClass]
    public class AllTests
    {

        [TestMethod]
        [ExpectedException(typeof(Exception), "Checked in before 5pm", AllowDerivedTypes=true )]
        public void DoNotAllowBabysitterChecksInBeforeMinAllowedTime() 
        {
            BabysitterWageCalculator calculator = new BabysitterWageCalculator();
            Decimal salary = salary = calculator.calculate(
                new DateTime(2015, 1, 1, 16, 59, 59),
                new DateTime(2015, 1, 1, 19, 00, 00),
                new DateTime(2015, 1, 1, 23, 00, 00)
                );
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Checked out after 4am next day", AllowDerivedTypes = true)]
        public void DoNotAllowBabysitterChecksOutAfterMaxAllowedTime()
        {
            BabysitterWageCalculator calculator = new BabysitterWageCalculator();
            Decimal salary = salary = calculator.calculate(
                new DateTime(2015, 1, 1, 17, 00, 00), 
                new DateTime(2015, 1, 1, 19, 00, 00), 
                new DateTime(2015, 1, 2, 4,  00, 01)
                );
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Checked out earlier than check in", AllowDerivedTypes = true)]
        public void DoNotAllowCheckOutEarlierThanCheckIn() 
        {
            BabysitterWageCalculator calculator = new BabysitterWageCalculator();
            Decimal salary = salary = calculator.calculate(
                new DateTime(2015, 1, 1, 18, 00, 00),
                new DateTime(2015, 1, 1, 19, 00, 00),
                new DateTime(2015, 1, 1, 17, 00, 00)
                );
        }
    }
}
