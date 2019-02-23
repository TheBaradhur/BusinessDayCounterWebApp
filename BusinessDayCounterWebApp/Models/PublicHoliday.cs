using System;

namespace BusinessDayCounterWebApp.Models
{
    /*
     * Custom public holiday template
     * Based on the type, specific calculators will compute the actual date
     * Flat structure to ease the serialization and API Calls
     */
    public class PublicHoliday
    {
        public string Name { get; set; }

        public PublicHolidayType HolidayType { get; set; } = 0;

        public int Month { get; set; }

        public int Day { get; set; }
        
        public bool MustHappenOnAWeekDay { get; set; } = false;

        // BasedOnAnotherHoliday
        public int? NumberOfDaysFromReference { get; set; }

        // SpecificDayInWeek
        public int? WeekInTheMonth { get; set; }

        public DayOfWeek? DayInWeek { get; set; }

        // RepeatEveryXYear
        public DateTime? StartingRecursionDate { get; set; }

        public int? YearRepetitionRate { get; set; }

        // Easter
        public EasterHolidayType? EasterType { get; set; } = 0;
    }
}