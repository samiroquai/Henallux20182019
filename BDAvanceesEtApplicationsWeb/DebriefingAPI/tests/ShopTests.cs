using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
namespace DDDDemo.Model.Tests
{
    [TestClass]
    public class ShopTests
    {
        [TestMethod]
        public void AddOpeningPeriod_Works()
        {
            TimeSpan opening1 = new TimeSpan(8,0,0);
            TimeSpan closing1=new TimeSpan(15,0,0);
            OpeningPeriod newPeriod1=new OpeningPeriod(opening1,closing1,DayOfWeek.Monday);
            Shop shop=new Shop("Test",1);
            shop.AddOpeningPeriod(newPeriod1);
            Assert.AreEqual(1,shop.OpeningPeriods.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(Model.OverlappingOpeningPeriodsException))]
        public void AddOpeningPeriod_Detects_OverlappingPeriods_SameDay_Case1()
        {
            TimeSpan opening1 = new TimeSpan(8,0,0);
            TimeSpan closing1=new TimeSpan(15,0,0);
            TimeSpan opening2=new TimeSpan(7,0,0);
            TimeSpan closing2=new TimeSpan(12,0,0);
            OpeningPeriod newPeriod1=new OpeningPeriod(opening1,closing1,DayOfWeek.Monday);
            OpeningPeriod newPeriod2=new OpeningPeriod(opening2,closing2,DayOfWeek.Monday);
            Shop shop=new Shop("Test",2);
            shop.AddOpeningPeriod(newPeriod1);
            shop.AddOpeningPeriod(newPeriod2);
        }

        [TestMethod]
        [ExpectedException(typeof(Model.OverlappingOpeningPeriodsException))]
        public void AddOpeningPeriod_Detects_OverlappingPeriods_SameDay_Case2()
        {
            TimeSpan opening1 = new TimeSpan(8,0,0);
            TimeSpan closing1=new TimeSpan(15,0,0);
            TimeSpan opening2=new TimeSpan(9,0,0);
            TimeSpan closing2=new TimeSpan(10,0,0);
            OpeningPeriod newPeriod1=new OpeningPeriod(opening1,closing1,DayOfWeek.Monday);
            OpeningPeriod newPeriod2=new OpeningPeriod(opening2,closing2,DayOfWeek.Monday);
            Shop shop=new Shop("Test",3);
            shop.AddOpeningPeriod(newPeriod1);
            shop.AddOpeningPeriod(newPeriod2);
        }

        [TestMethod]
        [ExpectedException(typeof(Model.OverlappingOpeningPeriodsException))]
        public void AddOpeningPeriod_Detects_OverlappingPeriods_SameDay_Case3()
        {
            TimeSpan opening1 = new TimeSpan(8,0,0);
            TimeSpan closing1=new TimeSpan(15,0,0);
            TimeSpan opening2=new TimeSpan(14,0,0);
            TimeSpan closing2=new TimeSpan(16,0,0);
            OpeningPeriod newPeriod1=new OpeningPeriod(opening1,closing1,DayOfWeek.Monday);
            OpeningPeriod newPeriod2=new OpeningPeriod(opening2,closing2,DayOfWeek.Monday);
            Shop shop=new Shop("Test",4);
            shop.AddOpeningPeriod(newPeriod1);
            shop.AddOpeningPeriod(newPeriod2);
        }

        #region ...
        [TestMethod]
        //[ExpectedException(typeof(Model.OverlappingOpeningPeriodsException))]
        public void AddOpeningPeriod_Detects_OverlappingPeriods_SameDay_Case4()
        {
            TimeSpan opening1 = new TimeSpan(8, 0, 0);
            TimeSpan closing1 = new TimeSpan(15, 0, 0);
            TimeSpan opening2 = new TimeSpan(7, 0, 0);
            TimeSpan closing2 = new TimeSpan(16, 0, 0);
            OpeningPeriod newPeriod1 = new OpeningPeriod(opening1, closing1, DayOfWeek.Monday);
            OpeningPeriod newPeriod2 = new OpeningPeriod(opening2, closing2, DayOfWeek.Monday);
            Shop shop = new Shop("Test",5);
            shop.AddOpeningPeriod(newPeriod1);
            Assert.ThrowsException<OverlappingOpeningPeriodsException>(() => shop.AddOpeningPeriod(newPeriod2));
            //shop.AddOpeningPeriod(newPeriod2);
        }
        #endregion



        [TestMethod]
        public void AddOpeningPeriod_DoesntDetect_OverlappingPeriods_DifferentDays()
        {
            TimeSpan opening1 = new TimeSpan(8,0,0);
            TimeSpan closing1=new TimeSpan(15,0,0);
            TimeSpan opening2=new TimeSpan(7,0,0);
            TimeSpan closing2=new TimeSpan(12,0,0);
            OpeningPeriod newPeriod1=new OpeningPeriod(opening1,closing1,DayOfWeek.Monday);
            OpeningPeriod newPeriod2=new OpeningPeriod(opening2,closing2,DayOfWeek.Wednesday);
            Shop shop=new Shop("Test",6);
            shop.AddOpeningPeriod(newPeriod1);
            shop.AddOpeningPeriod(newPeriod2);
            Assert.AreEqual(2,shop.OpeningPeriods.Count());
        }
   
    }
}
