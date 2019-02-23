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
            new object[] { new PublicHoliday { HolidayType = PublicHolidayType.FixedDate }, typeof(FixedPublicHolidayCalculator) },
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