using BusinessDayCounterWebApp.Models;
using BusinessDayCounterWebApp.Services.PublicHolidayCalculators;
using System;

namespace BusinessDayCounterWebApp.Services
{
    public class PublicHolidayCalculatorFactory : IPublicHolidayCalculatorFactory
    {
        public IPublicHolidayCalculator GetCalculator(PublicHoliday holiday)
        {
            IPublicHolidayCalculator calculator = null;

            switch (holiday.HolidayType)
            {
                case PublicHolidayType.FixedDate:
                    calculator = new FixedDayCalculator();
                    break;
                case PublicHolidayType.BasedOnAnotherHoliday:
                    calculator = new BasedOnOtherHolidayCalculator();
                    break;
                case PublicHolidayType.RepeatEveryXYear:
                    calculator = new YearRepetitionCalculator();
                    break;
                case PublicHolidayType.Easter:
                    calculator = new EasterCalculator();
                    break;
                case PublicHolidayType.SpecificDayInWeek:
                    calculator = new SpecificWeekDayCalculator();
                    break;
                default:
                    throw new ArgumentException();
            }

            return calculator;
        }
    }
}