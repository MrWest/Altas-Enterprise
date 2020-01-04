using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Tests
{
    [TestClass]
    public class DateTimeExtensionsTests
    {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void GetMonthsDelta_GivenSecondDateLesserThanFirst_Throws()
        {
            new DateTime(2015, 2, 1).GetMonthDelta(new DateTime(2015, 1, 1));
        }

        [TestMethod]
        public void GetMonthDelta_GivenTwoDates_ReturnsCorrectResult()
        {
            Assert.AreEqual(12, new DateTime(2015, 6, 1).GetMonthDelta(new DateTime(2016, 6, 1)));
            Assert.AreEqual(24, new DateTime(2013, 6, 1).GetMonthDelta(new DateTime(2015, 6, 1)));
            Assert.AreEqual(5, new DateTime(2015, 1, 1).GetMonthDelta(new DateTime(2015, 6, 1)));
            Assert.AreEqual(0, new DateTime(2015, 6, 1).GetMonthDelta(new DateTime(2015, 6, 1)));
        }
    }
}