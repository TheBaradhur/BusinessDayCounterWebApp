namespace BusinessDayCounterWebApp.Models
{
    public enum PublicHolidayType
    {
        // Xmas day, new year's eve, ...
        FixedDate = 0,
        // Good friday (2 days before easter)
        BasedOnAnotherHoliday = 1,
        // US Inauguration day, 20 jan every 4 years
        RepeatEveryXYear = 2,
        // Easter specific
        Easter = 3,
        // Mother's day (sunday of second week in May)
        SpecificDayInWeek = 4,
    }
}