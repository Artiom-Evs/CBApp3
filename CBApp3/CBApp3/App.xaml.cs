using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CBApp3.Domain.Models;
using CBApp3.Services;

namespace CBApp3
{
    public partial class App : Application
    {
        public static string proupsHttpAddress = 
            "http://mgke.minsk.edu.by/ru/main.aspx?guid=3791";
        public static string teachersHttpAddress = 
            "http://mgke.minsk.edu.by/ru/main.aspx?guid=3811";
        public static string groupsPageFileName =
            DependencyService.Get<Services.ILocalPath>().GetFullPath("groups.html");
        public static string teachersPageFileName =
            DependencyService.Get<Services.ILocalPath>().GetFullPath("teachers.html");
        public static string groupsFileName =
            DependencyService.Get<Services.ILocalPath>().GetFullPath("groups.xml");
        public static string teachersFileName =
            DependencyService.Get<Services.ILocalPath>().GetFullPath("teachers.xml");

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
        public static bool Connection
        {
            get
            {
                return DependencyService.Get<IConnectionInfo>().IsConnected;
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
