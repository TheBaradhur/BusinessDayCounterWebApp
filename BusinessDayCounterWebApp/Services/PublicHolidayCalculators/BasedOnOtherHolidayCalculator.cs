using BusinessDayCounterWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessDayCounterWebApp.Services.PublicHolidayCalculators
{
    public class BasedOnOtherHolidayCalculator : IPublicHolidayCalculator
    {
        public List<DateTime> GetPublicHolidayByYears(List<int> years, PublicHoliday holiday)
        {
            if (string.IsNullOrEmpty(holiday.Name))
            {
                throw new ArgumentException("Cannot process a custom holidays without a name.");
            }

            if (string.IsNullOrEmpty(holiday.ReferenceHolidayName))
            {
                throw new ArgumentException("You cannot have an empty ReferenceHolidayName for BasedOnOtherHoliday types.");
            }

            if (holiday.NumberOfDaysFromReference == null)
            {
                throw new ArgumentException("You cannot have an empty NumberOfDaysFromReference for BasedOnOtherHoliday types.");
            }

            var holidayDatesList = new List<DateTime>();
            if (years == null)
            {
                return holidayDatesList;
            }

            foreach (var referenceDate in holiday.ReferenceHolidayDates)
            {
                if (years.Any(x => x == referenceDate.Year))
                {
                    holidayDatesList.Add(referenceDate.AddDays(holiday.NumberOfDaysFromReference ?? 0));
                }                
            }

            return holidayDatesList;
        }
    }
}