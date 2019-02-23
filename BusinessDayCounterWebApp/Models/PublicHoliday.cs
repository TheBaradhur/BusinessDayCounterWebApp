using System;
using System.Collections.Generic;

namespace BusinessDayCounterWebApp.Models
{
    public class PublicHoliday
    {
        public string Name { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public List<DateTime> GetDates(List<int> years)
        {
            var publicHolidaysList = new List<DateTime>();
            if (years == null)
            {
                return publicHolidaysList;
            }

            foreach (var year in years)
            {
                publicHolidaysList.Add(new DateTime(year, Month, Day));
            }

            return publicHolidaysList;
        }
    }
}