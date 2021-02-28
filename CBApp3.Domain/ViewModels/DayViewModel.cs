using CBApp3.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace CBApp3.Domain.ViewModels
{
    public class DayViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangedEventHandler ContentChanged;
        private Day day;

        public DayViewModel(Day day)
        {
            this.day = day;
        }

        public Day Day
        {
            get => this.day;
            set
            {
                if (this.day != value)
                {
                    OnContentChanged(nameof(this.Day));

                    this.day = value;
                }
            }
        }

        public string Date
        {
            get => this.day.Date;
            set
            {
                if (this.day.Date != value)
                {
                    OnPropertyChanged(nameof(this.Date));

                    this.day.Date = value;
                }
            }
        }

        public List<string[]> Lessons
        {
            get => this.day.Lessons;
            set
            {
                if (this.day.Lessons != value)
                {
                    OnPropertyChanged(nameof(this.Lessons));

                    this.day.Lessons = value;
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
            if (PropertyChanged != null)
            {
                ContentChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
