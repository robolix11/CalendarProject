using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCalendar_Form.Controls
{
    public class SaveAppoitmentsButton : Control
    {
        private SolidBrush borderBrush = new SolidBrush(Color.Black), textBrush = new SolidBrush(Color.White);
        private Rectangle borderRect;
        private StringFormat stringFormat = new StringFormat();

        public SaveAppoitmentsButton()
        {
            Visible = true;
            this.Paint += SaveAppoitmentsButton_Paint;

            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            Text = "Termine Speichern";
        }

        private void SaveAppoitmentsButton_Paint(object sender, PaintEventArgs e)
        {
            Font font = new Font("Arial", Height / 4, FontStyle.Bold);

            Graphics g = e.Graphics;

            borderRect = new Rectangle(0, 0, Width, Height);
            g.Clip = new Region(borderRect);

            g.FillRectangle(new SolidBrush(BackColor), borderRect);
            borderRect.Width--;
            borderRect.Height--;
            g.DrawRectangle(new Pen(Color.Black), borderRect);
            g.DrawString(Text, font, new SolidBrush(Color.Cyan), borderRect, stringFormat);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            BackColor = Color.FromArgb(70, 70, 70);
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
            ((MainForm)Parent).SaveAppoitmentsButton_Click(this, e);
            MessageBox.Show("Termine gespeichert!");
        }
    }
}
