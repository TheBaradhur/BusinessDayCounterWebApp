using BusinessDayCounterWebApp.Helpers;
using BusinessDayCounterWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessDayCounterWebApp.Services
{
    public class DateCounter : IDateCounter
    {
        private readonly IPublicHolidayCalculator _publicHolidayCalculator;

        public DateCounter(IPublicHolidayCalculator publicHolidayCalculator)
        {
            _publicHolidayCalculator = publicHolidayCalculator;
        }

        public int BusinessDaysBetweenTwoDatesCustomHolidays(DateTime firstDate, DateTime secondDate, IList<PublicHoliday> publicHolidays)
        {            
            if (firstDate > secondDate)
            {
                return 0;
            }

            List<int> yearsToCompare = _publicHolidayCalculator.GetYearsBetweenDates(firstDate, secondDate);
            var convertedHolidaysList = publicHolidays.SelectMany(x => _publicHolidayCalculator.GetPublicHolidayByYears(yearsToCompare, x)).ToList();

            return BusinessDaysBetweenTwoDates(firstDate, secondDate, convertedHolidaysList);
        }

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            var businessDays = WeekdaysBetweenTwoDates(firstDate, secondDate);

            if (businessDays == 0)
                return businessDays;

            foreach (var publicHoliday in publicHolidays)
            {
                if (publicHoliday > firstDate && publicHoliday < secondDate)
                {
                    businessDays--;
                }
            }

            return businessDays;
        }

        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            var totalDaysCount = (secondDate - firstDate).Days - 1;

            if (totalDaysCount <= 0)
                return 0;

            var totalWeeksCount = totalDaysCount / 7;
            var weekDaysInFullWeek = totalWeeksCount * 5;

            var remainingDays = CalculateRemainingDays(firstDate, totalDaysCount);

            return weekDaysInFullWeek + remainingDays;
        }

        private int CalculateRemainingDays(DateTime firstDate, int totalDaysCount)
        {
            var remaingDays = totalDaysCount % 7;
            var daysToWeekend = DayOfWeek.Saturday - firstDate.DayOfWeek;
                       
            // Remaining days are within a week, before the weekend
            if (remaingDays <= daysToWeekend)
            {
                return remaingDays;
            }

            // Remaining days are within a week, but ends on weekend
            // Or they are stepping over the weekend
            if (remaingDays <= daysToWeekend + 2)
            {
                remaingDays = daysToWeekend;
            }
            else
            {
                remaingDays -= 2;
            }

            return remaingDays;
        }        
    }
}