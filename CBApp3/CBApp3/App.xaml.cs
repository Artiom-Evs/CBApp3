using CBApp3.Services;
using CBApp3.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CBApp3.Domain.ViewModels;

namespace CBApp3
{
    public partial class App : Application
    {
        public static string groupsHttpAddress =
            @"http://mgke.minsk.edu.by/ru/main.aspx?guid=3791";
        public static string teachersHttpAddress =
            @"http://mgke.minsk.edu.by/ru/main.aspx?guid=3811";
        /*
        public static string groupsFilePath =
            DependencyService.Get<Services.ILocalPath>().GetFullPath("groups.xml");
        public static string teachersFilePath =
            DependencyService.Get<Services.ILocalPath>().GetFullPath("teachers.xml");
        */

        private static EntitiesListViewModel groupsList;
        private static EntitiesListViewModel teachersList;
        private static DataController groupsController;
        private static DataController teachersController;

        public static EntitiesListViewModel GroupsList
        {
            get
            {
                if (groupsList == null)
                {
                    groupsList = new EntitiesListViewModel("Список групп", true);
                    return groupsList;
                }
                return groupsList;
            }
        }
        public static EntitiesListViewModel TeachersList
        {
            get
            {
                if (teachersList == null)
                {
                    teachersList = new EntitiesListViewModel("Список преподавателей", false);
                    return teachersList;
                }
                return teachersList;
            }
        }
        public static DataController GroupsController
        {
            get
            {
                if (groupsController == null)
                {
                    groupsController = new DataController(GroupsList, groupsHttpAddress, true);
                    return groupsController;
                }
                return groupsController;
            }
        }
        public static DataController TeachersController
        {
            get
            {
                if (teachersController == null)
                {
                    teachersController = new DataController(TeachersList, teachersHttpAddress, false);
                    return teachersController;
                }
                return teachersController;
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

            if (App.Connection)
            {
                GroupsController.UpdateData();
                TeachersController.UpdateData();

                MainPage = new AppShell();

                GroupsController.Loading.Wait();
                TeachersController.Loading.Wait();
            }
            else
            {
                MainPage = new AppShell();
            }
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
