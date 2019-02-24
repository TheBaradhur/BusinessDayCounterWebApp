using BusinessDayCounterWebApp.Models;
using BusinessDayCounterWebApp.Services.PublicHolidayCalculators;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace BusinessDayCounterWebApp.UnitTests.ServicesTests.PublicHolidayCalculatorsTests
{
    public class EasterCalculatorTests
    {
        // Date reference found on wikipedia: https://en.wikipedia.org/wiki/List_of_dates_for_Easter
        public static IEnumerable<object[]> PublicHolidaysListExpectations => new[]
        {
            new object[] { new List<int> { 2013, 2014, 2015 },
                new PublicHoliday { Name = "Catholic Easter", EasterType = EasterHolidayType.CatholicEaster },
                new List<DateTime> {
                    new DateTime(2013, 3, 31),
                    new DateTime(2014, 4, 20),
                    new DateTime(2015, 4, 5),
            }},
            
            // Anzac day falls on saturday in 2020
            new object[] { new List<int> { 2020 }, 
                new PublicHoliday { Name = "Orthodox Easter", EasterType = EasterHolidayType.OrthodoxEaster },
                new List<DateTime> { new DateTime(2020, 4, 19) } },
        };

        [Fact]
        public void GetPublicHolidayByYears_WhenNullName_ThenThrows()
        {
            // Arrange
            var target = new EasterCalculator();
            var holiday = new PublicHoliday { Name = null };

            // Act
            Action actual = () => target.GetPublicHolidayByYears(null, holiday);

            // Assert
            actual.Should().Throw<ArgumentException>().WithMessage("Cannot process a custom holidays without a name.");
        }

        [Theory]
        [MemberData(nameof(PublicHolidaysListExpectations))]
        public void GetPublicHolidayByYears_WhenPassedYearsAndHolidays_ThenReturnsExpectedDateList(List<int> years, PublicHoliday holiday, List<DateTime> expectedResult)
        {
            // Arrange
            var target = new EasterCalculator();

            // Act
            var actual = target.GetPublicHolidayByYears(years, holiday);

            // Assert
            actual.Should().BeEquivalentTo(expectedResult);
        }
    }
}