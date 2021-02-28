using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CBApp3.Domain.Models;
using CBApp3.Domain.ViewModels;

namespace CBApp3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntityViewPage : ContentPage
    {
        EntityViewModel viewModel;

        public EntityViewPage()
        {
            this.InitializeComponent();
        }

        public EntityViewPage(EntityViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            this.viewModel = (EntityViewModel)this.BindingContext ?? this.viewModel;

            this.FindByName<ListView>("listView").BindingContext = this.viewModel;
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Day day = e.Item as Day;

            DayViewModel dayViewModel = new DayViewModel(day);

            await Shell.Current.Navigation.PushAsync(new DayViewPage(dayViewModel));
        }
    }
}