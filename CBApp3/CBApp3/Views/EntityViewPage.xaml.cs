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

            this.listView.BindingContext = this.viewModel;

            if (this.viewModel.Days == null) this.CreateNullMessage();
        }

        private void CreateNullMessage()
        {
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            grid.Children.Add(
                    new Label
                    {
                        Text = "Расписание отсутствует!",
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 18
                    }, 0, 0);

            this.Content = grid;
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Day day = e.Item as Day;

            DayViewModel dayViewModel = new DayViewModel(day);

            await Shell.Current.Navigation.PushAsync(new DayViewPage(dayViewModel));
        }
    }
}