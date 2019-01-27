using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCalendar_Form
{
    public class ScoolHolidayProvider
    {
        public static async Task<ScoolHolidayModel[]> GetHollidays(int Year, string StateCode = "BE")
        {
            string url = "https://ferien-api.de/api/v1/holidays/"+(StateCode.Length>0?StateCode+"/":"")+Year;

            using(HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show(response.ReasonPhrase);
                    return null;
                }

                ScoolHolidayModel[] shm = await response.Content.ReadAsAsync<ScoolHolidayModel[]>();
                return shm;
            }
        }
    }

    public class ScoolHolidayModel
    {
        public string start, end, stateCode, name;
    }
}
