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
            Decimal salary = salary = calculator.calculate(new DateTime(2015, 1, 1, 16, 59, 59));
        }
    }
}
