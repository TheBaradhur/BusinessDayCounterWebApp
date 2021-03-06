﻿using BusinessDayCounterWebApp.Models;
using System;
using System.Collections.Generic;

namespace BusinessDayCounterWebApp.Services.PublicHolidayCalculators
{
    public class FixedDayCalculator : IPublicHolidayCalculator
    {
        public List<DateTime> GetPublicHolidayByYears(List<int> years, PublicHoliday holiday)
        {
            if (string.IsNullOrEmpty(holiday.Name))
            {
                throw new ArgumentException("Cannot process a custom holidays without a name.");
            }

            var publicHolidaysList = new List<DateTime>();
            if (years == null)
            {
                return publicHolidaysList;
            }

            foreach (var year in years)
            {
                var publicHolidayDate = new DateTime(year, holiday.Month, holiday.Day);
                if (holiday.MustHappenOnAWeekDay && publicHolidayDate.DayOfWeek > DayOfWeek.Friday)
                {
                    var daysToAdd = publicHolidayDate.DayOfWeek == DayOfWeek.Saturday ? 2 : 1;
                    publicHolidayDate = publicHolidayDate.AddDays(daysToAdd);
                }

                publicHolidaysList.Add(publicHolidayDate);
            }

            return publicHolidaysList;
        }        
    }
}