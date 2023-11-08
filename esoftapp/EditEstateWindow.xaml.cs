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
using System.Windows.Shapes;

namespace esoftapp
{
    /// <summary>
    /// Логика взаимодействия для EditEstateWindow.xaml
    /// </summary>
    public partial class EditEstateWindow : Window
    {
        esoftEntities db = new esoftEntities();
        List<string> addressesList = new List<string>();
        public EditEstateWindow(int typeid, int addressid, string house, string number, double longitdue, double latitude, int area, int room, int floor)
        {
            InitializeComponent();
            foreach (address a in db.address) addressesList.Add(a.city + " " + a.street);
            address_сb.ItemsSource = addressesList;
            type_сb.SelectedIndex = (typeid) - 1;
            address_сb.SelectedIndex = (addressid) - 1;
            house_tb.Text = house;
            number_tb.Text = number;
            longitude_tb.Text = longitdue.ToString();
            latitude_tb.Text = latitude.ToString();
            TotalArea_tb.Text = area.ToString();
            TotalRooms_tb.Text = room.ToString();
            Floor_tb.Text = floor.ToString();

        }
        private void confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Convert.ToDouble(latitude_tb.Text);
                Convert.ToDouble(longitude_tb.Text);
                Convert.ToInt16(TotalArea_tb.Text);
                Convert.ToInt16(TotalRooms_tb.Text);
                Convert.ToInt16(Floor_tb.Text);
            }
            catch
            {
                error_l.Text = "Ошибка типа данных";
            }
            finally
            {
                this.DialogResult = true;
            }
        }

        private void type_сb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                if (type_сb.SelectedIndex == 2)
                {
                    Floor_dp.Visibility = Visibility.Collapsed;
                    TotalRooms_tb.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Floor_dp.Visibility = Visibility.Visible;
                    TotalRooms_tb.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
