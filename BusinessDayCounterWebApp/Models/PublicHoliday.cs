using System;
using System.Collections.Generic;

namespace BusinessDayCounterWebApp.Models
{
    public class PublicHoliday
    {
        public string Name { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public bool HappensOnWeekDay { get; set; } = false;        
    }
}