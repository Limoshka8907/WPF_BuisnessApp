using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Practice2024.Models;

namespace WPF_Practice2024
{
    /// <summary>
    /// Логика взаимодействия для RealEstateView.xaml
    /// </summary>
    public partial class RealEstateView : UserControl
    {
            private DbforpraktikaContext dbforpraktikaContext;
            private int rowsCount;
        public RealEstateView()
        {
            InitializeComponent();
            webView.Source = new Uri("https://maps.geoapify.com/v1/staticmap?style=osm-carto&width=600&height=400&center=lonlat:65.55378,57.074768&zoom=13.4532&marker=lonlat:65.549416154359,57.0731;color:%23ff0000;size:medium;text:1&apiKey=6c7348250b1d4b6b9963fd2bcc57cede");
            // LINQ-запрос для поиска соответствующих записей
            var selected = dataGrid1.SelectedItem as RealEstate;
            dbforpraktikaContext = new DbforpraktikaContext();
            if (selected != null)
            {
                var disctricts = dbforpraktikaContext.Districts
                    .Where(di => di.IdDistrict == selected.IdDistrict)
                    .ToList();
                MessageBox.Show(disctricts.ToList().ToString());
            }
            else
            {
               // dataGrid1.SelectedItem = 0;
                var realEstates = dbforpraktikaContext.RealEstates
                    .Where(re => re.IdDistrict == 1)
                    .ToList();
            }
        }

        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            dbforpraktikaContext = new DbforpraktikaContext();
            dataGrid1.ItemsSource = dbforpraktikaContext.RealEstates.ToList();
            rowsCount = dataGrid1.Items.Count;

        }

        private void saveData(object sender, RoutedEventArgs e)
        {
            var item = dataGrid1.SelectedItem as RealEstate;
            try
            {
                if (rowsCount == dataGrid1.Items.Count)
                {

                    dbforpraktikaContext.Update(item);
                    dbforpraktikaContext.SaveChanges();
                    MessageBox.Show("Хорошо");
                }
                else
                {
                    dbforpraktikaContext.Add(item);
                    dbforpraktikaContext.SaveChanges();
                    rowsCount = dataGrid1.Items.Count;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Плохо!");
            }
        }

        private void deleteData(object sender, RoutedEventArgs e)
        {
            var item = dataGrid1.SelectedItem as RealEstate;

            try
            {
                dbforpraktikaContext.Remove(item);
                dbforpraktikaContext.SaveChanges();
                MessageBox.Show("Вроде удалил!");
                dataGrid1.ItemsSource = dbforpraktikaContext.Clients.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            // Получаем текст из TextBox
            string searchText = ((TextBox)sender).Text;

            // Выполняем запрос к базе данных с нечетким поиском
            var realEstate = dbforpraktikaContext.RealEstates.ToList()
                .Where(c =>
                    LevenshteinDistance.Calculate(c.AdressCity, searchText) <= 3 ||
                    LevenshteinDistance.Calculate(c.AdressStreet, searchText) <= 3
                )
                .ToList();

            // Отображаем результаты в DataGrid
            dataGrid1.ItemsSource = realEstate;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = dbforpraktikaContext.Clients.ToList();
        }
    }

    public class Coordinate
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

}
