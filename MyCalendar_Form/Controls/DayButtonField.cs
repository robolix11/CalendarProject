using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCalendar_Form.Controls
{
    public class DayButtonField : Control
    {
        public List<DayButton> DayButtons = new List<DayButton>(42);
        public DayButton SelectedDayButton {
            get {
                return dayButton;
            }
            set {
                DayButton old = dayButton;
                dayButton = value;
                if (dayButton != null) dayButton.Refresh();
                if(old != null) old.Refresh();

                if (dayButton == null)
                {
                    abf.SetAppointmentViewDate(null);
                    return;
                }
                abf.SetAppointmentViewDate(form.Cache.GetMonthData(form.CurrentYear, form.CurrentMonth).CalendarDays[int.Parse(dayButton.Text) - 1]);
            }
        }

        public DateTime SelectedDay {
            get {
                if (SelectedDayButton == null) return DateTime.Today;
                return new DateTime(form.CurrentYear, form.CurrentMonth, int.Parse(SelectedDayButton.Text));
            }
        }

        DayButton dayButton;

        MainForm form;
        AppointmentButtonField abf;

        public DayButtonField(MainForm xForm, AppointmentButtonField appointmentButtonField)
        {
            form = xForm;
            abf = appointmentButtonField;

            Init();
        }

        public void Init()
        {
            for (int index = 0; index < 42; index++)
            {
                int i = index % 7;
                int j = index / 7;

                DayButton db = new DayButton(Width / 7 * i, Height / 6 * j, form, Width / 7 - 5, Height / 6 - 5);
                DayButtons.Add(db);
                Controls.Add(db);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            for (int index = 0; index < 42; index++)
            {
                int i = index % 7;
                int j = index / 7;

                DayButtons[index].SetBounds(Width / 7 * i, Height / 6 * j, Width / 7 - 5, Height / 6 - 5);
            }
        }

        internal void RefreshDay(int day)
        {
            DayButton db = DayButtons.Find(d => d.IsMainMonth && d.Text == "" + day);
            if (db == null) return;
            db.Refresh();
        }

        internal void RefreshAll()
        {
            DayButtons.ForEach(db => db.Refresh());
        }
    }
}
