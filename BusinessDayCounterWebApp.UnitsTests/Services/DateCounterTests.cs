using BusinessDayCounterWebApp.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace BusinessDayCounterWebApp.UnitsTests
{
    public class DateCounterTests
    {
        public static IEnumerable<object[]> Dates => new[]
        {
            new object[] { new DateTime(2013, 10, 7), new DateTime(2013, 10, 9), 1 },
            new object[] { new DateTime(2013, 10, 5), new DateTime(2013, 10, 14), 5 },
            new object[] { new DateTime(2013, 10, 7), new DateTime(2014, 1, 1), 61 },
            new object[] { new DateTime(2013, 10, 7), new DateTime(2013, 10, 5), 0 },
        };

        [Theory]
        [MemberData(nameof(Dates))]
        public void WeekdaysBetweenTwoDates_WhenPasseValidDates_ThenReturnsCorrectCount(DateTime firstDate, DateTime secondDate, int expectedCounts)
        {
            // Arrange
            var target = new DateCounter();

            // Act
            var actual = target.WeekdaysBetweenTwoDates(firstDate, secondDate);

            // Assert
            actual.Should().Be(expectedCounts);
        }
    }
}