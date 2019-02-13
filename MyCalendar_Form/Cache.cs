using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCalendar_Form
{
    public class Cache
    {
        public List<MonthData> monthData = new List<MonthData>();

        public List<Tuple<Appointment, int, int>> YearlyAppointments = new List<Tuple<Appointment, int, int>>();
        public List<Tuple<Appointment, int>> MonthlyAppointments = new List<Tuple<Appointment, int>>();
        public List<Appointment> DaylyAppointments = new List<Appointment>();

        MainForm form;

        public Cache(MainForm form)
        {
            this.form = form;
        }

        public MonthData GetMonthData(int year, int month)
        {
            List<MonthData> yearOccurences = monthData.FindAll(md => md.Year == year);
            if (yearOccurences.Count == 0)
            {
                AddScoolHolidays(year);
                AddNationalHolidays(year);
            }

            MonthData next = yearOccurences.Find(md => md.Month == month);
            if(next == null)
            {
                next = new MonthData(year, month);
                monthData.Add(next);
            }
            else
            {
                DeleteRepeatingAppointmentsFromMonthData(next);
            }
            AddRepeatingAppointmentsToMonthData(next, month);
            return next;
        }

        

        public void AddAppointment(string year, string month, string day, Appointment a)
        {
            if (day == "-")
            {
                a.repeating = true;
                DaylyAppointments.Add(a);
                return;
            }

            int intDay = int.Parse(day);

            if (month == "-")
            {
                a.repeating = true;
                MonthlyAppointments.Add(new Tuple<Appointment,int>(a,intDay));
                return;
            }
            
            int intMonth = int.Parse(month);

            if (year == "-")
            {
                a.repeating = true;
                YearlyAppointments.Add(new Tuple<Appointment, int, int>(a, intDay, intMonth));
                return;
            }

            int intYear = int.Parse(year);

            MonthData md = GetMonthData(intYear, intMonth);
            md.AddAppointment(intDay, a);
        }

        private void AddRepeatingAppointmentsToMonthData(MonthData md, int month)
        {
            foreach(Appointment a in DaylyAppointments)
            {
                foreach(CalendarDay cd in md.CalendarDays)
                {
                    cd.AddAppointment(a);
                }
            }

            foreach(Tuple<Appointment,int> tuple in MonthlyAppointments)
            {
                for(int i = 0; i < md.CalendarDays.Count; i++)
                {
                    if (tuple.Item2 != i + 1) continue;
                    md.CalendarDays[i].AddAppointment(tuple.Item1);
                }
            }

            foreach (Tuple<Appointment, int, int> tuple2 in YearlyAppointments)
            {
                if (tuple2.Item3 != month) continue;
                for (int i = 0; i < md.CalendarDays.Count; i++)
                {
                    if (tuple2.Item2 != i + 1) continue;
                    md.CalendarDays[i].AddAppointment(tuple2.Item1);
                }
            }
        }

        private void DeleteRepeatingAppointmentsFromMonthData(MonthData md)
        {
            foreach(CalendarDay cd in md.CalendarDays)
            {
                cd.Appointments.RemoveAll(a => a.repeating);
            }
        }

        public async void AddScoolHolidays(int Year)
        {
            ScoolHolidayModel[] scoolHolidayModels = await ScoolHolidayProvider.GetHollidays(Year);

            try
            {
                scoolHolidayModels = await ScoolHolidayProvider.GetHollidays(Year);
            }
            catch(Exception e)
            {
                return;
            }
            if (scoolHolidayModels == null) return;

            foreach (ScoolHolidayModel shm in scoolHolidayModels)
            {
                DateTime start = Helper.DateTimeHelper.ConverStringToDateTime(shm.start);
                start = Helper.DateTimeHelper.SetTimePartZero(start);
                DateTime end = Helper.DateTimeHelper.ConverStringToDateTime(shm.end);
                end = Helper.DateTimeHelper.SetTimePartZero(end);

                DateTime current = new DateTime(start.Year, start.Month, start.Day, 0, 0, 0);
                while(current.CompareTo(end) <= 0)
                {
                    form.AddAppointment(""+current.Year, ""+current.Month, ""+current.Day, new Appointment(true, shm.name, AppointmentType.SchoolHoliday));
                    current = current.AddDays(1);
                }

                //MessageBox.Show("" + shm.start);
            }
        }

        public async void AddNationalHolidays(int Year)
        {
            Dictionary<string, NationalHoliday> _Result;

            try
            {
                _Result = await NationalHolidayProvider.GetHollidays(Year);
            }catch(Exception e)
            {
                //MessageBox.Show(e.StackTrace);
                return;
            }
            if (_Result == null) return;

            foreach (KeyValuePair<string, NationalHoliday> entry in _Result)
            {
                string dateString = entry.Value.datum;
                string[] _Split = dateString.Split('-');
                form.AddAppointment(_Split[0], _Split[1], _Split[2], new Appointment(true, entry.Key, AppointmentType.SpecialDay));
            }
        }
    }
}
