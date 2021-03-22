﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CBApp3.Services
{
    public class MySearchHandler<T> : SearchHandler
    {
        public delegate void ItemSelected(object handler, T selectedItem);
        public event ItemSelected ItemSelectedHandler;
        public IList<T> Items { get; set; }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = Items
                    .Where(item => item.ToString().ToLower().Contains(newValue.ToLower()))
                    .ToList<T>();
            }
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            await Task.Delay(1000);

            this.MyOnItemSelected((T)item);
        }

        protected void MyOnItemSelected(T selectedItem)
        {
            if (ItemSelectedHandler != null)
            {
                this.ItemSelectedHandler(this, selectedItem);
            }
        }
    }
}
