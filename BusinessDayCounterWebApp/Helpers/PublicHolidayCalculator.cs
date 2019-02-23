using BusinessDayCounterWebApp.Models;
using System;
using System.Collections.Generic;

namespace BusinessDayCounterWebApp.Helpers
{
    public class PublicHolidayCalculator : IPublicHolidayCalculator
    {
        public List<DateTime> GetPublicHolidayByYears(List<int> years, PublicHoliday holiday)
        {
            var publicHolidaysList = new List<DateTime>();
            if (years == null)
            {
                return publicHolidaysList;
            }

            foreach (var year in years)
            {
                var publicHolidayDate = new DateTime(year, holiday.Month, holiday.Day);
                if (holiday.HappensOnWeekDay && publicHolidayDate.DayOfWeek > DayOfWeek.Friday)
                {
                    var daysToAdd = publicHolidayDate.DayOfWeek == DayOfWeek.Saturday ? 2 : 1;
                    publicHolidayDate = publicHolidayDate.AddDays(daysToAdd);
                }

                publicHolidaysList.Add(publicHolidayDate);
            }

            return publicHolidaysList;
        }

        public List<int> GetYearsBetweenDates(DateTime firstDate, DateTime secondDate)
        {
            var yearsList = new List<int>();
            var iterator = new DateTime(firstDate.Year, firstDate.Month, firstDate.Day);

            while (iterator.Year <= secondDate.Year)
            {
                yearsList.Add(iterator.Year);
                iterator = iterator.AddYears(1);
            }

            return yearsList;
        }
    }
}