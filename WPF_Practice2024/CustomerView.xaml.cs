using Microsoft.EntityFrameworkCore;
using System.Text;
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
    public partial class CustomerView : UserControl
    {
        private DbforpraktikaContext dbforpraktikaContext;
        private int rowsCount;
        public CustomerView()
        {
            InitializeComponent();
        }

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

        private void dataGrid1_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            lstbox1.Items.Clear();
            lstbox2.Items.Clear();
            if (dataGrid1.SelectedItem != null)
            {

                var client = dataGrid1.SelectedItem as Client;
                dbforpraktikaContext = new DbforpraktikaContext();
                foreach (var a in dbforpraktikaContext.Demands.Where(a => a.IdClient == client.IdClient).ToList())
                {
                    lstbox1.Items.Add($"Id потребности {a.IdDemand} Агент#{a.IdAgent} Адрес:{a.Adress}");
                }
                foreach (var a in dbforpraktikaContext.Supplies.Where(a => a.IdClient == client.IdClient).ToList())
                {
                    lstbox2.Items.Add($"Id предложения {a.IdSupply} Агент#{a.IdAgent} Цена:{a.Price}");
                }
            }

        }
    }

    public static class LevenshteinDistance
    {
        public static int Calculate(string source, string target)
        {
            if (string.IsNullOrEmpty(source))
            {
                return target.Length;
            }

            if (string.IsNullOrEmpty(target))
            {
                return source.Length;
            }

            int n = source.Length;
            int m = target.Length;

            int[,] distance = new int[n + 1, m + 1];

            for (int i = 0; i <= n; i++)
            {
                distance[i, 0] = i;
            }

            for (int j = 0; j <= m; j++)
            {
                distance[0, j] = j;
            }

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (source[i - 1] == target[j - 1]) ? 0 : 1;

                    distance[i, j] = Math.Min(
                        Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                        distance[i - 1, j - 1] + cost
                    );
                }
            }

            return distance[n, m];
        }

    }
}
