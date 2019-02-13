using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCalendar_Form
{
    class MonthViewDrawer
    {
        MainForm f;
        Graphics g;


        public MonthViewDrawer(MainForm f)
        {
            this.f = f;
        }

        public void DrawMonthView(MonthData md, PaintEventArgs e)
        {
            g = e.Graphics;

            Font font = new Font("Arial", (f.ScreenHeight / 25), FontStyle.Bold);
            string MonthViewTitle = ((Months)(md.Month - 1)).ToString() + " " + md.Year;
            g.DrawString(MonthViewTitle, font, new SolidBrush(Color.White), (int)(f.ScreenWidth*0.5 - MonthViewTitle.Length*font.Size/3), 50);

            
            int startIndex = ((int)md.GetWeekdayOfFirstDay()-1+7)%7; //offset that monday is front
            int dayCount = md.GetDayCount();


            int dayCountBeforeMonth = md.GetDayCountBeforeMonth();
            for (int index = startIndex-1; index >= 0; index--)
            {
                f.dbf.DayButtons[index].Text = ""+ (dayCountBeforeMonth - (startIndex - index - 1));
                f.dbf.DayButtons[index].TextColor = Color.Gray;
                //f.dbf.DayButtons[index].IsScoolHoliday = false;
                f.dbf.DayButtons[index].IsNationalHoliday = false;
                f.dbf.DayButtons[index].IsMainMonth = false;
                //DrawDayAtIndex(index, dayCountBeforeMonth - (startIndex - index-1), Color.FromArgb(150, 150, 150), Color.Black, Color.FromArgb(100,100,100));
            }

            for (int index = startIndex; index < startIndex+dayCount; index++)
            {
                f.dbf.DayButtons[index].Text = "" + (index - startIndex + 1);
                f.dbf.DayButtons[index].TextColor = Color.Cyan;
                f.dbf.DayButtons[index].IsMainMonth = true;

                CalendarDay calendarDay = md.CalendarDays[(index - startIndex + 1)-1];
                List<Appointment> appointments = calendarDay.Appointments.FindAll(a => a.Type == AppointmentType.SchoolHoliday || a.Type == AppointmentType.SpecialDay);

                //f.dbf.DayButtons[index].IsScoolHoliday = appointments.FindAll(a => a.Type == AppointmentType.SchoolHoliday).Count <= 0 ? false : true;
                f.dbf.DayButtons[index].IsNationalHoliday = appointments.FindAll(a => a.Type == AppointmentType.SpecialDay).Count <= 0 ? false : true;
            }

            int dayAfterMonth = 1;
            for (int index = startIndex + dayCount; index < 42; index++)
            {
                f.dbf.DayButtons[index].Text = "" + (dayAfterMonth);
                f.dbf.DayButtons[index].TextColor = Color.Gray;
                //f.dbf.DayButtons[index].IsScoolHoliday = false;
                f.dbf.DayButtons[index].IsNationalHoliday = false;
                f.dbf.DayButtons[index].IsMainMonth = false;
                dayAfterMonth++;
            }

            g.Dispose();
        }  
        
        public void Rescale()
        {
            for (int index = 0; index < 42; index++)
            {
                int i = index % 7;
                int j = index / 7;

                int x = i * (f.ScreenWidth / 10) + (int)((f.ScreenWidth / 10) * 1.5);
                int y = j * (f.ScreenHeight / 10) + f.ScreenHeight / 3;

                f.dbf.DayButtons[index].SetBounds(x, y, (f.ScreenWidth / 10) - 5, (f.ScreenHeight / 10) - 5);
            }
        }
    }
}
