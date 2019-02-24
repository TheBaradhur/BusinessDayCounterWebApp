using BusinessDayCounterWebApp.Models;
using BusinessDayCounterWebApp.Services.PublicHolidayCalculators;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace BusinessDayCounterWebApp.UnitTests.ServicesTests.PublicHolidayCalculatorsTests
{
    public class BasedOnOtherHolidayCalculatorTests
    {
        public static IEnumerable<object[]> PublicHolidaysListExpectations => new[]
        {
            new object[] { new List<int> { 2013, 2015 },
                new PublicHoliday {
                    Name = "Good Friday",
                    NumberOfDaysFromReference = -2,
                    ReferenceHolidayName = "Easter",
                    ReferenceHolidayDates = new List<DateTime> { new DateTime(2013, 3, 31), new DateTime(2014, 4, 20), new DateTime(2015, 4, 5) },
                },
                new List<DateTime> {
                    new DateTime(2013, 3, 29),
                    new DateTime(2015, 4, 3),
            }},
            
            new object[] { new List<int> { 2020 }, 
                new PublicHoliday
                {
                    Name = "Boxing Day",
                    NumberOfDaysFromReference = 1,
                    ReferenceHolidayName = "Christmas",
                    ReferenceHolidayDates = new List<DateTime> { new DateTime(2020, 12, 25) }
                },
                new List<DateTime> { new DateTime(2020, 12, 26) } },
        };

        [Fact]
        public void GetPublicHolidayByYears_WhenNullName_ThenThrows()
        {
            // Arrange
            var target = new BasedOnOtherHolidayCalculator();
            var holiday = new PublicHoliday { Name = null };

            // Act
            Action actual = () => target.GetPublicHolidayByYears(null, holiday);

            // Assert
            actual.Should().Throw<ArgumentException>().WithMessage("Cannot process a custom holidays without a name.");
        }

        [Fact]
        public void GetPublicHolidayByYears_WhenNullReferenceName_ThenThrows()
        {
            // Arrange
            var target = new BasedOnOtherHolidayCalculator();
            var holiday = new PublicHoliday { Name = "Name", ReferenceHolidayName = null };

            // Act
            Action actual = () => target.GetPublicHolidayByYears(null, holiday);

            // Assert
            actual.Should().Throw<ArgumentException>().WithMessage("You cannot have an empty ReferenceHolidayName for BasedOnOtherHoliday types.");
        }

        [Fact]
        public void GetPublicHolidayByYears_WhenNullNumberOfDaysFromReference_ThenThrows()
        {
            // Arrange
            var target = new BasedOnOtherHolidayCalculator();
            var holiday = new PublicHoliday { Name = "Name", ReferenceHolidayName = "ReferenceName", NumberOfDaysFromReference = null };

            // Act
            Action actual = () => target.GetPublicHolidayByYears(null, holiday);

            // Assert
            actual.Should().Throw<ArgumentException>().WithMessage("You cannot have an empty NumberOfDaysFromReference for BasedOnOtherHoliday types.");
        }

        [Theory]
        [MemberData(nameof(PublicHolidaysListExpectations))]
        public void GetPublicHolidayByYears_WhenPassedYearsAndHolidays_ThenReturnsExpectedDateList(List<int> years, PublicHoliday holiday, List<DateTime> expectedResult)
        {
            // Arrange
            var target = new BasedOnOtherHolidayCalculator();

            // Act
            var actual = target.GetPublicHolidayByYears(years, holiday);

            // Assert
            actual.Should().BeEquivalentTo(expectedResult);
        }
    }
}