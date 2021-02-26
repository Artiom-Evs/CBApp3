using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using CBApp3.Domain.Models;

namespace CBApp3.Domain.ViewModels
{
    public class EntitiesListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangedEventHandler ContentChanged;
        private EntitiesList entitiesList;

        public EntitiesListViewModel(string name, bool isGroup)
        {
            this.EntitiesList = new EntitiesList(name, isGroup);
        }

        public EntitiesList EntitiesList
        {
            get => entitiesList;
            set
            {
                if (this.entitiesList != value)
                {
                    entitiesList = value;

                    OnContentChanged("EntitiesList");
                }    
            }
        }
        public string Name
        {
            get { return this.EntitiesList.Name; }
            set
            {
                if(this.EntitiesList.Name != value)
                {
                    this.EntitiesList.Name = value;

                    this.OnPropertyChanged("Name");
                }
            }
        }
        public string Date
        {
            get { return this.EntitiesList.Date; }
            set
            {
                if (this.EntitiesList.Date != value)
                {
                    this.EntitiesList.Date = value;

                    this.OnPropertyChanged("Date");
                }
            }
        }
        public bool IsGroup
        {
            get { return this.EntitiesList.IsGroup; }
            set
            {
                if (this.EntitiesList.IsGroup != value)
                {
                    this.EntitiesList.IsGroup = value;

                    this.OnPropertyChanged("IsGroup");
                }
            }
        }
        public List<Entity> Entities
        {
            get { return this.EntitiesList.Entities; }
            set
            {
                if (this.EntitiesList.Entities != value)
                {
                    this.EntitiesList.Entities = value;

                    this.OnPropertyChanged("Entities");
                }
            }
        }

        protected void OnContentChanged(string propertyName)
        {
            if (this.ContentChanged != null)
            {
                ContentChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
