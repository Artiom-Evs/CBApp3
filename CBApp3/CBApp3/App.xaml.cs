using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CBApp3.Domain.Models;

namespace CBApp3
{
    public partial class App : Application
    {
        private static EntitiesList groupsList;
        private static EntitiesList teachersList;
        public static EntitiesList GroupsList
        {
            get
            {
                if (groupsList == null)
                {
                    groupsList = new EntitiesList();
                    return groupsList;
                }

                return groupsList;
            }
        }
        public static EntitiesList TeachersList
        {
            get
            {
                if(teachersList == null)
                {
                    teachersList = new EntitiesList();
                    return teachersList;
                }

                return teachersList;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
