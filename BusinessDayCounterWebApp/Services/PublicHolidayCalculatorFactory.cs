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
                    calculator = new FixedPublicHolidayCalculator();
                    break;
                case PublicHolidayType.BasedOnAnotherHoliday:
                    throw new NotImplementedException();
                    break;
                case PublicHolidayType.RepeatEveryXYear:
                    throw new NotImplementedException();
                    break;
                case PublicHolidayType.Easter:
                    throw new NotImplementedException();
                    break;
                case PublicHolidayType.SpecificDayInWeek:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }

            return calculator;
        }
    }
}