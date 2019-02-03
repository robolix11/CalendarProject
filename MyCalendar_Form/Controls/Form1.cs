using MyCalendar_Form.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MyCalendar_Form
{
    public partial class Form1 : Form
    {
        public int ScreenWidth        {get { return ClientSize.Width; }        }
        public int ScreenHeight {get { return ClientSize.Height; } }

        public int CurrentYear = DateTime.Now.Year;
        public int CurrentMonth = DateTime.Now.Month;

        MonthViewDrawer mvd;
        public Cache Cache;
        //public DayButton[] DayButtons = new DayButton[42];
        public DayButtonField dbf;
        public AppointmentButtonField abf;

        public Form1()
        {
            InitializeComponent();
            this.Paint += Form1_Paint;
            this.Resize += Form1_Resize;

            this.MouseWheel += Form1_MouseWheel;

            this.BackColor = Color.FromArgb(40,40,40);

            mvd = new MonthViewDrawer(this);
            Cache = new Cache(this);

            //appointmentSyncTimer_Tick(null,null;

            InitOwnFormComponents();
        }

        protected override void OnClick(EventArgs e)
        {
            dbf.SelectedDayButton = null;
        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            int delta = e.Delta;
            if(delta < 0)
            {
                ScrollMonthUp();
            }
            else
            {
                ScrollMonthDown();
            }
            this.Refresh();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            dbf.SetBounds(0, ScreenHeight / 3, 2 * ScreenWidth / 3, 2 * ScreenHeight / 3);
            abf.SetBounds(2 * ScreenWidth / 3, ScreenHeight / 3, ScreenWidth / 3, 2 * ScreenHeight / 3);
            this.Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            mvd.DrawMonthView(Cache.GetMonthData(CurrentYear,CurrentMonth), e);
        }

        public void AddAppointment(int year, int month, int day, Appointment a)
        {
            Cache.AddAppointment(year, month, day, a);
            if(year == CurrentYear && month == CurrentMonth)
            {
                dbf.RefreshDay(day);
            }
        }
        
        private void ScrollMonthDown()
        {
            if (CurrentMonth == 1)
            {
                CurrentYear--;
                CurrentMonth = 12;
                return;
            }
            CurrentMonth--;
        }

        private void ScrollMonthUp()
        {
            if(CurrentMonth == 12)
            {
                CurrentYear++;
                CurrentMonth = 1;
                return;
            }
            CurrentMonth++;
        }

        private void InitOwnFormComponents()
        {
            abf = new AppointmentButtonField(this);
            dbf = new DayButtonField(this, abf);
            dbf.SetBounds(0, ScreenHeight / 3, 2 * ScreenWidth / 3,2 * ScreenHeight / 3);
            abf.SetBounds(2 * ScreenWidth / 3, ScreenHeight / 3, ScreenWidth / 3, 2 * ScreenHeight / 3);
            Controls.Add(dbf);
            Controls.Add(abf);
        }


        private void appointmentSyncTimer_Tick(object sender, EventArgs e)
        {
            string _Path = Directory.GetCurrentDirectory();
            _Path = Path.Combine(_Path, "Data\\AppointmentData.xml");

            if (!File.Exists(_Path)) { return; }

            XmlDocument _XmlDocument = new XmlDocument();
            _XmlDocument.Load(_Path);

            foreach (XmlNode _Node in _XmlDocument.DocumentElement.ChildNodes)
            {
                //_Node.
            }



            //MessageBox.Show(_Path);

            //"\Data\AppointmentData.xml"
        }
    }
}
