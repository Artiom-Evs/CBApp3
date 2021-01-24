using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CBApp3.Domain.Models;
using CBApp3.Domain.Services;
using CBApp3.Services;

namespace CBApp3
{
    public partial class App : Application
    {
        public static string groupsHttpAddress = 
            "http://mgke.minsk.edu.by/ru/main.aspx?guid=3791";
        public static string teachersHttpAddress = 
            "http://mgke.minsk.edu.by/ru/main.aspx?guid=3811";
        public static string groupsHtmlPath =
            DependencyService.Get<Services.ILocalPath>().GetFullPath("groups.html");
        public static string teachersHtmlPath =
            DependencyService.Get<Services.ILocalPath>().GetFullPath("teachers.html");
        public static string groupsXmlPath =
            DependencyService.Get<Services.ILocalPath>().GetFullPath("groups.xml");
        public static string teachersXmlPath =
            DependencyService.Get<Services.ILocalPath>().GetFullPath("teachers.xml");

        public static Task<EntitiesList> LoadGroupsTask;
        public static Task<EntitiesList> LoadTeachersTask;

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
            set
            {
                groupsList = value;
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
            set
            {
                teachersList = value;
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

            //Task task1 = DataController.LoadHtmlPageAsync(groupsHttpAddress, groupsHtmlPath);
            //Task task2 = DataController.LoadHtmlPageAsync(teachersHttpAddress, teachersHtmlPath);

            Task task1 = Task.Run(() => DataController.LoadHtmlPage(groupsHttpAddress, groupsHtmlPath));
            Task task2 = Task.Run(() => DataController.LoadHtmlPage(teachersHttpAddress, teachersHtmlPath));

            Parser parser = new Parser();

            task1.Wait();
            var task3 = Task.Run(() => parser.ParsePage(groupsHtmlPath, true));

            task2.Wait();
            var task4 = Task.Run(() => parser.ParsePage(teachersHtmlPath, false));

            GroupsList = task3.Result;
            TeachersList = task4.Result;

            MainPage = new AppShell();
        }

        private void Task1()
        {
            if (App.Connection)
            {
                /*
                App.LoadGroupsTask = 
                    DataController.LoadHtmlPage(proupsHttpAddress, groupsHtmlPath);
                App.LoadTeachersTask = 
                    DataController.LoadHtmlPage(teachersHttpAddress, teachersHtmlPath);
                 */

                App.LoadGroupsTask =
                    DataController.DataProcessing(groupsHttpAddress, groupsHtmlPath, true);
                App.LoadTeachersTask =
                    DataController.DataProcessing(teachersHttpAddress, teachersHtmlPath, false);
            }
            else
            {
                App.GroupsList.Load(groupsXmlPath);
                App.TeachersList.Load(groupsXmlPath);
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
