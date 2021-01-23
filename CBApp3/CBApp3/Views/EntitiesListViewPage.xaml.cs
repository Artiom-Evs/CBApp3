﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CBApp3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntitiesListViewPage : ContentPage
    {
public EntitiesListViewPage()
        {
            InitializeComponent();

            this.BindingContext = App.GroupsList;
        }
    }
}