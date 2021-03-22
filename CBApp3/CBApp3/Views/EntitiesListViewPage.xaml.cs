using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using System.ComponentModel;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CBApp3.Domain.ViewModels;
using CBApp3.Domain.Models;
using CBApp3.Services;

namespace CBApp3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class EntitiesListViewPage : ContentPage
    {
        EntitiesListViewModel viewModel;

        public EntitiesListViewPage()
        {
            InitializeComponent();
        }

        public EntitiesListViewPage(EntitiesListViewModel viewModel)
        {
            this.InitializeComponent();

            this.viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            this.viewModel = (EntitiesListViewModel)this.BindingContext ?? this.viewModel;

            this.viewModel.ContentChanged += UpdateContent;

            this.listView.BindingContext = this.viewModel;

            MySearchHandler<Entity> searchHandler = new MySearchHandler<Entity>();
            searchHandler.Items = this.viewModel.Entities;
            searchHandler.Placeholder = "Найти...";
            searchHandler.ShowsResults = true;
            searchHandler.FontSize = 20;
            
            Shell.SetSearchHandler(this, searchHandler);

            searchHandler.ItemSelectedHandler += listView_ItemTapped;
        }

        private void UpdateContent(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "EntitiesList")
            {
                UpdateChildrenLayout();
            }
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Entity entity = e.Item as Entity;

            EntityViewModel entityViewModel = new EntityViewModel(entity);

            await Shell.Current.Navigation.PushAsync(new EntityViewPage(entityViewModel));
        }
    }
}