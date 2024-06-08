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
    /// Логика взаимодействия для DemandsView.xaml
    /// </summary>
    public partial class DemandsView : UserControl
    {
        public DemandsView()
        {
            InitializeComponent();
        }
        private DbforpraktikaContext dbforpraktikaContext;
        private int rowsCount;
        static int agentId;
        static int clientId;
        static int minPrice;
        static int maxPrice;
        static string address;
        static string type_realty;
        static int id_realty;

        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            rowsCount = dataGrid1.Items.Count;
        }

        private void saveData(object sender, RoutedEventArgs e)
        {
            if (r1.IsChecked == true)
            {
                var item = dataGrid1.SelectedItem as DemandApartment;
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
                var item = dataGrid1.SelectedItem as DemandHouse;
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
                var item = dataGrid1.SelectedItem as DemandLand;
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
            else if (r4.IsChecked == true)
            {
                var item = dataGrid1.SelectedItem as Demand;
                try
                {
                    dbforpraktikaContext.Update(item);
                    dbforpraktikaContext.SaveChanges();
                    MessageBox.Show("Хорошо");
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
                var item = dataGrid1.SelectedItem as DemandApartment;
                try
                {
                    dbforpraktikaContext.Remove(item);
                    dbforpraktikaContext.SaveChanges();
                    MessageBox.Show("Вроде удалил!");
                    dataGrid1.ItemsSource = dbforpraktikaContext.DemandApartments.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!");
                }
            }
            else if (r2.IsChecked == true)
            {
                var item = dataGrid1.SelectedItem as DemandHouse;
                try
                {
                    dbforpraktikaContext.Remove(item);
                    dbforpraktikaContext.SaveChanges();
                    MessageBox.Show("Вроде удалил!");
                    dataGrid1.ItemsSource = dbforpraktikaContext.DemandHouses.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!");
                }
            }
            else if (r3.IsChecked == true)
            {
                var item = dataGrid1.SelectedItem as DemandLand;
                try
                {
                    dbforpraktikaContext.Remove(item);
                    dbforpraktikaContext.SaveChanges();
                    MessageBox.Show("Вроде удалил!");
                    dataGrid1.ItemsSource = dbforpraktikaContext.DemandLands.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка!");
                }
            }
            else if (r4.IsChecked == true)
            {
                var item = dataGrid1.SelectedItem as Demand;
                try
                {
                    dbforpraktikaContext.Remove(item);
                    dbforpraktikaContext.SaveChanges();
                    MessageBox.Show("Вроде удалил!");
                    dataGrid1.ItemsSource = dbforpraktikaContext.Demands.ToList();
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
                dataGrid1.ItemsSource = dbforpraktikaContext.DemandApartments.ToList();
            }
            else if (r2.IsChecked == true)
            {
                dataGrid1.ItemsSource = dbforpraktikaContext.DemandHouses.ToList();
            }
            else if (r3.IsChecked == true)
            {
                dataGrid1.ItemsSource = dbforpraktikaContext.DemandLands.ToList();
            }
            else if (r4.IsChecked == true)
            {
                dataGrid1.ItemsSource = dbforpraktikaContext.Demands.ToList();
            }
        }

        private void r1_Checked(object sender, RoutedEventArgs e)
        {
            if (demandPanel != null) demandPanel.Visibility = Visibility.Hidden;
            dataGrid1.CanUserAddRows = true;
            dbforpraktikaContext = new DbforpraktikaContext();
            dataGrid1.ItemsSource = dbforpraktikaContext.DemandApartments.ToList();
        }

        private void r2_Checked(object sender, RoutedEventArgs e)
        {
            if (demandPanel != null) demandPanel.Visibility = Visibility.Hidden;
            dataGrid1.CanUserAddRows = true;
            dbforpraktikaContext = new DbforpraktikaContext();
            dataGrid1.ItemsSource = dbforpraktikaContext.DemandHouses.ToList();
        }

        private void r3_Checked(object sender, RoutedEventArgs e)
        {
            if (demandPanel != null) demandPanel.Visibility = Visibility.Hidden;
            dataGrid1.CanUserAddRows = true;
            dbforpraktikaContext = new DbforpraktikaContext();
            dataGrid1.ItemsSource = dbforpraktikaContext.DemandLands.ToList();
        }
        private void r4_Checked(object sender, RoutedEventArgs e)
        {
            com1.Items.Clear();
            com2.Items.Clear();
            com3.Items.Clear();
            dataGrid1.CanUserAddRows = false;
            dbforpraktikaContext = new DbforpraktikaContext();
            dataGrid1.ItemsSource = dbforpraktikaContext.Demands.ToList();
            demandPanel.Visibility = Visibility.Visible;
            foreach (var agent in dbforpraktikaContext.Agents.ToList())
            {
                com1.Items.Add(agent.IdAgent + " " + agent.FirstName);
            }
            foreach (var client in dbforpraktikaContext.Clients.ToList())
            {
                com2.Items.Add(client.IdClient + " " + client.FirstName);
            }
            com3.Items.Add("квартира");
            com3.Items.Add("дом");
            com3.Items.Add("земля");

        }

        private void com3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            com4.Items.Clear();
            if (com3.SelectedItem != null && com3.SelectedItem.ToString() == "дом")
            {
                dbforpraktikaContext = new DbforpraktikaContext();
                foreach (var house in dbforpraktikaContext.Houses.ToList())
                {
                    com4.Items.Add(house.IdHouse);
                }
            }
            else if (com3.SelectedItem != null && com3.SelectedItem.ToString() == "квартира")
            {
                dbforpraktikaContext = new DbforpraktikaContext();
                foreach (var apartment in dbforpraktikaContext.Apartments.ToList())
                {
                    com4.Items.Add(apartment.IdApartment);
                }
            }
            else if (com3.SelectedItem != null && com3.SelectedItem.ToString() == "земля")
            {
                dbforpraktikaContext = new DbforpraktikaContext();
                foreach (var land in dbforpraktikaContext.Lands.ToList())
                {
                    com4.Items.Add(land.IdLand);
                }
            }

        }
        private void newDemand(object sender, EventArgs e)
        {
            int? a = int.Parse(com4.SelectedItem.ToString());
            if (com1.SelectedItem != null && com2.SelectedItem != null && com3.SelectedItem != null && com4.SelectedItem != null)
            {
                dbforpraktikaContext = new DbforpraktikaContext();
                int? sliderVal1;
                int? sliderVal2;

                if (int.Parse(slider.SL_Bat1.Value.ToString()) == 0 && int.Parse(slider.SL_Bat2.Value.ToString()) == 0)
                {
                    sliderVal1 = null;
                    sliderVal2 = null;
                }
                else
                {
                    sliderVal1 = int.Parse(slider.SL_Bat1.Value.ToString());
                    sliderVal2 = int.Parse(slider.SL_Bat2.Value.ToString());
                }
                if (com3.SelectedItem == "квартира")
                {
                    dbforpraktikaContext.Demands.Add(new Demand
                    {
                        IdAgent = int.Parse(com1.SelectedItem.ToString().Split(' ')[0]),
                        IdClient = int.Parse(com2.SelectedItem.ToString().Split(' ')[0]),
                        IdApartment = int.Parse(com4.SelectedItem.ToString()),
                        Adress = txtAddress.Text,
                        MinPrice = sliderVal1,
                        MaxPrice = sliderVal2,
                        TypeRealEstate = com3.SelectedItem.ToString()
                    });
                }
                else if (com3.SelectedItem == "дом")
                {
                    dbforpraktikaContext.Demands.Add(new Demand
                    {
                        IdAgent = int.Parse(com1.SelectedItem.ToString().Split(' ')[0]),
                        IdClient = int.Parse(com2.SelectedItem.ToString().Split(' ')[0]),
                        IdHouse = int.Parse(com4.SelectedItem.ToString()),
                        Adress = txtAddress.Text,
                        MinPrice = sliderVal1,
                        MaxPrice = sliderVal2,
                        TypeRealEstate = com3.SelectedItem.ToString()

                    });
                }
                else if (com3.SelectedItem == "земля")
                {
                    dbforpraktikaContext.Demands.Add(new Demand
                    {
                        IdAgent = int.Parse(com1.SelectedItem.ToString().Split(' ')[0]),
                        IdClient = int.Parse(com2.SelectedItem.ToString().Split(' ')[0]),
                        IdLand = int.Parse(com4.SelectedItem.ToString()),
                        Adress = txtAddress.Text,
                        MinPrice = sliderVal1,
                        MaxPrice = sliderVal2,
                        TypeRealEstate = com3.SelectedItem.ToString()

                    });
                }
                dbforpraktikaContext.SaveChanges();
                dataGrid1.ItemsSource = dbforpraktikaContext.Demands.ToList();
            }
            else
            {
                MessageBox.Show("Проверьте поля для заполнения");
            }

        }
    }
}

