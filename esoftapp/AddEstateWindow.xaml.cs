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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace esoftapp
{
    /// <summary>
    /// Логика взаимодействия для AddEstateWindow.xaml
    /// </summary>
    public partial class AddEstateWindow : Window
    {
        esoftEntities db = new esoftEntities();
        List<string> addressesList = new List<string>();
        public AddEstateWindow()
        {
            InitializeComponent();
            foreach (address a in db.address) addressesList.Add(a.city + " " + a.street);
            address_сb.ItemsSource = addressesList;
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
                    floors_dp.Visibility = Visibility.Collapsed;
                    rooms_dp.Visibility = Visibility.Collapsed;
                }
                else
                {
                    floors_dp.Visibility = Visibility.Visible;
                    rooms_dp.Visibility = Visibility.Visible;
                }
            }   
        }
    }
}
