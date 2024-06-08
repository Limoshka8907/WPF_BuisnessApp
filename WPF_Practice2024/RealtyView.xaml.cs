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
    /// Логика взаимодействия для RealtyView.xaml
    /// </summary>
    public partial class RealtyView : UserControl
    {
        public RealtyView()
        {
            InitializeComponent();
        }
        private DbforpraktikaContext dbforpraktikaContext;
        private int rowsCount;

        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            rowsCount = dataGrid1.Items.Count;
        }

        private void saveData(object sender, RoutedEventArgs e)
        {
            if (r1.IsChecked == true)
            {
                var item = dataGrid1.SelectedItem as Apartment;
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
            else if (r2.IsChecked == true)
            {
                var item = dataGrid1.SelectedItem as House;
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
            else if (r3.IsChecked == true)
            {
                var item = dataGrid1.SelectedItem as Land;
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
        }

        private void deleteData(object sender, RoutedEventArgs e)
        {
            if (r1.IsChecked == true)
            {
                var item = dataGrid1.SelectedItem as Apartment;
                try
                {
                    dbforpraktikaContext.Remove(item);
                    dbforpraktikaContext.SaveChanges();
                    MessageBox.Show("Вроде удалил!");
                    dataGrid1.ItemsSource = dbforpraktikaContext.Apartments.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!");
                }
            }
            else if (r2.IsChecked == true)
            {
                var item = dataGrid1.SelectedItem as House;
                try
                {
                    dbforpraktikaContext.Remove(item);
                    dbforpraktikaContext.SaveChanges();
                    MessageBox.Show("Вроде удалил!");
                    dataGrid1.ItemsSource = dbforpraktikaContext.Houses.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!");
                }
            }
            else if (r3.IsChecked == true)
            {
                var item = dataGrid1.SelectedItem as Land;
                try
                {
                    dbforpraktikaContext.Remove(item);
                    dbforpraktikaContext.SaveChanges();
                    MessageBox.Show("Вроде удалил!");
                    dataGrid1.ItemsSource = dbforpraktikaContext.Lands.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!");
                }
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (r1.IsChecked == true)
            {
                dataGrid1.ItemsSource = dbforpraktikaContext.Apartments.ToList();
            }
            else if (r2.IsChecked == true)
            {
                dataGrid1.ItemsSource = dbforpraktikaContext.Houses.ToList();
            }
            else if (r3.IsChecked == true)
            {
                dataGrid1.ItemsSource = dbforpraktikaContext.Lands.ToList();
            }
        }

        private void r1_Checked(object sender, RoutedEventArgs e)
        {
            dbforpraktikaContext = new DbforpraktikaContext();
            dataGrid1.ItemsSource = dbforpraktikaContext.Apartments.ToList();  
        }

        private void r2_Checked(object sender, RoutedEventArgs e)
        {
            dbforpraktikaContext = new DbforpraktikaContext();
            dataGrid1.ItemsSource = dbforpraktikaContext.Houses.ToList();
        }

        private void r3_Checked(object sender, RoutedEventArgs e)
        {
            dbforpraktikaContext = new DbforpraktikaContext();
            dataGrid1.ItemsSource = dbforpraktikaContext.Lands.ToList();
        }
    }
}

