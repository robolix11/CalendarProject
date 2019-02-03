using MyCalendar_Form.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCalendar_Form
{
    public class DayButton : Control
    {
        private SolidBrush borderBrush = new SolidBrush(Color.Black), textBrush = new SolidBrush(Color.White);
        private Rectangle borderRect;
        private StringFormat stringFormat = new StringFormat();

        public Color TextColor { get { return textBrush.Color; } set { textBrush.Color = value; } }
        public Color BorderColor { get { return borderBrush.Color; } set { borderBrush.Color = value; } }
        public Color NationalHolidayMarkerColor = Color.Gold, ScoolHolidayMarkerColor = Color.Lime, ActiveColor = Color.Black;

        private Color PassiveColor = Color.Gray;

        public bool IsSelected
        {
            get { return ((DayButtonField)Parent).SelectedDayButton == null ? false : ((DayButtonField)Parent).SelectedDayButton.Equals(this); }
        }
        public bool IsMainMonth = false;

        public bool IsScoolHoliday {
            get {
                if (!IsMainMonth) return false;
                Form1 form = (Form1)(Parent.Parent);
                return form.Cache.GetMonthData(form.CurrentYear, form.CurrentMonth).CalendarDays[int.Parse(Text)-1].Appointments.FindAll(a => a.Type == AppointmentType.SchoolHoliday).Count > 0;
            }
        }
        public bool IsNationalHoliday = false;

        public override Cursor Cursor { get; set; } = Cursors.Default;
        public float BorderThickness { get; set; } = 2;

        private Form1 mForm;

        public DayButton(int x, int y, Form1 f,int width = 100, int height = 100)
        {
            mForm = f;
            //this.SetLocation(x, y);
            //this.SetSize(width, height);
            this.SetBounds(x, y, width, height);
            this.Visible = true;
            
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            PassiveColor = mForm.BackColor;

            this.Paint += DayButton_Paint;
        }
        

        private void DayButton_Paint(object sender, PaintEventArgs e)
        {
            Font font = new Font("Arial", (Height / 3), FontStyle.Bold);

            BackColor = IsSelected ? ActiveColor : PassiveColor;

            borderRect = new Rectangle(0, 0, Width, Height);
            e.Graphics.Clip = new Region(borderRect);

            e.Graphics.DrawRectangle(new Pen(borderBrush, BorderThickness), borderRect);
            e.Graphics.DrawString(this.Text, font, textBrush, borderRect, stringFormat);

            bool schoolHoliday = IsScoolHoliday;
            bool nationalHoliday = IsNationalHoliday;
            if (!(schoolHoliday || nationalHoliday)) return;

            Rectangle innerBorderRect = new Rectangle(5,5,Width-10,Height-10);
            e.Graphics.DrawRectangle(new Pen(nationalHoliday ? NationalHolidayMarkerColor : ScoolHolidayMarkerColor, BorderThickness), innerBorderRect);
            
        }


        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (!IsMainMonth) return;

            ((DayButtonField)Parent).SelectedDayButton = this;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            PassiveColor = Color.LightGray;

            base.OnMouseEnter(e);

            Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            PassiveColor = mForm.BackColor;

            base.OnMouseLeave(e);

            Refresh();
        }

    }
}
