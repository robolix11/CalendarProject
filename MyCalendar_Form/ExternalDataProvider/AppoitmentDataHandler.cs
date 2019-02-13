using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalendar_Form.ExternalDataProvider
{
    public class AppoitmentDataHandler
    {
        public static MainForm form;
        public static void AddAppoitmentsFromFile(string xPath)
        {
            xPath = xPath.Replace("/", "\\");
            if (!File.Exists(xPath))
            {
                string dir = xPath.Substring(0, xPath.LastIndexOf("\\"));
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                File.Create(xPath);
            }

            string[] lines;
            try
            {
                lines = File.ReadAllLines(xPath);
            }catch(Exception e)
            {
                return;
            }
            if (lines == null) return;

            foreach (string line in lines)
            {
                if (line == "") continue;
                string[] splitted = line.Split('|');
                if (splitted.Count() != 3) return;

                string datestring = splitted[0];
                string timestring = splitted[1];
                string textstring = splitted[2].Replace("##PIPE##","|");

                string[] datepartstrings = datestring.Split('.');

                Appointment appointment;
                if (timestring == "-")
                {
                    appointment = new Appointment(true, textstring, AppointmentType.Default);
                }
                else {
                    string[] timepartstrings = timestring.Split(':');
                    int hour = int.Parse(timepartstrings[0]);
                    int minute = int.Parse(timepartstrings[1]);
                    appointment = new Appointment(hour, minute, textstring, AppointmentType.Default);
                }
                form.AddAppointment(datepartstrings[2], datepartstrings[1], datepartstrings[0], appointment);
            }
        }

        public static void WriteAllAppoitmentsToFile(string xPath)
        {
            string FileContent = "";

            foreach(Tuple<Appointment,int,int> tuple in form.Cache.YearlyAppointments)
            {
                Appointment appointment = tuple.Item1;
                FileContent += "" + tuple.Item2 + "." + tuple.Item3 + ".-" + "|" 
                    + (appointment.wholeDay ? "-" : (appointment.hour < 10 ? "0" + appointment.hour : "" + appointment.hour) 
                        + ":" + (appointment.minute < 10 ? "0" + appointment.minute : "" + appointment.minute)) 
                    + "|" + appointment.Title.Replace("|","##PIPE##") + Environment.NewLine;
            }

            foreach (Tuple<Appointment, int> tuple in form.Cache.MonthlyAppointments)
            {
                Appointment appointment = tuple.Item1;
                FileContent += "" + tuple.Item2 + ".-.-" + "|"
                    + (appointment.wholeDay ? "-" : (appointment.hour < 10 ? "0" + appointment.hour : "" + appointment.hour)
                        + ":" + (appointment.minute < 10 ? "0" + appointment.minute : "" + appointment.minute))
                    + "|" + appointment.Title.Replace("|", "##PIPE##") + Environment.NewLine;
            }

            foreach (Appointment appointment in form.Cache.DaylyAppointments)
            {
                FileContent += "-.-.-|"
                    + (appointment.wholeDay ? "-" : (appointment.hour < 10 ? "0" + appointment.hour : "" + appointment.hour)
                        + ":" + (appointment.minute < 10 ? "0" + appointment.minute : "" + appointment.minute))
                    + "|" + appointment.Title.Replace("|", "##PIPE##") + Environment.NewLine;
            }

            foreach(MonthData md in form.Cache.monthData)
            {
                foreach(CalendarDay cd in md.CalendarDays)
                {
                    foreach(Appointment appointment in cd.Appointments.Where(a => a.Type == AppointmentType.Default && !a.repeating))
                    {
                        FileContent += cd.Day + "."+cd.Month+"."+cd.Year+"|"
                            + (appointment.wholeDay ? "-" : (appointment.hour < 10 ? "0" + appointment.hour : "" + appointment.hour)
                                + ":" + (appointment.minute < 10 ? "0" + appointment.minute : "" + appointment.minute))
                            + "|" + appointment.Title.Replace("|", "##PIPE##") + Environment.NewLine;
                    }
                }
            }

            xPath = xPath.Replace("/", "\\");
            if (!File.Exists(xPath))
            {
                string dir = xPath.Substring(0, xPath.LastIndexOf("\\"));
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                File.Create(xPath);
            }
            File.WriteAllText(xPath, FileContent);
        }
    }
}
