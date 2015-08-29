using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BabysitterKata;

namespace BabysitterKataTest
{
    [TestClass]
    public class AllTests
    {
        private BabysitterWageCalculator calculator;

        private readonly DateTime T_04_59_PM = new DateTime(2015, 1, 1, 16, 59, 59);
        private readonly DateTime T_05_00_PM = new DateTime(2015, 1, 1, 17, 00, 00);
        private readonly DateTime T_06_00_PM = new DateTime(2015, 1, 1, 18, 00, 00);
        private readonly DateTime T_06_01_PM = new DateTime(2015, 1, 1, 18, 00, 01);
        private readonly DateTime T_07_00_PM = new DateTime(2015, 1, 1, 19, 00, 00);
        private readonly DateTime T_09_59_PM = new DateTime(2015, 1, 1, 21, 59, 59);
        private readonly DateTime T_10_00_PM = new DateTime(2015, 1, 1, 22, 00, 00);
        private readonly DateTime T_10_30_PM = new DateTime(2015, 1, 1, 22, 30, 00);
        private readonly DateTime T_10_59_PM = new DateTime(2015, 1, 1, 22, 59, 59);
        private readonly DateTime T_11_00_PM = new DateTime(2015, 1, 1, 23, 00, 00);
        private readonly DateTime T_11_30_PM = new DateTime(2015, 1, 1, 23, 30, 00);
        private readonly DateTime T_00_00_AM = new DateTime(2015, 1, 2, 00, 00, 00);
        private readonly DateTime T_01_00_AM = new DateTime(2015, 1, 2, 01, 00, 00);
        private readonly DateTime T_01_30_AM = new DateTime(2015, 1, 2, 01, 30, 00);
        private readonly DateTime T_04_00_AM = new DateTime(2015, 1, 2, 04, 00, 00);
        private readonly DateTime T_04_01_AM = new DateTime(2015, 1, 2, 04, 00, 01);


        [TestInitialize()]
        public void TestInitialize()
        {
            calculator = new BabysitterWageCalculator();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Checked in before 5pm", AllowDerivedTypes=true )]
        public void DoNotAllowBabysitterChecksInBeforeMinAllowedTime() 
        {
            Decimal salary = calculator.calculate(T_04_59_PM, T_07_00_PM, T_04_00_AM);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Checked out after 4am next day", AllowDerivedTypes = true)]
        public void DoNotAllowBabysitterChecksOutAfterMaxAllowedTime()
        {
            Decimal salary = calculator.calculate(T_05_00_PM, T_07_00_PM, T_04_01_AM);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Checked out earlier than check in", AllowDerivedTypes = true)]
        public void DoNotAllowCheckOutEarlierThanCheckIn() 
        {
            Decimal salary = calculator.calculate(T_06_00_PM, T_07_00_PM, T_05_00_PM);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Bed time before check in", AllowDerivedTypes = true)]
        public void BedTimeIfAnyShouldBeAfterCheckIn()
        {
            Decimal salary = calculator.calculate(T_06_00_PM, T_05_00_PM, T_07_00_PM);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Bed time after check out", AllowDerivedTypes = true)]
        public void BedTimeIfAnyShouldBeBeforeCheckOut()
        {
            Decimal salary = calculator.calculate(T_05_00_PM, T_07_00_PM, T_06_00_PM);
        }

        [TestMethod]
        public void BabysitterGetPaid24Per2HourFromStartToBedTime()
        {
            Assert.AreEqual(new Decimal(24), calculator.calculate(T_10_00_PM, T_00_00_AM, T_00_00_AM));
        }

        [TestMethod]
        public void BabysitterGetPaid24Per2HourFromStartToBedTimeBeforeMidnight()
        {
            Assert.AreEqual(new Decimal(24), calculator.calculate(T_05_00_PM, T_07_00_PM, T_07_00_PM));
        }

        [TestMethod]
        public void BabysitterGetPaid12Per1Hour1SecondFromStartToBedTime()
        {
            Assert.AreEqual(new Decimal(12), calculator.calculate(T_10_59_PM, T_00_00_AM, T_00_00_AM));
        }

        [TestMethod]
        public void BabysitterGetPaid12Per1Hour1SecondFromStartToBedTimeBeforeMidnight()
        {
            Assert.AreEqual(new Decimal(12), calculator.calculate(T_09_59_PM, T_11_00_PM, T_11_00_PM));
        }

        [TestMethod]
        public void BabysitterGetPaid8Per1HourFromBedTimeToMidnight()
        {
            Assert.AreEqual(new Decimal(8), calculator.calculate(T_11_00_PM, T_11_00_PM, T_00_00_AM));
        }

        [TestMethod]
        public void BabysitterGetPaid12Per1HourFromStartToBed8Per1HourToMidnight16Per1HourToEnd()
        {
            Assert.AreEqual(new Decimal(36), calculator.calculate(T_10_00_PM, T_11_00_PM, T_01_00_AM));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Bed time after midnight", AllowDerivedTypes = true)]
        public void UndefinedBehaviorIfBedtimeAfterMidnight()
        {
            Assert.AreEqual(new Decimal(36), calculator.calculate(T_10_00_PM, T_01_00_AM, T_01_00_AM));
        }

        [TestMethod]
        public void BabysitterGetPaid12Per1HourFromStartToBed8Per1HourToMidnight16Per1HourToEndFractionalHours()
        {
            /*
             *  10:30pm to 11:30pm: 1 full hour: charge 12
             *  11:30pm to midnight: 0 full hours, carry 0.5 hour: carge 0
             *  midnight to 1:30am: 1.5 + 0.5 = 2 full hours: charge 2 * 16 + 32
             *  Grand total: 44
             */
            Assert.AreEqual(new Decimal(44), calculator.calculate(T_10_30_PM, T_11_30_PM, T_01_30_AM));
        }

    }
}
