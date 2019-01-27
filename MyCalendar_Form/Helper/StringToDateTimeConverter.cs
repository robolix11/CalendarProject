using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalendar_Form.Helper
{
    class DateTimeHelper
    {
        public static DateTime ConverStringToDateTime(string xDTString)
        {
            string[] _Split = xDTString.Split('T');

            string _Date = _Split[0];
            string _Time = _Split[1];

            _Split = _Date.Split('-');
            int _Year = int.Parse(_Split[0]);
            int _Month = int.Parse(_Split[1]);
            int _Day = int.Parse(_Split[2]);

            _Split = _Time.Split(':');
            int _Hour = int.Parse(_Split[0]);
            int _Minute = int.Parse(_Split[1]);

            return new DateTime(_Year, _Month, _Day, _Hour, _Minute, 0);
        }

        public static DateTime SetTimePartZero(DateTime dateTime)
        {
            dateTime.Subtract(new TimeSpan(dateTime.Hour, dateTime.Minute, 0));
            return dateTime;
        }
    }
}
