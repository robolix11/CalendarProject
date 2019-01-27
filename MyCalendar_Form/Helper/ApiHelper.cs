using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MyCalendar_Form
{
    class ApiHelper
    {
        public static HttpClient ApiClient { get
            {
                if(apiClient == null)
                {
                    InitClient();
                }
                return apiClient;
            } }

        private static HttpClient apiClient;
        public static void InitClient()
        {
            apiClient = new HttpClient();
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
