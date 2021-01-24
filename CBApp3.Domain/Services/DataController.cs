using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace CBApp3.Domain.Services
{
    public static class DataController
    {
        public static void LoadHtmlPage(string htmlAddress, string path)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                client.DownloadFile(new System.Uri(htmlAddress), path);
            }
        }
        public static Task LoadHtmlPageAsync(string htmlAddress, string path)
        {
            //преподаватели "http://mgke.minsk.edu.by/ru/main.aspx?guid=3811"
            //учащиеся "http://mgke.minsk.edu.by/ru/main.aspx?guid=3791"

            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                return client.DownloadFileTaskAsync(new System.Uri(htmlAddress), path);
            }
        }

        public static Task<Models.EntitiesList> DataProcessing(string htmlFilePath, bool isGroup)
        {
            Parser parser = new Parser();

            return Task.Run(() => parser.ParsePage(htmlFilePath, isGroup));
        }
        public static async Task<Models.EntitiesList> DataProcessing(Task loadTask, string htmlFilePath, bool isGroup)
        {
            Parser parser = new Parser();

            loadTask.Wait();

            return parser.ParsePage(htmlFilePath, isGroup);
        }
        public static async Task<Models.EntitiesList> DataProcessing(string httpAddress, string htmlFilePath, bool isGroup)
        {
            Task loadTask = LoadHtmlPageAsync(httpAddress, htmlFilePath);

            Parser parser = new Parser();

            loadTask.Wait();

            return parser.ParsePage(htmlFilePath, isGroup);
        }
    }
}
