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
    /// Логика взаимодействия для AddSupplyWindow.xaml
    /// </summary>
    public partial class AddSupplyWindow : Window
    {
        esoftEntities db = new esoftEntities();
        List<string> clientList = new List<string>();
        List<string> agentList = new List<string>();
        List<string> realestateList = new List<string>();

        bool isAllowedByPrice = false;
        bool isAllowedByCombo = false;
        public AddSupplyWindow()
        {
            InitializeComponent();
            foreach (clients client in db.clients) clientList.Add(client.id.ToString());
            foreach (agents agent in db.agents) agentList.Add(agent.id.ToString());
            foreach (realestate real in db.realestate) realestateList.Add(real.id.ToString());
            client_cb.ItemsSource = clientList;
            agent_cb.ItemsSource = agentList;
            estate_cb.ItemsSource = realestateList;
        }

        private void price_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (char c in price_tb.Text) if (!char.IsDigit(c)) price_tb.Text = price_tb.Text.Trim(c);

            if (price_tb.Text.Length > 0) isAllowedByPrice = true;
            else isAllowedByPrice = false;

            if (isAllowedByPrice && isAllowedByCombo) confirm_btn.IsEnabled = true;
            else confirm_btn.IsEnabled = false;
        }
        private void confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (client_cb.SelectedItem != null && agent_cb.SelectedItem != null && estate_cb.SelectedItem != null) isAllowedByCombo = true;
            else isAllowedByCombo = false;

            if (isAllowedByPrice && isAllowedByCombo) confirm_btn.IsEnabled = true;
            else confirm_btn.IsEnabled = false;
        }
    }
}
