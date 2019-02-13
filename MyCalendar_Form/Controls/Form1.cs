using MyCalendar_Form.Controls;
using MyCalendar_Form.ExternalDataProvider;
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
    public partial class MainForm : Form
    {
        const string APPOINTMENT_DATA_FILE = "/ExternalMemory/AppoitmentData.txt";

        public int ScreenWidth        {get { return ClientSize.Width; }        }
        public int ScreenHeight {get { return ClientSize.Height; } }

        public int CurrentYear = DateTime.Now.Year;
        public int CurrentMonth = DateTime.Now.Month;

        MonthViewDrawer mvd;
        public Cache Cache;
        //public DayButton[] DayButtons = new DayButton[42];
        public DayButtonField dbf;
        public AppointmentButtonField abf;
        public AddAppointmentButton aab;
        public SaveAppoitmentsButton sab;

        AddAppointmentForm addAppointmentForm;
        
        Label HeaderLabel;

        public MainForm()
        {
            InitializeComponent();
            this.Paint += Form1_Paint;
            this.Resize += Form1_Resize;

            this.MouseWheel += Form1_MouseWheel;

            HeaderLabel = new Label();
            HeaderLabel.SetBounds(0, 0, Width, Height / 3);

            this.BackColor = Color.FromArgb(40,40,40);

            mvd = new MonthViewDrawer(this);
            Cache = new Cache(this);
            AppoitmentDataHandler.form = this;
            addAppointmentForm = new AddAppointmentForm(this);

            InitOwnFormComponents();

            AppoitmentDataHandler.AddAppoitmentsFromFile(Environment.CurrentDirectory + APPOINTMENT_DATA_FILE);
        }

        protected override void OnClick(EventArgs e)
        {
            dbf.SelectedDayButton = null;
        }
        
        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (abf.Bounds.Contains(e.Location)) return;

            if(e.Delta < 0)
            {
                ScrollMonthUp();
            }
            else
            {
                ScrollMonthDown();
            }

            dbf.SelectedDayButton = null;

            this.Refresh();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            dbf.SetBounds(0, ScreenHeight / 3, 2 * ScreenWidth / 3, 2 * ScreenHeight / 3);
            abf.SetBounds(2 * ScreenWidth / 3, ScreenHeight / 3, ScreenWidth / 3, 2 * ScreenHeight / 3);
            aab.SetBounds((int)(Width / 12 * 10.75) - 5, Height / 3 - (int)(1.5 * Height / 12), Width / 12, Height / 12);
            sab.SetBounds((int)(Width / 12 * 8.75) - 5, Height / 3 - (int)(1.5 * Height / 12), Width / 9, Height / 12);
            this.Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            mvd.DrawMonthView(Cache.GetMonthData(CurrentYear,CurrentMonth), e);
        }

        public void AddAppointment(string year, string month, string day, Appointment a)
        {
            Cache.AddAppointment(year, month, day, a);

            if(year == "-" || month == "-")
            {
                if(day == "-")
                {
                    dbf.RefreshAll();
                    return;
                }

                dbf.RefreshDay(int.Parse(day));
                return;
            }

            if(int.Parse(year) == CurrentYear && int.Parse(month) == CurrentMonth)
            {
                dbf.RefreshDay(int.Parse(day));
            }
        }

        public void AddAppointmentButton_Click(AddAppointmentButton addAppointmentButton, EventArgs e)
        {
            if (addAppointmentForm.Visible) return;
            addAppointmentForm.Visible = true;
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

            aab = new AddAppointmentButton();
            sab = new SaveAppoitmentsButton();
            aab.SetBounds((int)(Width / 12 * 10.75) - 5, Height / 3 - (int)(1.5 * Height / 12), Width / 12, Height / 12);
            sab.SetBounds((int)(Width / 12 * 8.75) - 5, Height / 3 - (int)(1.5 * Height / 12), Width / 9, Height / 12);
            Controls.Add(aab);
            Controls.Add(sab);
        }

        public void SaveAppoitmentsButton_Click(SaveAppoitmentsButton saveAppoitmentsButton, EventArgs e)
        {
            AppoitmentDataHandler.WriteAllAppoitmentsToFile(Environment.CurrentDirectory + APPOINTMENT_DATA_FILE);
        }
    }
}
