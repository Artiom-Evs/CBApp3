using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using CBApp3.Domain.Models;

namespace CBApp3.Domain.ViewModels
{
    public class EntitysListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private EntitiesList entitiesList;

        public EntitysListViewModel()
        {
            entitiesList = new EntitiesList();
        }

        public string Name
        {
            get { return entitiesList.Name; }
            set
            {
                if(entitiesList.Name != value)
                {
                    entitiesList.Name = value;

                    this.OnPropertyChanged("Name");
                }
            }
        }
        public string Date
        {
            get { return entitiesList.Date; }
            set
            {
                if (entitiesList.Date != value)
                {
                    entitiesList.Date = value;

                    this.OnPropertyChanged("Date");
                }
            }
        }
        public bool IsGroup
        {
            get { return entitiesList.IsGroup; }
            set
            {
                if (entitiesList.IsGroup != value)
                {
                    entitiesList.IsGroup = value;

                    this.OnPropertyChanged("IsGroup");
                }
            }
        }
        public List<Entity> Entities
        {
            get { return entitiesList.Entities; }
            set
            {
                if (entitiesList.Entities != value)
                {
                    entitiesList.Entities = value;

                    this.OnPropertyChanged("Entities");
                }
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
