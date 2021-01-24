using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CBApp3.Domain.Services;
using CBApp3.Views;

namespace CBApp3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Обновить данные", "Функция обновления данных пока не доступна", "ОК");
        }
        private void MenuItem_Clicked_1(object sender, EventArgs e)
        {
            App.Current.Quit();
        }
    }
}
