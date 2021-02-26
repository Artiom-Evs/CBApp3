using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace CBApp3.Domain.Services
{
public static class DataController
    {
        public static string GetFileText(string path)
        {
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            else
            {
                throw new FileNotFoundException("Файл не был загружен.");
            }
        }

        public static Task<string> LoadPageText(string uri)
        {
            using (WebClient wc = new WebClient())
            {
                return wc.DownloadStringTaskAsync(new Uri(uri));
            }   
        }

        public static Task LoadPageFile(string uri, string path)
        {
            //преподаватели "http://mgke.minsk.edu.by/ru/main.aspx?guid=3811"
            //учащиеся "http://mgke.minsk.edu.by/ru/main.aspx?guid=3791"

            using (WebClient wc = new WebClient())
            {
                return wc.DownloadFileTaskAsync(new System.Uri(uri), path);
            }
        }
    }
}
