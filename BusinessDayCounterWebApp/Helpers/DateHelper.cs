using System;
using System.Collections.Generic;

namespace BusinessDayCounterWebApp.Helpers
{
    public class DateHelper : IDateHelper
    {
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