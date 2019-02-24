using BusinessDayCounterWebApp.Models;
using System;
using System.Collections.Generic;

namespace BusinessDayCounterWebApp.Services.PublicHolidayCalculators
{
    public class EasterCalculator : IPublicHolidayCalculator
    {
        public List<DateTime> GetPublicHolidayByYears(List<int> years, PublicHoliday holiday)
        {
            var easterHolidays = new List<DateTime>();

            foreach (var year in years)
            {
                easterHolidays.Add(holiday.EasterType == EasterHolidayType.CatholicEaster ?
                    GetCatholicEasterHolidays(year)
                    :
                    GetOrthodoxEasterHolidays(year));
            }

            return easterHolidays;
        }

        // Both formulas found here: https://mycodepad.wordpress.com/2013/04/28/c-calculating-orthodox-and-catholic-easter/        
        private DateTime GetOrthodoxEasterHolidays(int year)
        {
            int a = year % 19;
            int b = year % 7;
            int c = year % 4;

            int d = (19 * a + 16) % 30;
            int e = (2 * c + 4 * b + 6 * d) % 7;
            int f = (19 * a + 16) % 30;
            int key = f + e + 3;

            int month = (key > 30) ? 5 : 4;
            int day = (key > 30) ? key - 30 : key;

            return new DateTime(year, month, day);
        }

        private DateTime GetCatholicEasterHolidays(int year)
        {
            int month = 3;
            int G = year % 19 + 1;
            int C = year / 100 + 1;
            int X = (3 * C) / 4 - 12;
            int Y = (8 * C + 5) / 25 - 5;
            int Z = (5 * year) / 4 - X - 10;
            int E = (11 * G + 20 + Y - X) % 30;
            if (E == 24) { E++; }
            if ((E == 25) && (G > 11)) { E++; }
            int N = 44 - E;
            if (N < 21) { N = N + 30; }
            int P = (N + 7) - ((Z + N) % 7);
            if (P > 31)
            {
                P = P - 31;
                month = 4;
            }
            return new DateTime(year, month, P);
        }
    }
}