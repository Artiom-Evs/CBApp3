using System;
using System.Collections.Generic;
using Xamarin.Forms;

using CBApp3.Views;

namespace CBApp3
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            Routing.RegisterRoute(nameof(EntityViewPage), typeof(EntityViewPage));
            Routing.RegisterRoute(nameof(DayViewPage), typeof(DayViewPage));
        }

        protected async override void OnNavigated(ShellNavigatedEventArgs args)
        {
            if (!App.Connection)
            {
                await this.DisplayAlert("Внимание!", "Вы не подключены к сети интернет!", "ОК");
            }
        }
    }
}
