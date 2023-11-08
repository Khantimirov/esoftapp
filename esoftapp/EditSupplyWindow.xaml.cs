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
    /// Логика взаимодействия для EditSupplyWindow.xaml
    /// </summary>
    public partial class EditSupplyWindow : Window
    {
        esoftEntities db = new esoftEntities();
        List<string> clientList = new List<string>();
        List<string> agentList = new List<string>();
        List<string> realestateList = new List<string>();
        public EditSupplyWindow(int client, int agent, int estate, int price)
        {
            InitializeComponent();
            foreach (clients c in db.clients) clientList.Add(c.id.ToString());
            foreach (agents a in db.agents) agentList.Add(a.id.ToString());
            foreach (realestate real in db.realestate) realestateList.Add(real.id.ToString());
            client_cb.ItemsSource = clientList;
            agent_cb.ItemsSource = agentList;
            estate_cb.ItemsSource = realestateList;

            client_cb.Text = client.ToString();
            agent_cb.Text = agent.ToString();
            estate_cb.Text = estate.ToString();
            price_tb.Text = price.ToString();
        }
        private void price_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (char c in price_tb.Text) if (!char.IsDigit(c)) price_tb.Text = price_tb.Text.Trim(c);

            if (price_tb.Text.Length > 0) confirm_btn.IsEnabled = true;
            else confirm_btn.IsEnabled = false;
        }
        private void confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
