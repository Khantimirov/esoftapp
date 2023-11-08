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
    /// Логика взаимодействия для EditDemandWindow.xaml
    /// </summary>
    public partial class EditDemandWindow : Window
    {
        esoftEntities db = new esoftEntities();
        List<string> clientList = new List<string>();
        List<string> agentList = new List<string>();
        List<string> addressList = new List<string>();
        public EditDemandWindow(int id)
        {
            InitializeComponent();
            demands dem = db.demands.Find(id);

            foreach (clients c in db.clients) clientList.Add(c.id.ToString());
            client_cb.ItemsSource = clientList;
            foreach (agents a in db.agents) agentList.Add(a.id.ToString());
            agent_cb.ItemsSource = agentList;
            foreach (address ad in db.address) addressList.Add(ad.city + " " + ad.street);
            address_cb.ItemsSource = addressList;
            address_cb.Text = db.address.Find(dem.address_id).city + " " + db.address.Find(dem.address_id).street;
            type_cb.Text = db.types.Find(dem.type_id).type;

            client_cb.Text = dem.client_id.ToString();
            agent_cb.Text = dem.agent_id.ToString();
            

            type_cb.SelectedIndex = (dem.type_id) - 1;
            address_cb.SelectedIndex = (dem.address_id) - 1;    

            if (dem.min_price != null) minprice_tb.Text = dem.min_price;
            if (dem.max_price != null) maxprice_tb.Text = dem.max_price;
            if (dem.min_area != null) minarea_tb.Text = dem.min_area;
            if (dem.max_area != null) maxarea_tb.Text = dem.max_price;
            if (dem.min_floor != null) minfloor_tb.Text = dem.min_floor;
            if (dem.max_floor != null) maxfloor_tb.Text = dem.max_floor;
            if (dem.min_rooms != null) minroom_tb.Text = dem.min_rooms;
            if (dem.max_rooms != null) maxroom_tb.Text = dem.max_rooms;



             
            //type_cb.Text = db.types.Find(dem.type_id).type;
        }
        private void confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        private void tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (minprice_tb.Text.Length > 0 && maxprice_tb.Text.Length > 0 && minarea_tb.Text.Length > 0 && maxarea_tb.Text.Length > 0 && minroom_tb.Text.Length > 0 && maxroom_tb.Text.Length > 0 && minfloor_tb.Text.Length > 0 && maxfloor_tb.Text.Length > 0) confirm_btn.IsEnabled = true;
            else confirm_btn.IsEnabled = false;
        }

        private void type_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                if (type_cb.Text == "Земля")
                {
                    maxfloor_dp.Visibility = Visibility.Collapsed;
                    minfloor_dp.Visibility = Visibility.Collapsed;
                    maxroom_dp.Visibility = Visibility.Collapsed;
                    minroom_dp.Visibility = Visibility.Collapsed;
                    maxfloor_tb.Text = "0";
                    minfloor_tb.Text = "0";
                    maxroom_tb.Text = "0";
                    minroom_tb.Text = "0";
                }
                else
                {
                    maxfloor_dp.Visibility = Visibility.Visible;
                    minfloor_dp.Visibility = Visibility.Visible;
                    maxroom_dp.Visibility = Visibility.Visible;
                    minroom_dp.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
