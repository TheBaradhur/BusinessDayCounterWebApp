using BusinessDayCounterWebApp.Models;
using BusinessDayCounterWebApp.Services.PublicHolidayCalculators;

namespace BusinessDayCounterWebApp.Services
{
    public interface IPublicHolidayCalculatorFactory
    {
        IPublicHolidayCalculator GetCalculator(PublicHoliday holiday);
    }
}