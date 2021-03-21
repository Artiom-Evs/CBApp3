using CBApp3.Services;
using CBApp3.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CBApp3.Domain.ViewModels;
using CBApp3.Domain.Services;

namespace CBApp3
{
    public partial class App : Application
    {
        public static string groupsHttpAddress =
            @"http://mgke.minsk.edu.by/ru/main.aspx?guid=3791";
        public static string teachersHttpAddress =
            @"http://mgke.minsk.edu.by/ru/main.aspx?guid=3811";
        public static string groupsFilePath =
            DependencyService.Get<Services.ILocalPath>().GetFullPath("groups.xml");
        public static string teachersFilePath =
            DependencyService.Get<Services.ILocalPath>().GetFullPath("teachers.xml");

        private static EntitiesListViewModel groupsList;
        private static EntitiesListViewModel teachersList;
        
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
                Task task1 = Task.Run(() => UpdateData(GroupsList, true, groupsHttpAddress));
                Task task2 = Task.Run(() => UpdateData(TeachersList, false, teachersHttpAddress));

                MainPage = new AppShell();

                task1.Wait();
                task2.Wait();
            }
            else
            {
                MainPage = new AppShell();
            }
        }

        private void UpdateData(EntitiesListViewModel viewModel, bool isGroup, string url)
        {
            viewModel.EntitiesList =
                Parser.ParsePage(DataController.LoadPageText(url), isGroup);
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
