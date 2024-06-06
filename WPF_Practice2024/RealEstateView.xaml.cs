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
        public RealEstateView()
        {
            InitializeComponent();
            webView.Source = new Uri("https://maps.geoapify.com/v1/staticmap?style=osm-carto&width=600&height=400&center=lonlat:65.55378,57.074768&zoom=13.4532&marker=lonlat:65.549416154359,57.0731;color:%23ff0000;size:medium;text:1&apiKey=6c7348250b1d4b6b9963fd2bcc57cede");
        }
        private DbforpraktikaContext dbforpraktikaContext;
        private int rowsCount;

        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            dbforpraktikaContext = new DbforpraktikaContext();
            dataGrid1.ItemsSource = dbforpraktikaContext.Clients.ToList();
            rowsCount = dataGrid1.Items.Count;
        }

        private void saveData(object sender, RoutedEventArgs e)
        {
            var item = dataGrid1.SelectedItem as Client;
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
            var item = dataGrid1.SelectedItem as Client;
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
            var clients = dbforpraktikaContext.Clients.ToList()
                .Where(c =>
                    LevenshteinDistance.Calculate(c.FirstName, searchText) <= 3 ||
                    LevenshteinDistance.Calculate(c.LastName, searchText) <= 3
                )
                .ToList();

            // Отображаем результаты в DataGrid
            dataGrid1.ItemsSource = clients;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = dbforpraktikaContext.Clients.ToList();
        }
    }
}
