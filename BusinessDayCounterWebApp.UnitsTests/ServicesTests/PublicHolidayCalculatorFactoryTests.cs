using BusinessDayCounterWebApp.Models;
using BusinessDayCounterWebApp.Services;
using BusinessDayCounterWebApp.Services.PublicHolidayCalculators;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace BusinessDayCounterWebApp.UnitTests.ServicesTests
{
    public class PublicHolidayCalculatorFactoryTests
    {
        public static IEnumerable<object[]> PublicHolidaysTypeList => new[]
        {
            new object[] { new PublicHoliday { HolidayType = PublicHolidayType.FixedDate }, typeof(FixedDayCalculator) },
            new object[] { new PublicHoliday { HolidayType = PublicHolidayType.BasedOnAnotherHoliday }, typeof(BasedOnOtherHolidayCalculator) },
            new object[] { new PublicHoliday { HolidayType = PublicHolidayType.Easter }, typeof(EasterCalculator) },
            new object[] { new PublicHoliday { HolidayType = PublicHolidayType.SpecificDayInWeek }, typeof(SpecificWeekDayCalculator) },
            new object[] { new PublicHoliday { HolidayType = PublicHolidayType.RepeatEveryXYear }, typeof(YearRepetitionCalculator) },
        };

        [Theory]
        [MemberData(nameof(PublicHolidaysTypeList))]
        public void GetCalculator_WhenPassedHoliday_ThenReturnsCorrectCalculator(PublicHoliday publicHoliday, Type typeOfCalculator)
        {
            // Arrange
            var target = new PublicHolidayCalculatorFactory();

            // Act
            var actual = target.GetCalculator(publicHoliday);

            // Assert
            actual.GetType().Should().Be(typeOfCalculator);
        }
    }
}