using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для AddDealWindow.xaml
    /// </summary>
    public partial class AddDealWindow : Window
    {
        esoftEntities db = new esoftEntities();
        List<string> supplyList = new List<string>();
        List<string> demandList = new List<string>();
        bool isAllowed = true;
        public AddDealWindow()
        {
            InitializeComponent();
            foreach (supplies s in db.supplies)
            {
                foreach (deals dls in db.deals)
                {
                    if (dls.supply_id == s.id)
                    {
                        isAllowed = false;
                        break;
                    }
                }
                if(isAllowed) supplyList.Add(s.id.ToString());
                isAllowed = true;
            }
            isAllowed = true;
            foreach (demands d in db.demands)
            {
                foreach (deals dls in db.deals)
                {
                    if (dls.demand_id == d.id)
                    {
                        isAllowed = false;
                        break;
                    }
                }
                if (isAllowed)demandList.Add(d.id.ToString());
                isAllowed = true;
            }
            supply_cb.ItemsSource = supplyList;
            demand_cb.ItemsSource = demandList;
        }
        private void confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (supply_cb.SelectedItem != null && demand_cb.SelectedItem != null) confirm_btn.IsEnabled = true;
            else confirm_btn.IsEnabled = false;
        }
    }
}
