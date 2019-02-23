using System;
using System.Collections.Generic;

namespace BusinessDayCounterWebApp.Helpers
{
    public interface IDateHelper
    {
        List<int> GetYearsBetweenDates(DateTime firstDate, DateTime secondDate);
    }
}