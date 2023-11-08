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
    /// Логика взаимодействия для EditDealWindow.xaml
    /// </summary>
    public partial class EditDealWindow : Window
    {
        esoftEntities db = new esoftEntities();
        List<string> supplyList = new List<string>();
        List<string> demandList = new List<string>();
        bool isAllowed = true;
        public EditDealWindow(int id)
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
                if (isAllowed) supplyList.Add(s.id.ToString());
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
                if (isAllowed) demandList.Add(d.id.ToString());
                isAllowed = true;
            }

            deals de = db.deals.Find(id);

            supplyList.Add(de.supply_id.ToString());
            demandList.Add(de.demand_id.ToString());

            supply_cb.ItemsSource = supplyList;
            demand_cb.ItemsSource = demandList;

            supply_cb.Text = de.supply_id.ToString();
            demand_cb.Text = de.demand_id.ToString();
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
