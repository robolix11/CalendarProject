using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCalendar_Form.Controls
{
    class AppointmentButton : Control
    {
        private SolidBrush borderBrush = new SolidBrush(Color.Black), textBrush = new SolidBrush(Color.White);
        private Rectangle borderRect;
        private StringFormat stringFormat = new StringFormat();

        AppointmentType AppointmentType = AppointmentType.Default;

        public AppointmentButton()
        {
            Visible = true;
            this.Paint += AppointmentButton_Paint;

            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
        }

        private void AppointmentButton_Paint(object sender, PaintEventArgs e)
        {
            Font font = new Font("Arial", (Height / 3), FontStyle.Bold);

            Graphics g = e.Graphics;

            borderRect = new Rectangle(0, 0, Width, Height);
            g.Clip = new Region(borderRect);

            g.FillRectangle(new SolidBrush(Parent.BackColor), borderRect);
            borderRect.Inflate(-1, -1);
            g.DrawRectangle(new Pen(Color.Black), borderRect);
            g.DrawString(Text, font, new SolidBrush(Color.Cyan), borderRect, stringFormat);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            BackColor = Color.LightGray;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            BackColor = Parent.BackColor;
            base.OnMouseLeave(e);
        }
    }
}
