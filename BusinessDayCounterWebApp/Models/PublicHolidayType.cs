namespace BusinessDayCounterWebApp.Models
{
    public enum PublicHolidayType
    {
        FixedDate = 0,
        BasedOnAnotherDate = 1,
        RepeatEveryXYear = 2,
        Easter = 3
    }
}