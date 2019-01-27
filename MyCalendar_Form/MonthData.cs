using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalendar_Form
{
    class MonthData : IComparable<MonthData>
    {
        public int Year { get; }
        public int Month { get; }
        public CalendarDay[] CalendarDays;

        public MonthData(int year, int month)
        {
            Year = year;
            Month = month;


            int dayCount = GetDayCount();
            CalendarDays = new CalendarDay[dayCount];

            for(int i = 1; i <= dayCount; i++)
            {
                CalendarDays[i-1] = new CalendarDay(i, Month,Year);
            }
        }

        public DayOfWeek GetWeekdayOfFirstDay()
        {
            return new DateTime(Year, Month, 1).DayOfWeek;
        }

        public int GetDayCount()
        {
            DateTime dt = new DateTime(Month == 12?Year+1:Year, Month == 12?1: Month + 1, 1);
            dt = dt.Subtract(new TimeSpan(1, 0, 0, 0));
            return dt.Day;
        }

        public int GetDayCountBeforeMonth()
        {
            return new DateTime(Year, Month, 1).Subtract(new TimeSpan(1, 0, 0, 0)).Day;
        }

        public void AddAppointment(int day, Appointment a)
        {
            CalendarDays[day-1].AddAppointment(a);
        }

        public int CompareTo(MonthData other)
        {
            if(Year < other.Year)
            {
                return -1;
            }
            if (Year > other.Year)
            {
                return 1;
            }
            if (Month < other.Month)
            {
                return -1;
            }
            if (Month > other.Month)
            {
                return 1;
            }
            return 0;
        }
    }
}
