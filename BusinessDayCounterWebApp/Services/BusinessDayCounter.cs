using System;
using System.Collections.Generic;

namespace BusinessDayCounterWebApp.Services
{
    public class BusinessDayCounter : IBusinessDayCounter
    {
        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            throw new NotImplementedException();
        }

        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            throw new NotImplementedException();
        }
    }
}
