using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CBApp3.Domain.ViewModels;

namespace CBApp3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayViewPage : ContentPage
    {
        private DayViewModel viewModel;

        public DayViewPage()
        {
            InitializeComponent();
        }

        public DayViewPage(DayViewModel viewModel)
        {
            this.InitializeComponent();

            this.viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            this.viewModel = (DayViewModel)this.BindingContext ?? this.viewModel;

            this.BindingContext = this.viewModel;

            this.CreateGridContent();
        }

        private void CreateGridContent()
        {
            Grid grid = this.grid_1;
            List<string[]> lessons = this.viewModel.Lessons;

            for (int i = lessons.Count - 1; i >= 0; i--)
            {
                if (lessons[i][0] == "-" || lessons[i][0] == " ")
                {
                    lessons.Remove(lessons[i]);
                }
                else break;
            }

            if (lessons.Count == 0)
            {
                CreateNullMessage();
                return;
            }

            for (int i = 0; i < lessons.Count; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Auto)
                });
            }

            for (int i = 0; i < lessons.Count; i++)
            {
                string text1, text2;

                text1 = (i + 1).ToString();
                CreateGridItem(grid, text1, 0, i);

                text2 = lessons[i][0].Replace(") ", ")\n").Replace("1.", "1. ").Replace(" 2.", "\n2. ").Replace(" 3.", "\n3. ") + "\n" + lessons[i][1];
                CreateGridItem(grid, text2, 1, i);
            }

            this.UpdateChildrenLayout();
        }

        private void CreateGridItem(Grid grid, string text, int x, int y)
        {
            grid.Children.Add(
                    new Label
                    {
                        Text = text,
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 18,
                        Padding = 10
                    }, x, y);
        }

        private void CreateNullMessage()
        {
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            grid.Children.Add(
                    new Label
                    {
                        Text = "Расписание отсутствует!",
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 18
                    }, 0, 0);

            this.Content = grid;
        }
    }
}