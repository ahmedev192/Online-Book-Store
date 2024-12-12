using LiveCharts;
using LiveCharts.Wpf;
using OnlineBookStore.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace OnlineBookStore.Views
{
    public partial class StatisticsMonitoringView : Window
    {
        private readonly StatisticsService _statisticsService;

        public StatisticsMonitoringView()
        {
            InitializeComponent();
            _statisticsService = new StatisticsService();

            LoadTopSellingBooks();
            LoadPopularCategories();
        }

        private void LoadTopSellingBooks()
        {
            var topSellingBooks = _statisticsService.GetTopSellingBooks();

            // Parse data for chart
            var bookTitles = topSellingBooks.Select(b => b.Split('(')[0].Trim()).ToArray();
            var bookSales = topSellingBooks.Select(b => int.Parse(b.Split('(')[1].Split(' ')[0])).ToArray();

            TopSellingBooksChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Sales",
                    Values = new ChartValues<int>(bookSales)
                }
            };

            TopSellingBooksChart.AxisX.Add(new Axis
            {
                Title = "Books",
                Labels = bookTitles
            });

            TopSellingBooksChart.AxisY.Add(new Axis
            {
                Title = "Sales",
                LabelFormatter = value => value.ToString("N0")
            });
        }

        private void LoadPopularCategories()
        {
            var popularCategories = _statisticsService.GetPopularCategories();

            // Parse data for chart
            var categoryNames = popularCategories.Select(c => c.Split('(')[0].Trim()).ToArray();
            var categorySales = popularCategories.Select(c => int.Parse(c.Split('(')[1].Split(' ')[0])).ToArray();

            PopularCategoriesChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Sales",
                    Values = new ChartValues<int>(categorySales)
                }
            };

            PopularCategoriesChart.AxisX.Add(new Axis
            {
                Title = "Categories",
                Labels = categoryNames
            });

            PopularCategoriesChart.AxisY.Add(new Axis
            {
                Title = "Sales",
                LabelFormatter = value => value.ToString("N0")
            });
        }
    }
}
