using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCalendar_Form.Controls
{
    public class AddAppointmentButton : Control
    {
        private SolidBrush borderBrush = new SolidBrush(Color.Black), textBrush = new SolidBrush(Color.White);
        private Rectangle borderRect;
        private StringFormat stringFormat = new StringFormat();
        
        public AddAppointmentButton()
        {
            Visible = true;
            this.Paint += AddAppointmentButton_Paint;

            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            Text = "+";
        }

        private void AddAppointmentButton_Paint(object sender, PaintEventArgs e)
        {
            Font font = new Font("Arial", (float)(Height/1.5), FontStyle.Bold);

            Graphics g = e.Graphics;

            borderRect = new Rectangle(0, 0, Width, Height);
            g.Clip = new Region(borderRect);

            g.FillRectangle(new SolidBrush(BackColor), borderRect);
            borderRect.Inflate(-1, -1);
            g.DrawRectangle(new Pen(Color.Black), borderRect);
            g.DrawString(Text, font, new SolidBrush(Color.Cyan), borderRect, stringFormat);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            BackColor = Color.White;
            base.OnMouseEnter(e);
            Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            BackColor = Parent.BackColor;
            base.OnMouseLeave(e);
            Refresh();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            ((Form1)Parent).AddAppointmentButton_Click(this, e);
        }
    }
}
