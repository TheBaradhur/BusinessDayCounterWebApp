using BusinessDayCounterWebApp.Helpers;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace BusinessDayCounterWebApp.UnitTests.HelpersTests
{
    public class DateHelperTests
    {
        public static IEnumerable<object[]> YearsListExpectations => new[]
        {
            new object[] { new DateTime(2014, 10, 7), new DateTime(2013, 10, 9), new List<int>() },
            new object[] { new DateTime(2013, 10, 7), new DateTime(2013, 10, 9), new List<int> { 2013 } },
            new object[] { new DateTime(2013, 10, 7), new DateTime(2015, 10, 9), new List<int> { 2013, 2014, 2015 } },
        };

        [Theory]
        [MemberData(nameof(YearsListExpectations))]
        public void GetYearsBetweenDates_WhenPassedDates_ThenReturnsExpectedYearsList(DateTime firstDate, DateTime secondDate, List<int> expectedResult)
        {
            // Arrange
            var target = new DateHelper();

            // Act
            var actual = target.GetYearsBetweenDates(firstDate, secondDate);

            // Assert
            actual.Should().BeEquivalentTo(expectedResult);
        }
    }
}