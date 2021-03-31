using System;
using System.Net;
using System.Threading.Tasks;

using CBApp3.Domain.ViewModels;
using CBApp3.Domain.Services;


namespace CBApp3.Services
{
    public class DataController
    {
        public delegate void DataLoadedDelegate();
        public DataLoadedDelegate DataLoaded;
        public EntitiesListViewModel ViewModel { get; private set; }
        public string Address { get; private set; }
        public bool IsGroup { get; private set; }
        public Task Loading { get; protected set; }

        public DataController(EntitiesListViewModel viewModel, string address, bool isGroup)
        {
            this.ViewModel = viewModel;
            this.Address = address;
            this.IsGroup = isGroup;
        }

        public void UpdateData()
        {
            this.Loading = Task.Run(() =>
            {
                this.ViewModel.EntitiesList =
                    Parser.ParsePage(this.LoadPageText(this.Address), this.IsGroup);
                this.OnDataLoaded();
            });
        }

        private Task<string> LoadPageText(string url)
        {
            using (WebClient wc = new WebClient())
            {
                return wc.DownloadStringTaskAsync(new Uri(url));
            }
        }

        private void OnDataLoaded()
        {
            if (this.DataLoaded != null)
            {
                this.DataLoaded();
            }
        }
    }
}
