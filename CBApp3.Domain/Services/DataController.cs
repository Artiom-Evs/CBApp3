using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace CBApp3.Domain.Services
{
    public class DataController
    {
        public Task LoadHtmlPage(string htmlAddress, string path)
        {
            //преподаватели "http://mgke.minsk.edu.by/ru/main.aspx?guid=3811"
            //учащиеся "http://mgke.minsk.edu.by/ru/main.aspx?guid=3791"

            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                return client.DownloadFileTaskAsync(new System.Uri(htmlAddress), path);
            }
        }
    }
}
