using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DDDDemo.Model.Tests
{
    [TestClass]
    public class OpeningPeriodTests
    {
        [TestMethod]
        [ExpectedException(typeof(Model.InvalidOpeningPeriodException))]
        public void Detects_Invalid_OpeningAndClosing()
        {
            TimeSpan opening = new TimeSpan(8,0,0);
            TimeSpan closing=new TimeSpan(7,0,0);
            OpeningPeriod newPeriod=new OpeningPeriod(opening,closing,DayOfWeek.Monday);
        }

        [TestMethod]
        public void Constructor_Works()
        {
            TimeSpan opening = new TimeSpan(8,0,0);
            TimeSpan closing=new TimeSpan(9,0,0);
            OpeningPeriod newPeriod=new OpeningPeriod(opening,closing,DayOfWeek.Monday);
            Assert.AreEqual(opening,newPeriod.Opening);
            Assert.AreEqual(closing,newPeriod.Closing);
        }
    }
}
