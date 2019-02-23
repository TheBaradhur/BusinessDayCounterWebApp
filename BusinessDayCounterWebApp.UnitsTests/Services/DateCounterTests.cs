using BusinessDayCounterWebApp.Models;
using BusinessDayCounterWebApp.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace BusinessDayCounterWebApp.UnitsTests
{
    public class DateCounterTests
    {
        public static IEnumerable<object[]> WeekDaysDates => new[]
        {
            new object[] { new DateTime(2013, 10, 7), new DateTime(2013, 10, 9), 1 },
            new object[] { new DateTime(2013, 10, 5), new DateTime(2013, 10, 14), 5 },
            new object[] { new DateTime(2013, 10, 7), new DateTime(2014, 1, 1), 61 },
            new object[] { new DateTime(2013, 10, 7), new DateTime(2013, 10, 5), 0 },
        };
        
        public static IList<DateTime> HolidaysList = new List<DateTime> 
        {
            new DateTime(2013, 12, 25),
            new DateTime(2013, 12, 26),
            new DateTime(2014, 1, 1) 
        };

        public static IEnumerable<object[]> BusinessDaysDates => new[]
        {
            new object[] { new DateTime(2013, 10, 7), new DateTime(2013, 10, 9), HolidaysList, 1 },
            new object[] { new DateTime(2013, 12, 24), new DateTime(2013, 12, 27), HolidaysList, 0 },
            new object[] { new DateTime(2013, 10, 7), new DateTime(2014, 1, 1), HolidaysList, 59 },
        };

        public static IList<PublicHoliday> SimpleCustomHolidaysList = new List<PublicHoliday>
        {
            new PublicHoliday { Name = "Christmas", Month = 12, Day = 25 },
            new PublicHoliday { Name = "Boxing Day", Month = 12, Day = 26 },
            new PublicHoliday { Name = "New Year", Month = 1, Day = 1 },
        };

        public static IEnumerable<object[]> BusinessDaysDatesCustomHolidays => new[]
        {
            new object[] { new DateTime(2013, 10, 7), new DateTime(2013, 10, 9), SimpleCustomHolidaysList, 1 },
            new object[] { new DateTime(2013, 12, 24), new DateTime(2013, 12, 27), SimpleCustomHolidaysList, 0 },
            new object[] { new DateTime(2013, 10, 7), new DateTime(2014, 1, 1), SimpleCustomHolidaysList, 59 },
        };

        [Theory]
        [MemberData(nameof(WeekDaysDates))]
        public void WeekdaysBetweenTwoDates_WhenPasseValidDates_ThenReturnsCorrectCount(DateTime firstDate, DateTime secondDate, int expectedCounts)
        {
            // Arrange
            var target = new DateCounter();

            // Act
            var actual = target.WeekdaysBetweenTwoDates(firstDate, secondDate);

            // Assert
            actual.Should().Be(expectedCounts);
        }

        [Theory]
        [MemberData(nameof(BusinessDaysDates))]
        public void BusinessDaysBetweenTwoDates_WhenPasseValidDates_ThenReturnsCorrectCount(DateTime firstDate, DateTime secondDate, IList<DateTime> holidaysList, int expectedCounts)
        {
            // Arrange
            var target = new DateCounter();

            // Act
            var actual = target.BusinessDaysBetweenTwoDates(firstDate, secondDate, holidaysList);

            // Assert
            actual.Should().Be(expectedCounts);
        }

        [Theory]
        [MemberData(nameof(BusinessDaysDatesCustomHolidays))]
        public void BusinessDaysBetweenTwoDatesCustomHolidays_WhenPasseValidDatesAndCustomHolidays_ThenReturnsCorrectCount(DateTime firstDate, DateTime secondDate, IList<PublicHoliday> holidaysList, int expectedCounts)
        {
            // Arrange
            var target = new DateCounter();

            // Act
            var actual = target.BusinessDaysBetweenTwoDatesCustomHolidays(firstDate, secondDate, holidaysList);

            // Assert
            actual.Should().Be(expectedCounts);
        }
    }
}