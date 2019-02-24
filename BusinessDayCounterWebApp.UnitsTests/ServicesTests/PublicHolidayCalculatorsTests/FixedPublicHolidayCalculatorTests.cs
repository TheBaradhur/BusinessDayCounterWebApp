using BusinessDayCounterWebApp.Models;
using BusinessDayCounterWebApp.Services.PublicHolidayCalculators;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace BusinessDayCounterWebApp.UnitTests.ServicesTests.PublicHolidayCalculatorsTests
{
    public class FixedPublicHolidayCalculatorTests
    {
        
        public static IEnumerable<object[]> PublicHolidaysListExpectations => new[]
        {
            new object[] { new List<int> { 2013 },
                new PublicHoliday { Name = "Christmas", Month = 12, Day = 25 },
                new List<DateTime> { new DateTime(2013, 12, 25) } },

            new object[] { new List<int> { 2013, 2014, 2015 },
                new PublicHoliday { Name = "Christmas", Month = 12, Day = 25 },
                new List<DateTime> {
                    new DateTime(2013, 12, 25),
                    new DateTime(2014, 12, 25),
                    new DateTime(2015, 12, 25),
            }},
            
            // Anzac day falls on saturday in 2020
            new object[] { new List<int> { 2020 }, 
                new PublicHoliday { Name = "Anzac Day", Month = 4, Day = 25, MustHappenOnAWeekDay = true },
                new List<DateTime> { new DateTime(2020, 4, 27) } },
        };
        
        [Theory]
        [MemberData(nameof(PublicHolidaysListExpectations))]
        public void GetPublicHolidayByYears_WhenPassedYearsAndHOlidays_ThenReturnsExpectedDateList(List<int> years, PublicHoliday holiday, List<DateTime> expectedResult)
        {
            // Arrange
            var target = new FixedDayCalculator();

            // Act
            var actual = target.GetPublicHolidayByYears(years, holiday);

            // Assert
            actual.Should().BeEquivalentTo(expectedResult);
        }
    }
}