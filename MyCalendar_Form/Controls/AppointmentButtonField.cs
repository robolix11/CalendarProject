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
        int offset = 0;

        private List<AppointmentButton> appointmentButtons = new List<AppointmentButton>();
        MainForm form;
        public AppointmentButtonField(MainForm form)
        {
            this.form = form;
            Visible = true;
            this.MouseWheel += AppointmentButtonField_MouseWheel;
        }

        private void AppointmentButtonField_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                offset = offset > 0 ? offset - 1: 0;
            }
            else
            {
                if (appointmentButtons.Count - offset <= 7) return;
                offset++;
            }

            for (int i = 0; i < appointmentButtons.Count; i++) { 
                appointmentButtons[i].SetBounds(0, Height / 7 * (i - offset), Width, Height / 7 - 5);
            }
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
                appointmentButtons[index].Text = (a.wholeDay ? "--:--" : "" + (a.hour.ToString().Length < 2 ? "0"+a.hour:""+a.hour)  + " : " + (a.minute.ToString().Length < 2 ? "0" + a.minute :"" + a.minute)) + " | " + a.Title;
                appointmentButtons[index].Show();
                appointmentButtons[index].Refresh();
                index++;
            }
            for(int i = index; i < appointmentButtons.Count; i++)
            {
                appointmentButtons[i].Hide();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            for(int i = 0; i < appointmentButtons.Count; i++)
            {
                AppointmentButton ab = appointmentButtons[i];
                ab.SetBounds(0, Height / 7 * i, Width, Height / 7 - 5);
                ab.Refresh();
            }
        }
    }
}
