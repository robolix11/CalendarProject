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

        int CurrentYear = DateTime.Now.Year;
        int CurrentMonth = DateTime.Now.Month;

        MonthViewDrawer mvd;
        Cache cache = new Cache();
        public DayButton[] DayButtons = new DayButton[42];

        public Form1()
        {
            InitializeComponent();
            this.Paint += Form1_Paint;
            this.Resize += Form1_Resize;

            this.MouseWheel += Form1_MouseWheel;

            this.BackColor = Color.FromArgb(40,40,40);

            mvd = new MonthViewDrawer(this);

            //appointmentSyncTimer_Tick(null,null;

            AddDayButtonsToForm();
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
            mvd.Rescale();
            this.Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            mvd.DrawMonthView(cache.GetMonthData(CurrentYear,CurrentMonth), e);
        }

        private void AddAppointment(int year, int month, int day, Appointment a)
        {
            cache.AddAppointment(year, month, day, a);
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

        private void AddDayButtonsToForm()
        {
            for (int index = 0; index < 42; index++)
            {
                int i = index % 7;
                int j = index / 7;

                int x = i * (ScreenWidth / 10) + (int)((ScreenWidth / 10) * 1.5);
                int y = j * (ScreenHeight / 10) + ScreenHeight / 3;

                DayButton db = new DayButton(x, y, (ScreenWidth / 10) - 5, (ScreenHeight / 10) - 5);
                DayButtons[index] = db;
                Controls.Add(db);
            }
        }
    }
}
