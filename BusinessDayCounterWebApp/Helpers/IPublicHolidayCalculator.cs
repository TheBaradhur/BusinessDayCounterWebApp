using BusinessDayCounterWebApp.Models;
using System;
using System.Collections.Generic;

namespace BusinessDayCounterWebApp.Helpers
{
    public interface IPublicHolidayCalculator
    {
        List<DateTime> GetPublicHolidayByYears(List<int> years, PublicHoliday holiday);

        List<int> GetYearsBetweenDates(DateTime firstDate, DateTime secondDate);
    }
}