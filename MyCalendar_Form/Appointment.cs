using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalendar_Form
{
    public class Appointment : IComparable<Appointment>
    {
        private string title;

        public string Title { get { return "" + title; } set
            {
                title = ("" + value[0]).ToUpper() + value.Substring(1);
            } }

        public int hour = 0, minute = 0;
        public bool wholeDay = false;
        public AppointmentType Type;

        public Appointment(int hour, int minute, string title, AppointmentType type)
        {
            this.hour = hour;
            this.minute = minute;
            this.Title = title;
            Type = type;
        }

        public Appointment(bool wholeDay, string title, AppointmentType type)
        {
            this.wholeDay = wholeDay;
            this.Title = title;
            Type = type;
        }

        public int CompareTo(Appointment obj)
        {
            if (wholeDay && !obj.wholeDay) return -1;
            if (!wholeDay && obj.wholeDay) return 1;
            if (hour < obj.hour) return -1;
            if (obj.hour < hour) return 1;
            if (minute < obj.minute) return -1;
            if (obj.minute < minute) return 1;
            return 0;
        }
    }

    public enum AppointmentType
    {
        Default,
        SchoolHoliday,
        SpecialDay        
    }
}
