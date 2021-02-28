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
            Grid grid = this.FindByName<Grid>("grid_1");
            List<string[]> lessons = this.viewModel.Lessons;

            for (int i = lessons.Count - 1; i >= 0; i--)
            {
                if (lessons[i][0] == "-" || lessons[i][0] == " ")
                {
                    lessons.Remove(lessons[i]);
                }
                else break;
            }

            for (int i = 0; i < lessons.Count; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Auto)
                });
            }

            var p = Padding;
            p.Bottom = 10;

            for (int i = 0; i < lessons.Count; i++)
            {
                grid.Children.Add(
                    new Label
                    {
                        Text = (i + 1).ToString(),
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Start,
                        FontSize = 18,
                        Padding = p
                    }, 0, i);

                grid.Children.Add(
                    new Label
                    {
                        Text = lessons[i][0].Replace(") ", ")\n").Replace("1.", "1. ").Replace(" 2.", "\n2. ").Replace(" 3.", "\n3. ") + "\n" + lessons[i][1],
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 18,
                        Padding = p
                    }, 1, i);

                this.UpdateChildrenLayout();
            }
        }
    }
}