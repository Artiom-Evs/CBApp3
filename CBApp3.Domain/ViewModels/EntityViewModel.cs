using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using CBApp3.Domain.Models;

namespace CBApp3.Domain.ViewModels
{
    public class EntityViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangedEventHandler ContentChanged;
        private Entity entity;

        public EntityViewModel(Entity entity)
        {
            this.entity = entity;
        }

        public Entity Entity
        {
            get => this.entity;
            set
            {
                if (this.entity != value)
                {
                    OnContentChanged(nameof(Entity));

                    entity = value;
                }
            }
        }

        public string Name
        {
            get => entity.Name;
            set
            {
                OnPropertyChanged(nameof(Name));
                
                entity.Name = value;
            }
        }

        public string Date
        {
            get => entity.Date;
            set
            {
                if (entity.Date != value)
                {
                    if (entity.Name != value)
                    {
                        OnPropertyChanged(nameof(Date));

                        entity.Date = value;
                    }
                }
            }
        }

        public List<Day> Days
        {
            get => entity.Days;
            set
            {
                if (entity.Days != value)
                {
                    OnPropertyChanged(nameof(Days));

                    entity.Days = value;
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

        protected void OnContentChanged(string propertyName)
        {
            if (ContentChanged != null)
            {
                ContentChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
