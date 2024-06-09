using Microsoft.Identity.Client;
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

namespace WPF_Practice2024
{
    /// <summary>
    /// Логика взаимодействия для HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        private List<Record> records = new List<Record>();
        public HomeView()
        {
            InitializeComponent();
            com1.Items.Clear();
            com1.Items.Add("Встреча с клиентом");
            com1.Items.Add("Показ");
            com1.Items.Add("Звонок");
        }
        private void createRecord(object sender, RoutedEventArgs e)
        {
            string date = "";
            if (calendar.SelectedDate.HasValue)
            {
                date = calendar.SelectedDate.Value.ToString("dd/MM/yyyy");
                if (com1.SelectedItem != null)
                {
                    records.Add(new Record { date = date, title = com1.SelectedItem.ToString()});
                    lstBox.Items.Add(date + " " + com1.SelectedItem);
                }
            }
        }
        private void deleteRecord(object sender, RoutedEventArgs e)
        {

        }

        private void calendar_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
           
        }

        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            lstBox.Items.Clear();
            foreach (var record in records)
            {
                if (calendar.SelectedDate.Value.ToString("dd/MM/yyyy") == record.date)
                {
                    lstBox.Items.Add(record.date + " " + record.title);
                }
            }
        }
    }

    public class Record
    {
        public string date;
        public string title;
    }
}
