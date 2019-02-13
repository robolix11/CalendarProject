using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCalendar_Form.Controls
{
    public partial class AddAppointmentForm : Form
    {
        string CurrDay = "" + DateTime.Now.Day, CurrMonth = ""+DateTime.Now.Month, CurrYear = "" + DateTime.Now.Year;
        MainForm form1;
        public AddAppointmentForm(MainForm form1)
        {
            InitializeComponent();

            this.form1 = form1;
            BackColor = Color.FromArgb(40, 40, 40);

            numericUpDown1.Text = CurrDay;
            numericUpDown2.Text = CurrMonth;
            numericUpDown3.Text = CurrYear;

            numericUpDown1.Controls[0].Hide();
            numericUpDown2.Controls[0].Hide();
            numericUpDown3.Controls[0].Hide();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //base.OnClosing(e);
            e.Cancel = true;
            Visible = false;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length <= 0) return;
            Appointment appointment = checkBox4.Checked ? new Appointment(true, textBox1.Text, AppointmentType.Default) : new Appointment(int.Parse(numericUpDown5.Text), int.Parse(numericUpDown4.Text), textBox1.Text, AppointmentType.Default);
            form1.AddAppointment(numericUpDown3.Text, numericUpDown2.Text, numericUpDown1.Text, appointment);
            Visible = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                numericUpDown1.ReadOnly = false;
                numericUpDown1.Text = CurrDay;

                numericUpDown2.ReadOnly = false;
                numericUpDown2.Text = CurrMonth;

                numericUpDown3.ReadOnly = false;
                numericUpDown3.Text = CurrYear;
            }
            else
            {

                checkBox2.Checked = false;
                checkBox3.Checked = false;

                CurrDay = numericUpDown1.Text;
                numericUpDown1.ReadOnly = true;
                numericUpDown1.Text = "-";

                CurrMonth = numericUpDown2.Text;
                numericUpDown2.ReadOnly = true;
                numericUpDown2.Text = "-";

                CurrYear = numericUpDown3.Text;
                numericUpDown3.ReadOnly = true;
                numericUpDown3.Text = "-";
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox2.Checked)
            {
                numericUpDown2.ReadOnly = false;
                numericUpDown2.Text = CurrMonth;

                numericUpDown3.ReadOnly = false;
                numericUpDown3.Text = CurrYear;
            }
            else
            {

                checkBox1.Checked = false;
                checkBox3.Checked = false;

                numericUpDown1.ReadOnly = false;
                numericUpDown1.Text = CurrDay;

                CurrMonth = numericUpDown2.Text;
                numericUpDown2.ReadOnly = true;
                numericUpDown2.Text = "-";

                CurrYear = numericUpDown3.Text;
                numericUpDown3.ReadOnly = true;
                numericUpDown3.Text = "-";
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox3.Checked)
            {
                numericUpDown3.ReadOnly = false;
                numericUpDown3.Text = CurrYear;
            }
            else
            {

                checkBox1.Checked = false;
                checkBox2.Checked = false;

                numericUpDown1.ReadOnly = false;
                numericUpDown1.Text = CurrDay;

                numericUpDown2.ReadOnly = false;
                numericUpDown2.Text = CurrMonth;

                CurrYear = numericUpDown3.Text;
                numericUpDown3.ReadOnly = true;
                numericUpDown3.Text = "-";
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown5.ReadOnly = checkBox4.Checked;
            numericUpDown5.Text = checkBox4.Checked ? "-" : "" + DateTime.Now.Hour;

            numericUpDown4.ReadOnly = checkBox4.Checked;
            numericUpDown4.Text = checkBox4.Checked ? "-" : "" + DateTime.Now.Minute;
        }



        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            CurrDay = numericUpDown1.Text;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            CurrMonth = numericUpDown2.Text;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            CurrYear = numericUpDown3.Text;
        }
    }
}
