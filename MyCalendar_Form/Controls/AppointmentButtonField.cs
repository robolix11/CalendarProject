using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCalendar_Form.Controls
{
    public class AppointmentButtonField : Control
    {
        private List<AppointmentButton> appointmentButtons = new List<AppointmentButton>();
        Form1 form;
        public AppointmentButtonField(Form1 form)
        {
            this.form = form;
            Visible = true;
        }

        public void SetAppointmentViewDate(CalendarDay calendarDay)
        {
            if(calendarDay == null)
            {
                foreach(AppointmentButton ab in appointmentButtons)
                {
                    ab.Hide();
                }
                return;
            }

            int index = 0;
            calendarDay.Appointments.Sort();
            foreach(Appointment a in calendarDay.Appointments)
            {
                if(appointmentButtons.Count < index+1)
                {
                    appointmentButtons.Add(new AppointmentButton());
                    appointmentButtons[index].SetBounds(0, Height / 7 * index, Width, Height / 7 - 5);

                    Controls.Add(appointmentButtons[index]);
                }
                appointmentButtons[index].Text = (a.wholeDay ? "--:--" : a.hour + ":" + a.minute) + "|" + a.title;
                appointmentButtons[index].Show();
                appointmentButtons[index].Refresh();
                index++;
            }
            for(int i = index; i < appointmentButtons.Count; i++)
            {
                appointmentButtons[i].Hide();
            }
        }
    }
}
