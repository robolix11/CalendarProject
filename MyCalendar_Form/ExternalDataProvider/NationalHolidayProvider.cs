using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCalendar_Form
{
    class NationalHolidayProvider
    {
        public static async Task<Dictionary<string, NationalHoliday>> GetHollidays(int Year, string StateCode = "BE")
        {
            string url = "https://feiertage-api.de/api/?" + (StateCode.Length > 0 ? "nur_land="+StateCode + "&" : "") + "jahr="+Year;

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(response.ReasonPhrase);
                    return null;
                }

                Dictionary<string, NationalHoliday> _Result = await response.Content.ReadAsAsync<Dictionary<string, NationalHoliday>>();
                return _Result;
            }
        }
    }

    class NationalHoliday
    {
        public string datum, hinweis;
    }
}
