using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class DateTimeUtilitiesTests
    {
        [TestMethod]
        public void MonthNames_ReturnsLocalizedVersionsOfAllMonthNames()
        {
            // Arrange
            var currentDateFormat = (DateTimeFormatInfo.CurrentInfo ?? DateTimeFormatInfo.InvariantInfo);
            string[] expectedNames = currentDateFormat.MonthNames.Take(12).Aggregate(new List<string>(), (list, m) =>
            {
                list.Add(m.Capitalize());
                return list;
            }).ToArray();

            // Act
            string[] actualNames = DateTimeUtilities.MonthNames.ToArray();

            // Assert
            CollectionAssert.AreEqual(expectedNames, actualNames);
        }
    }
}
