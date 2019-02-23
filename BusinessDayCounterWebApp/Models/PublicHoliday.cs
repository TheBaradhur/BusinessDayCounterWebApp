namespace BusinessDayCounterWebApp.Models
{
    public class PublicHoliday
    {
        public string Name { get; set; }

        public PublicHolidayType HolidayType { get; set; }

        public int Month { get; set; }

        public int WeekInTheMonth { get; set; }

        public int Day { get; set; }
        
        public bool HappensOnWeekDay { get; set; } = false;
    }
}