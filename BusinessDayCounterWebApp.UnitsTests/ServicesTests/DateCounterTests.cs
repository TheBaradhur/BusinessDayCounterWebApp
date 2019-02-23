using BusinessDayCounterWebApp.Helpers;
using BusinessDayCounterWebApp.Models;
using BusinessDayCounterWebApp.Services;
using FluentAssertions;
using NSubstitute;
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

        private readonly IPublicHolidayCalculator _publicHolidayCalculator;

        public DateCounterTests()
        {
            _publicHolidayCalculator = Substitute.For<IPublicHolidayCalculator>();
        }

        [Theory]
        [MemberData(nameof(WeekDaysDates))]
        public void WeekdaysBetweenTwoDates_WhenPasseValidDates_ThenReturnsCorrectCount(DateTime firstDate, DateTime secondDate, int expectedCounts)
        {
            // Arrange
            var target = new DateCounter(_publicHolidayCalculator);

            // Act
            var actual = target.WeekdaysBetweenTwoDates(firstDate, secondDate);

            // Assert
            actual.Should().Be(expectedCounts);
        }

        [Theory]
        [MemberData(nameof(BusinessDaysDates))]
        public void BusinessDaysBetweenTwoDates_WhenPasseValidDates_ThenReturnsCorrectCount(
            DateTime firstDate,
            DateTime secondDate,
            IList<DateTime> holidaysList,
            int expectedCounts)
        {
            // Arrange
            var target = new DateCounter(_publicHolidayCalculator);

            // Act
            var actual = target.BusinessDaysBetweenTwoDates(firstDate, secondDate, holidaysList);

            // Assert
            actual.Should().Be(expectedCounts);
        }

        [Fact]
        public void BusinessDaysBetweenTwoDatesCustomHolidays_WhenPasseValidDatesAndCustomHoliday_ThenReturnsCorrectCount()
        {
            // Arrange
            var firstDate = new DateTime(2020, 4, 20);
            var secondDate = new DateTime(2020, 4, 30);
            var anzacDayHoliday = new PublicHoliday { Name = "Anzac Day", Month = 4, Day = 25, HappensOnWeekDay = true };
            var holidayList = new List<PublicHoliday> { anzacDayHoliday };
            var anzacDayDateIn2020 = new List<DateTime> { new DateTime(2020, 4, 27) };
            var year = new List<int> { 2020 };

            _publicHolidayCalculator.GetYearsBetweenDates(firstDate, secondDate).Returns(year);
            _publicHolidayCalculator.GetPublicHolidayByYears(year, anzacDayHoliday).Returns(anzacDayDateIn2020);

            var target = new DateCounter(_publicHolidayCalculator);

            // Act
            var actual = target.BusinessDaysBetweenTwoDatesCustomHolidays(firstDate, secondDate, holidayList);

            // Assert
            actual.Should().Be(6);
        }
    }
}