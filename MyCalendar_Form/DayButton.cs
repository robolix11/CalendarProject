﻿using System;
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
        private SolidBrush borderBrush = new SolidBrush(Color.Black), textBrush = new SolidBrush(Color.Cyan);
        private Rectangle borderRect;
        private bool active = false, mouse_over = false;
        private StringFormat stringFormat = new StringFormat();

        public Color TextColor { get { return textBrush.Color; } set { textBrush.Color = value; } }
        public Color BorderColor { get { return borderBrush.Color; } set { borderBrush.Color = value; } }
        public Color NationalHolidayMarkerColor = Color.Gold;
        public Color ScoolHolidayMarkerColor = Color.Lime;

        public bool IsScoolHoliday = false, IsNationalHoliday = false;

        public override Cursor Cursor { get; set; } = Cursors.Default;
        public float BorderThickness { get; set; } = 2;

        public DayButton(int x, int y, int width = 100, int height = 100)
        {
            //this.SetLocation(x, y);
            //this.SetSize(width, height);
            this.SetBounds(x, y, width, height);
            this.Visible = true;
            
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            this.Paint += DayButton_Paint;
        }
        

        private void DayButton_Paint(object sender, PaintEventArgs e)
        {
            Font font = new Font("Arial", (Height / 3), FontStyle.Bold);

            //borderRect = new Rectangle(this.Location.X-Width/2, this.Location.Y-Height/2, Width, Height);
            //borderRect = new Rectangle(this.Location.X, this.Location.Y, Width, Height);
            borderRect = new Rectangle(0, 0, Width, Height);
            e.Graphics.Clip = new Region(borderRect);

            e.Graphics.DrawRectangle(new Pen(borderBrush, BorderThickness), borderRect);
            e.Graphics.DrawString(this.Text, font, textBrush, borderRect, stringFormat);

            if (!(IsScoolHoliday || IsNationalHoliday)) return;


            Rectangle innerBorderRect = new Rectangle(5,5,Width-10,Height-10);
            e.Graphics.DrawRectangle(new Pen(textBrush, BorderThickness), innerBorderRect);
            
        }


        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            MessageBox.Show("i was clicked!");
            active = true;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            BackColor = Color.Gray;
            base.OnMouseEnter(e);
            mouse_over = true;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            BackColor = Parent.BackColor;
            base.OnMouseLeave(e);
            mouse_over = false;
        }

    }
}
