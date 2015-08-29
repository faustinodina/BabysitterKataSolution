﻿using System;
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
        private readonly DateTime T_07_00_PM = new DateTime(2015, 1, 1, 19, 00, 00);
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

    }
}
