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
        List<MonthData> monthData = new List<MonthData>();

        Form1 form;

        public Cache(Form1 form)
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
            return next;
        }

        public void AddAppointment(int year, int month, int day, Appointment a)
        {
            MonthData md = GetMonthData(year, month);
            md.AddAppointment(day, a);
        }

        public async void AddScoolHolidays(int Year)
        {
            ScoolHolidayModel[] scoolHolidayModels = await ScoolHolidayProvider.GetHollidays(Year);

            foreach (ScoolHolidayModel shm in scoolHolidayModels)
            {
                DateTime start = Helper.DateTimeHelper.ConverStringToDateTime(shm.start);
                start = Helper.DateTimeHelper.SetTimePartZero(start);
                DateTime end = Helper.DateTimeHelper.ConverStringToDateTime(shm.end);
                end = Helper.DateTimeHelper.SetTimePartZero(end);

                DateTime current = new DateTime(start.Year, start.Month, start.Day, 0, 0, 0);
                while(current.CompareTo(end) <= 0)
                {
                    form.AddAppointment(current.Year, current.Month, current.Day, new Appointment(true, shm.name, AppointmentType.SchoolHoliday));
                    current = current.AddDays(1);
                }

                //MessageBox.Show("" + shm.start);
            }
        }

        public async void AddNationalHolidays(int Year)
        {
            Dictionary<string, NationalHoliday> _Result = await NationalHolidayProvider.GetHollidays(Year);
            foreach(KeyValuePair<string, NationalHoliday> entry in _Result)
            {
                string dateString = entry.Value.datum;
                string[] _Split = dateString.Split('-');
                form.AddAppointment(int.Parse(_Split[0]), int.Parse(_Split[1]), int.Parse(_Split[2]), new Appointment(true, entry.Key, AppointmentType.SpecialDay));
            }
        }
    }
}
