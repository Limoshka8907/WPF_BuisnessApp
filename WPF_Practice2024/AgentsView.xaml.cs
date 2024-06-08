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
    /// Логика взаимодействия для AgentsView.xaml
    /// </summary>
    public partial class AgentsView : UserControl
    {
        public AgentsView()
        {
            InitializeComponent();
        }
        private DbforpraktikaContext dbforpraktikaContext;
        private int rowsCount;

        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            dbforpraktikaContext = new DbforpraktikaContext();
            dataGrid1.ItemsSource = dbforpraktikaContext.Agents.ToList();
            rowsCount = dataGrid1.Items.Count;
        }

        private void saveData(object sender, RoutedEventArgs e)
        {
            var item = dataGrid1.SelectedItem as Agent;
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
            var item = dataGrid1.SelectedItem as Agent;
            try
            {
                dbforpraktikaContext.Remove(item);
                dbforpraktikaContext.SaveChanges();
                MessageBox.Show("Вроде удалил!");
                dataGrid1.ItemsSource = dbforpraktikaContext.Agents.ToList();
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
            var agents = dbforpraktikaContext.Agents.ToList()
                .Where(c =>
                    LevenshteinDistance.Calculate(c.FirstName, searchText) <= 3 ||
                    LevenshteinDistance.Calculate(c.LastName, searchText) <= 3
                )
                .ToList();

            // Отображаем результаты в DataGrid
            dataGrid1.ItemsSource = agents;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = dbforpraktikaContext.Agents.ToList();
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lstbox1.Items.Clear();
            var agent = dataGrid1.SelectedItem as Agent;
            dbforpraktikaContext = new DbforpraktikaContext();
            foreach(var a in dbforpraktikaContext.Demands.Where(a => a.IdAgent == agent.IdAgent).ToList())
            {
                lstbox1.Items.Add($"Id потребности {a.IdDemand} Клиент#{a.IdClient} Адрес:{a.Adress}");
            }
            
        }
    }
}
