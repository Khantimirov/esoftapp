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
    /// Логика взаимодействия для AddDemandWindow.xaml
    /// </summary>
    public partial class AddDemandWindow : Window
    {
        esoftEntities db = new esoftEntities();
        List<string> clientList = new List<string>();
        List<string> agentList = new List<string>();
        List<string> addressList = new List<string>();
        public AddDemandWindow()
        {
            InitializeComponent();
            foreach (clients c in db.clients) clientList.Add(c.id.ToString());
            client_cb.ItemsSource = clientList;
            client_cb.Text = db.clients.First().id.ToString();
            foreach (agents a in db.agents) agentList.Add(a.id.ToString());
            agent_cb.ItemsSource = agentList;
            agent_cb.Text = db.agents.First().id.ToString();
            foreach (address ad in db.address) addressList.Add(ad.city + " " + ad.street);
            address_cb.ItemsSource = addressList;
            address_cb.Text = db.address.First().city.Trim() + " " + db.address.First().street.Trim();
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
