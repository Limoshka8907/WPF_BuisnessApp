using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для SupplyView.xaml
    /// </summary>
    public partial class SupplyView : UserControl
    {
        public SupplyView()
        {
            InitializeComponent();
        }
        private DbforpraktikaContext dbforpraktikaContext;
        private int rowsCount;
        private int rowsCount2;

        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid1.CanUserAddRows = false;
            dbforpraktikaContext = new DbforpraktikaContext();
            dataGrid1.ItemsSource = dbforpraktikaContext.Supplies.ToList();
            dataGrid2.ItemsSource = dbforpraktikaContext.Deals.ToList();
            dataGrid2.CanUserAddRows = false;
            foreach(var s in dbforpraktikaContext.Supplies.ToList())
            {
                com1.Items.Add($"{s.IdSupply} Недвижисмость: #{s.IdRealEstate}");

            }
            foreach (var d in dbforpraktikaContext.Demands.ToList())
            {
                com2.Items.Add($"{d.IdDemand} Тип недвижимости: {d.TypeRealEstate} Объект: #{d.IdHouse}{d.IdLand}{d.IdApartment}");
            }
            rowsCount = dataGrid1.Items.Count;
            rowsCount2 = dataGrid2.Items.Count;
        }

        private void saveData(object sender, RoutedEventArgs e)
        {
            var item = dataGrid1.SelectedItem as Supply;
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
                if (dataGrid1.SelectedItem != null)
                {
                    MessageBox.Show(ex.Message, "Плохо!");
                }
            }
            var item2 = dataGrid2.SelectedItem as Deal;
            try
            {
                if (rowsCount2 == dataGrid2.Items.Count)
                {

                    dbforpraktikaContext.Update(item2);
                    dbforpraktikaContext.SaveChanges();
                    MessageBox.Show("Хорошо");
                    rowsCount2 = dataGrid2.Items.Count;

                }
                else
                {
                    dbforpraktikaContext.Add(item2);
                    dbforpraktikaContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                if(dataGrid2.SelectedItem != null)
                {
                    MessageBox.Show(ex.Message, "Плохо!");
                }
            }
        }

        private void deleteData(object sender, RoutedEventArgs e)
        {
            var item = dataGrid1.SelectedItem as Supply;
            try
            {
                dbforpraktikaContext.Remove(item);
                dbforpraktikaContext.SaveChanges();
                MessageBox.Show("Вроде удалил!");
                dataGrid1.ItemsSource = dbforpraktikaContext.Supplies.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }

        }
        private void doDeal(object sender, RoutedEventArgs e)
        {
            try
            {
                dbforpraktikaContext = new DbforpraktikaContext();
                int id_supply = int.Parse(com1.SelectedItem.ToString().Split(' ')[0]);
                int id_demand = int.Parse(com2.SelectedItem.ToString().Split(' ')[0]);
                dbforpraktikaContext.Deals.Add(new Deal { Id_Demand = id_demand, Id_Supply = id_supply });
                dbforpraktikaContext.SaveChanges();
                dataGrid2.ItemsSource = dbforpraktikaContext.Deals.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            //// Получаем текст из TextBox
            //string searchText = ((TextBox)sender).Text;

            //// Выполняем запрос к базе данных с нечетким поиском
            //var Supplies = dbforpraktikaContext.Supplies.ToList()
            //    .Where(c =>
            //        LevenshteinDistance.Calculate(c.FirstName, searchText) <= 3 ||
            //        LevenshteinDistance.Calculate(c.LastName, searchText) <= 3
            //    )
            //    .ToList();

            //// Отображаем результаты в DataGrid
            //dataGrid1.ItemsSource = Supplies;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = dbforpraktikaContext.Supplies.ToList();
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataGrid2_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            dataGrid1.CanUserAddRows = false;
            if (addDealPanel != null)
            {
                addDealPanel.Visibility = Visibility.Hidden;
            }
            if (addSupplyPanel != null) addSupplyPanel.Visibility = Visibility.Visible;
            dbforpraktikaContext = new DbforpraktikaContext();
            dataGrid1.ItemsSource = dbforpraktikaContext.Supplies.ToList();
            foreach (var a in dbforpraktikaContext.Agents.ToList())
            {
                com1_1.Items.Add($"{a.IdAgent} {a.MiddleName}");

            }
            foreach (var c in dbforpraktikaContext.Clients.ToList())
            {
                com1_2.Items.Add($"{c.IdClient} {c.MiddleName}");

            }
            foreach (var r in dbforpraktikaContext.RealEstates.ToList())
            {
                com1_3.Items.Add($"{r.IdRealEstate} {r.AdressCity} {r.AdressHouse}");

            }
        }
        private void addSupply(object sender, RoutedEventArgs e)
        {
            if (com1_1.SelectedItem != null && com1_2.SelectedItem != null && com1_3.SelectedItem != null)
            {
                dbforpraktikaContext = new DbforpraktikaContext();
                int plusPrice = 0;

                int realEstate = int.Parse(com1_3.SelectedItem.ToString().Split(' ')[0]);
                foreach (var i in dbforpraktikaContext.RealEstates.Where(x => x.IdRealEstate == realEstate))
                {
                    if (i.IdHouse != null)
                    {
                        plusPrice = 30000 + int.Parse(priceBox.Text) / 100;
                    }
                    else if (i.IdApartment != null)
                    {
                        plusPrice = 36000 + int.Parse(priceBox.Text) / 100;
                    }
                    else if (i.IdLand != null)
                    {
                        plusPrice = 30000 + int.Parse(priceBox.Text) / 100 * 2;
                    }
                }

                dbforpraktikaContext.Supplies.Add(new Supply
                {
                    IdAgent = int.Parse(com1_1.SelectedItem.ToString().Split(' ')[0]),
                    IdClient = int.Parse(com1_2.SelectedItem.ToString().Split(' ')[0]),
                    IdRealEstate = int.Parse(com1_3.SelectedItem.ToString().Split(' ')[0]),
                    Price = plusPrice + int.Parse(priceBox.Text)
                });
                dbforpraktikaContext.SaveChanges();
                dataGrid1.ItemsSource = dbforpraktikaContext.Supplies.ToList();
            }
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            if (addSupplyPanel != null)
            {
                addSupplyPanel.Visibility = Visibility.Hidden;
            }
            addDealPanel.Visibility = Visibility.Visible ;
            dataGrid2.CanUserAddRows = false;
        }
    }
}

