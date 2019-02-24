using BusinessDayCounterWebApp.Helpers;
using BusinessDayCounterWebApp.Models;
using BusinessDayCounterWebApp.Services.PublicHolidayCalculators;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace BusinessDayCounterWebApp.Services
{
    public class DateCounter : IDateCounter
    {
        private readonly IPublicHolidayCalculatorFactory _publicHolidayCalculatorFactory;
        private readonly IDateHelper _dateHelper;
        private readonly ILogger _logger;

        public DateCounter(IPublicHolidayCalculatorFactory publicHolidayCalculatorFactory, IDateHelper dateHelper, ILogger<DateCounter> logger)
        {
            _publicHolidayCalculatorFactory = publicHolidayCalculatorFactory;
            _dateHelper = dateHelper;
            _logger = logger;
        }

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<PublicHoliday> publicHolidays)
        {            
            if (firstDate > secondDate)
            {
                return 0;
            }

            List<int> yearsToCompare = _dateHelper.GetYearsBetweenDates(firstDate, secondDate);

            IPublicHolidayCalculator calculator = null;
            var convertedHolidaysList = new List<DateTime>();
           
            foreach (var holiday in publicHolidays)
            {
                try
                {
                    calculator = _publicHolidayCalculatorFactory.GetCalculator(holiday);
                    convertedHolidaysList.AddRange(calculator.GetPublicHolidayByYears(yearsToCompare, holiday));
                }
                catch (Exception e)
                {
                    _logger.LogError("An error occured when processing the custom holiday {0} with message {1}.", holiday.Name, e.Message);
                }
            }

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