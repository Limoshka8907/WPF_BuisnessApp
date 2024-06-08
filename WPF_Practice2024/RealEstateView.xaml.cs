using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
        }

        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            dbforpraktikaContext = new DbforpraktikaContext();
            dataGrid1.ItemsSource = dbforpraktikaContext.RealEstates.ToList();
            rowsCount = dataGrid1.Items.Count;
            // webView.Source = new Uri("https://maps.geoapify.com/v1/staticmap?style=osm-carto&width=600&height=400&center=lonlat:65.55378,57.074768&zoom=13.4532&marker=lonlat:65.549416154359,57.0731;color:%23ff0000;size:medium;text:1&apiKey=6c7348250b1d4b6b9963fd2bcc57cede");

            // LINQ-запрос для поиска соответствующих записей

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


        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selected = dataGrid1.SelectedItem as RealEstate;
            if (selected == null) { selected = new RealEstate { IdDistrict = 647 }; }
            dbforpraktikaContext = new DbforpraktikaContext();
            if (selected != null)
            {

                List<District> disctricts = dbforpraktikaContext.Districts
                    .Where(di => di.IdDistrict == selected.IdDistrict)
                    .ToList();
                string data = disctricts[0].Area;
                // Удаление лишних символов
                data = data.Trim('(', ')', ' ');

                // Разделение строки на пары координат
                string[] coordinatePairs = data.Split(',');

                // Создание списка координат
                List<Coordinate> coordinates = new List<Coordinate>();
                for (int i = 0; i < coordinatePairs.Length; i += 2)
                {// Замена точки на запятую, если используется точка как разделитель
                    if (coordinatePairs[i].Contains("."))
                    {
                        coordinatePairs[i] = coordinatePairs[i].Replace('.', '.');
                    }
                    string latitude = coordinatePairs[i];
                    string longitude = coordinatePairs[i + 1];

                    coordinates.Add(new Coordinate { Latitude = latitude, Longitude = longitude });

                }

                // Инициализация списка маркеров
                var markers = new List<string>();
                string baseurl = "";
                // Добавление маркеров для каждой пары координат
                for (int i = 0; i < coordinates.Count; i++)
                {
                    var lon = coordinates[i].Longitude.TrimEnd(')').TrimStart('(');
                    var lat = coordinates[i].Latitude.TrimStart('(').TrimEnd(')');
                    string marker = "";
                    if (i == 0)
                    {
                        marker = $"marker=lonlat:{lon},{lat};color:%23ff0000;size:medium;text:{i + 1}";
                    }
                    else
                    {
                        marker = $"|lonlat:{lon},{lat};color:%23ff0000;size:medium;text:{i + 1}";
                    }
                    markers.Add(marker);
                }

                baseurl = $"https://maps.geoapify.com/v1/staticmap?style=osm-bright&width=600&height=400&center=lonlat:{coordinates[0].Longitude.TrimEnd(')')},{coordinates[0].Latitude}&zoom=13.4532";


                // Создание полного URL-адреса карты с маркерами
                string url = $"{baseurl}&";
                for (int i = 0; i < markers.Count; i++)
                {

                    url += ($"{markers[i]}");
                }
                url += "&apiKey=6c7348250b1d4b6b9963fd2bcc57cede";
                // Открытие карты в веб-браузере

                webView.Source = (new Uri(url));
            }
            else
            {
                // dataGrid1.SelectedItem = 0;
                var realEstates = dbforpraktikaContext.RealEstates
                    .Where(re => re.IdDistrict == 1)
                    .ToList();
            }


        }
    }

    public class Coordinate
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

}
