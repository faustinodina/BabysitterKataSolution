using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BabysitterKata;

namespace BabysitterKataTest
{
    [TestClass]
    public class CliTests
    {
        [TestMethod]
        public void CliHandlesArgsArray()
        {
            //10_30_PM, T_11_30_PM, T_01_30_A
            BabysitterKataCli cliHelper = new BabysitterKataCli();
            string[] args = { "8/29/2015 22:30", "8/29/2015 23:30", "8/30/2015 1:30" };
            Assert.AreEqual("Your salary is 44", cliHelper.execute(args));
        }

        [TestMethod]
        public void CliHandlesIncorrectNumberOfArguments()
        {
            BabysitterKataCli cliHelper = new BabysitterKataCli();
            string[] args = { "8/29/2015 22:30", "8/29/2015 23:30" };
            Assert.AreEqual("Bad arguments", cliHelper.execute(args).Substring(0, 13));
        }

        [TestMethod]
        public void CliHandlesBadlyFormattedArguments()
        {
            BabysitterKataCli cliHelper = new BabysitterKataCli();
            string[] args = { "8/29/2015 22:30", "8/29/2015 23:30", "qwerty" };
            Assert.AreEqual("Bad arguments", cliHelper.execute(args).Substring(0, 13));
        }

        [TestMethod]
        public void CliHandlesInternalExceptions()
        {
            BabysitterKataCli cliHelper = new BabysitterKataCli();
            // bedtime after midnight
            string[] args = { "8/29/2015 22:30", "8/30/2015 00:30", "8/30/2015 1:30" };
            Assert.AreEqual("Error", cliHelper.execute(args).Substring(0, 5));
        }
    }
}
