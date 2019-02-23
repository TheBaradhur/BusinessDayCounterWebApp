using BusinessDayCounterWebApp.Models;
using System;
using System.Collections.Generic;

namespace BusinessDayCounterWebApp.Services.PublicHolidayCalculators
{
    public interface IPublicHolidayCalculator
    {
        List<DateTime> GetPublicHolidayByYears(List<int> years, PublicHoliday holiday);
    }
}