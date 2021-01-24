using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CBApp3.Domain.ViewModels;

namespace CBApp3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("Name", "name")]
    public partial class EntitiesListViewPage : ContentPage
    {
        public EntitiesListViewPage()
        {
            InitializeComponent();

            this.BindingContext = new EntitysListViewModel();
        }
        
        public string Name
        {
            get { return Name; }
            set
            {
                Name = Uri.UnescapeDataString(value);
            }
        }
    }
}