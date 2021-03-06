﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalendar_Form
{
    public class CalendarDay
    {
        public int Day { get; }
        public int Month { get; }
        public int Year{ get; }
        public List<Appointment> Appointments = new List<Appointment>();

        public CalendarDay(int day, int month, int year)
        {
            this.Day = day;
            this.Month = month;
            this.Year = year;


            //Added becouse not actual Holidays, but higly prefered to have on the List
            if(day == 31 && month == 12)
            {
                AddAppointment(new Appointment(true, "Silvester", AppointmentType.SpecialDay));
            }

            if (day == 24 && month == 12)
            {
                AddAppointment(new Appointment(true, "Heiligabend", AppointmentType.SpecialDay));
            }
        }

        public void AddAppointment(Appointment a)
        {
            Appointments.Add(a);
            Appointments.Sort();
        }
    }
}
