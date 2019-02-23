using System;
using System.Collections.Generic;

namespace BusinessDayCounterWebApp.Services
{
    public interface IDateCounter
    {
        int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate);

        int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays);
    }
}