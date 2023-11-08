using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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

namespace esoftapp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        esoftEntities db = new esoftEntities();
        List<clients> clientList = new List<clients>();
        List<agents> agentList = new List<agents>();
        List<realestate> realestateList = new List<realestate>();
        List<supplies> supplyList = new List<supplies>();
        ObservableCollection<demands> demandList = new ObservableCollection<demands>();
        List<deals> dealList = new List<deals>();
        public MainWindow()
        {
            InitializeComponent();
            foreach (clients c in db.clients) clientList.Add(c);
            clients_dg.ItemsSource = clientList;
            foreach (agents a in db.agents) agentList.Add(a);
            agents_dg.ItemsSource = agentList;
            foreach (realestate re in db.realestate) realestateList.Add(re);
            realestate_dg.ItemsSource = realestateList;
            foreach (supplies s in db.supplies) supplyList.Add(s);
            supply_dg.ItemsSource = supplyList;
            foreach (demands d in db.demands) demandList.Add(d);
            demands_dg.ItemsSource = demandList;
            foreach (deals d in db.deals) dealList.Add(d);
            deals_dg.ItemsSource = dealList;

            double? sellerComission = 0, totalComission = 0;
            foreach (deals d in db.deals)
            {
                switch (d.supplies.realestate.typeid)
                {
                    case 1:
                        {
                            sellerComission = 36000 + (0.01 * d.supplies.price);
                            totalComission = sellerComission + (0.03 * d.supplies.price);
                            break;
                        }
                    case 2:
                        {
                            sellerComission = 30000 + (0.01 * d.supplies.price);
                            totalComission = sellerComission + (0.03 * d.supplies.price);
                            break;
                        }
                    case 3:
                        {
                            sellerComission = 30000 + (0.02 * d.supplies.price);
                            totalComission = sellerComission + (0.03 * d.supplies.price);
                            break;
                        }
                }
                calculations_list.Items.Add("Сделка " + d.id + " " + " Отчисления от продавца: " + sellerComission + " Отчисления от покупателя: " + (0.03*d.supplies.price) + " Отчисление риэлтору продавца: " + (totalComission *  (float)d.supplies.agents.DealShare/100 ) + " Отчисление риэлтору покупателя: " + (totalComission * (float)d.demands.agents.DealShare/100) + " Отчисление компании: " + (totalComission - (totalComission * (float)d.supplies.agents.DealShare/100) - (totalComission * (float)d.demands.agents.DealShare/100)) );
            }
        }

        private void clients_btn_Click(object sender, RoutedEventArgs e)
        {
            restore_to_default();
            clients_btn.IsEnabled = false;
            clients_panel.Visibility = Visibility.Visible;
        }


        void restore_to_default()
        {
            clients_btn.IsEnabled = true;
            agents_btn.IsEnabled = true;
            realestate_btn.IsEnabled = true;
            supplies_btn.IsEnabled = true;
            demands_btn.IsEnabled = true;
            deals_btn.IsEnabled = true;
            calculations_btn.IsEnabled = true;
            clients_panel.Visibility = Visibility.Collapsed;
            agents_panel.Visibility = Visibility.Collapsed;
            realestate_panel.Visibility = Visibility.Collapsed;
            supply_panel.Visibility = Visibility.Collapsed;
            demands_panel.Visibility = Visibility.Collapsed;
            deals_panel.Visibility = Visibility.Collapsed;
            calculations_panel.Visibility = Visibility.Collapsed;
        }

        private void agents_btn_Click(object sender, RoutedEventArgs e)
        {
            restore_to_default();
            agents_btn.IsEnabled = false;
            agents_panel.Visibility = Visibility.Visible;
        }

        private void realestate_btn_Click(object sender, RoutedEventArgs e)
        {
            restore_to_default();
            realestate_btn.IsEnabled = false;
            realestate_panel.Visibility = Visibility.Visible;
        }

        private void supplies_btn_Click(object sender, RoutedEventArgs e)
        {
            restore_to_default();
            supplies_btn.IsEnabled = false;
            supply_panel.Visibility = Visibility.Visible;
        }

        private void demands_btn_Click(object sender, RoutedEventArgs e)
        {
            restore_to_default();
            demands_btn.IsEnabled = false;
            demands_panel.Visibility = Visibility.Visible;
        }

        private void deals_btn_Click(object sender, RoutedEventArgs e)
        {
            restore_to_default();
            deals_btn.IsEnabled = false;
            deals_panel.Visibility = Visibility.Visible;
        }
        private void calculations_btn_Click(object sender, RoutedEventArgs e)
        {
            restore_to_default();
            calculations_btn.IsEnabled = false;
            calculations_panel.Visibility = Visibility.Visible;
        }
        private void clientadd_btn_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow acw = new AddClientWindow();
            if (acw.ShowDialog() == true)
            {
                db.clients.Add(new clients { FirstName = acw.firstname_tb.Text, LastName = acw.lastname_tb.Text, MiddleName = acw.middlename_tb.Text, email = acw.email_tb.Text, Phone = acw.phone_tb.Text, id = (db.clients.OrderByDescending(s => s.id).First().id) + 1 });
                clientList.Add(new clients { FirstName = acw.firstname_tb.Text, LastName = acw.lastname_tb.Text, MiddleName = acw.middlename_tb.Text, email = acw.email_tb.Text, Phone = acw.phone_tb.Text, id = (db.clients.OrderByDescending(s => s.id).First().id) + 1 });
                db.SaveChanges();
                clients_dg.Items.Refresh();
            }     
        }

        private void clients_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(clients_tb.Text != "")
            {
                foreach (clients c in db.clients)
                {
                    if (c.FirstName.Contains(clients_tb.Text))
                    {
                        clientselected_l.Text = c.id + " " + c.FirstName;
                        clientedit_btn.IsEnabled = true;
                        clientdelete_btn.IsEnabled = true;
                        break;
                    }
                }
            }
            else
            {
                clientedit_btn.IsEnabled = false;
                clientdelete_btn.IsEnabled = false;
                clientselected_l.Text = "";
            }
        }

        private void clientdelete_btn_Click(object sender, RoutedEventArgs e)
        {
            string[] text = clientselected_l.Text.Split(' ');
            clients c = db.clients.Find(Convert.ToInt16(text[0]));
            bool isdeleteallowed = true;
            foreach (demands demand in db.demands)
            {
                if (demand.client_id == c.id)
                {
                    clientedit_btn.IsEnabled = false;
                    clientdelete_btn.IsEnabled = false;
                    clientselected_l.Text = "Его удалять нельзя!";
                    isdeleteallowed = false;
                    break;
                }
            }
            if (isdeleteallowed)
            {
                foreach (supplies supply in db.supplies)
                {
                    if (supply.client_id == c.id)
                    {
                        clientedit_btn.IsEnabled = false;
                        clientdelete_btn.IsEnabled = false;
                        clientselected_l.Text = "Его удалять нельзя!";
                        isdeleteallowed = false;
                        break;
                    }
                }
            }
            if (isdeleteallowed)
            {
                db.clients.Remove(c);
                db.SaveChanges();
                clientedit_btn.IsEnabled = false;
                clientdelete_btn.IsEnabled = false;
                clientselected_l.Text = "Запись удалена";
                clientList.Remove(c);
                clients_dg.Items.Refresh();
            }
        }

        private void clientedit_btn_Click(object sender, RoutedEventArgs e)
        {
            string[] text = clientselected_l.Text.Split(' ');
            clients c = db.clients.Find(Convert.ToInt16(text[0]));
            EditClientWindow ecw = new EditClientWindow(c.FirstName, c.MiddleName, c.LastName, c.Phone, c.email);
            if (ecw.ShowDialog() == true)
            {
                c.FirstName = ecw.firstname_tb.Text;
                c.MiddleName = ecw.middlename_tb.Text;
                c.LastName = ecw.lastname_tb.Text;
                c.Phone = ecw.phone_tb.Text;
                c.email = ecw.email_tb.Text;
                db.SaveChanges();
                clients_dg.Items.Refresh();
            }
        }

        private void agents_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (agents_tb.Text != "")
            {
                foreach (agents a in db.agents)
                {
                    if (a.FirstName.Contains(agents_tb.Text))
                    {
                        agentselected_l.Text = a.id + " " + a.FirstName;
                        agentedit_btn.IsEnabled = true;
                        agentdelete_btn.IsEnabled = true;
                        break;
                    }
                }
            }
            else
            {
                agentedit_btn.IsEnabled = false;
                agentdelete_btn.IsEnabled = false;
                agentselected_l.Text = "";
            }
        }

        private void agentadd_btn_Click(object sender, RoutedEventArgs e)
        {
            AddAgentWindow aaw = new AddAgentWindow();
            if (aaw.ShowDialog() == true)
            {
                db.agents.Add(new agents { FirstName = aaw.firstname_tb.Text, LastName = aaw.lastname_tb.Text, MiddleName = aaw.middlename_tb.Text, DealShare = Convert.ToInt16(aaw.share_tb.Text), id = (db.agents.OrderByDescending(s => s.id).First().id) + 1 });
                agentList.Add(new agents { FirstName = aaw.firstname_tb.Text, LastName = aaw.lastname_tb.Text, MiddleName = aaw.middlename_tb.Text, DealShare = Convert.ToInt16(aaw.share_tb.Text), id = (db.agents.OrderByDescending(s => s.id).First().id) + 1 });
                db.SaveChanges();
                agents_dg.Items.Refresh();
            }
        }

        private void agentedit_btn_Click(object sender, RoutedEventArgs e)
        {
            string[] text = agentselected_l.Text.Split(' ');
            agents a = db.agents.Find(Convert.ToInt16(text[0]));
            EditAgentWindow eaw = new EditAgentWindow(a.FirstName, a.MiddleName, a.LastName, Convert.ToInt32(a.DealShare));
            if (eaw.ShowDialog() == true)
            {
                a.FirstName = eaw.firstname_tb.Text;
                a.MiddleName = eaw.middlename_tb.Text;
                a.LastName = eaw.lastname_tb.Text;
                a.DealShare = Convert.ToInt32(eaw.share_tb.Text);
                db.SaveChanges();
                agents_dg.Items.Refresh();
            }
        }

        private void agentdelete_btn_Click(object sender, RoutedEventArgs e)
        {
            string[] text = agentselected_l.Text.Split(' ');
            agents a = db.agents.Find(Convert.ToInt16(text[0]));
            bool isdeleteallowed = true;
            foreach (demands demand in db.demands)
            {
                if (demand.agent_id == a.id)
                {
                    agentedit_btn.IsEnabled = false;
                    agentdelete_btn.IsEnabled = false;
                    agentselected_l.Text = "Его удалять нельзя!";
                    isdeleteallowed = false;
                    break;
                }
            }
            if (isdeleteallowed)
            {
                foreach (supplies supply in db.supplies)
                {
                    if (supply.agent_id == a.id)
                    {
                        agentedit_btn.IsEnabled = false;
                        agentdelete_btn.IsEnabled = false;
                        agentselected_l.Text = "Его удалять нельзя!";
                        isdeleteallowed = false;
                        break;
                    }
                }
            }
            if (isdeleteallowed)
            {
                db.agents.Remove(a);
                db.SaveChanges();
                agentedit_btn.IsEnabled = false;
                agentdelete_btn.IsEnabled = false;
                agentselected_l.Text = "Запись удалена";
                agentList.Remove(a);
                agents_dg.Items.Refresh();
            }
        }

        private void realestateadd_btn_Click(object sender, RoutedEventArgs e)
        {
            AddEstateWindow aew = new AddEstateWindow();
            if (aew.ShowDialog() == true)
            {
                try
                {
                    db.realestate.Add(new realestate { typeid = (aew.type_сb.SelectedIndex) + 1, addressid = (aew.address_сb.SelectedIndex) + 1, address_house = aew.house_tb.Text, adress_number = aew.number_tb.Text, Coordinate_latitude = (double?)Convert.ToDouble(aew.latitude_tb.Text), Coordinate_longitude = (double?)Convert.ToDouble(aew.longitude_tb.Text), TotalArea = Convert.ToInt16(aew.TotalArea_tb.Text), TotalRooms = Convert.ToInt16(aew.TotalRooms_tb.Text), Floor = Convert.ToInt16(aew.Floor_tb.Text), id = (db.realestate.OrderByDescending(s => s.id).First().id) + 1 });
                    realestateList.Add(new realestate { typeid = (aew.type_сb.SelectedIndex) + 1, addressid = (aew.address_сb.SelectedIndex) + 1, address_house = aew.house_tb.Text, adress_number = aew.number_tb.Text, Coordinate_latitude = (double?)Convert.ToDouble(aew.latitude_tb.Text), Coordinate_longitude = (double?)Convert.ToDouble(aew.longitude_tb.Text), TotalArea = Convert.ToInt16(aew.TotalArea_tb.Text), TotalRooms = Convert.ToInt16(aew.TotalRooms_tb.Text), Floor = Convert.ToInt16(aew.Floor_tb.Text), id = (db.realestate.OrderByDescending(s => s.id).First().id) + 1 });
                    db.SaveChanges();
                    realestate_dg.Items.Refresh();
                }
                catch
                {

                }
            }
        }

        private void realestate_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (realestate_tb.Text != "")
            {
                foreach (realestate re in db.realestate)
                {
                    if (re.id.ToString().Contains(realestate_tb.Text))
                    {
                        realestateselected_l.Text = re.id.ToString();
                        realestateedit_btn.IsEnabled = true;
                        realestatedelete_btn.IsEnabled = true;
                        break;
                    }
                }
            }
            else
            {
                realestateedit_btn.IsEnabled = false;
                realestatedelete_btn.IsEnabled = false;
                realestateselected_l.Text = "";
            }
        }

        private void realestatedelete_btn_Click(object sender, RoutedEventArgs e)
        {
            realestate re = db.realestate.Find(Convert.ToInt16(realestateselected_l.Text));
            bool isdeleteallowed = true;
            foreach (supplies supply in db.supplies)
                {
                    if (supply.realestate_id == re.id)
                    {
                        realestateedit_btn.IsEnabled = false;
                        realestatedelete_btn.IsEnabled = false;
                        realestateselected_l.Text = "Его удалять нельзя!";
                        isdeleteallowed = false;
                        break;
                    }
                }
            if (isdeleteallowed)
            {
                db.realestate.Remove(re);
                db.SaveChanges();
                realestateedit_btn.IsEnabled = false;
                realestatedelete_btn.IsEnabled = false;
                realestateselected_l.Text = "Запись удалена";
                realestateList.Remove(re);
                realestate_dg.Items.Refresh();
            }
        }

        private void realestateedit_btn_Click(object sender, RoutedEventArgs e)
        {
            realestate re = db.realestate.Find(Convert.ToInt16(realestateselected_l.Text));
            EditEstateWindow eew = new EditEstateWindow(re.typeid, re.addressid, re.address_house, re.adress_number, (double)re.Coordinate_longitude, (double)re.Coordinate_latitude, (int)re.TotalArea, (int)re.TotalRooms, (int)re.Floor);
            if (eew.ShowDialog() == true)
            {
                re.typeid = (eew.type_сb.SelectedIndex) + 1;
                re.addressid = (eew.address_сb.SelectedIndex) + 1;
                re.address_house = eew.house_tb.Text;
                re.adress_number = eew.number_tb.Text;
                re.Coordinate_longitude = Convert.ToDouble(eew.longitude_tb.Text);
                re.Coordinate_latitude = Convert.ToDouble(eew.latitude_tb.Text);
                re.TotalArea = Convert.ToInt16(eew.TotalArea_tb.Text);
                re.TotalRooms = Convert.ToInt16(eew.TotalRooms_tb.Text);
                re.Floor = Convert.ToInt16(eew.Floor_tb.Text);

                db.SaveChanges();
                realestate_dg.Items.Refresh();
            }
        }

        private void supply_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (supply_tb.Text != "")
            {
                foreach (supplies s in db.supplies)
                {
                    if (s.id.ToString().Contains(supply_tb.Text))
                    {
                        supplyselected_l.Text = s.id.ToString();
                        supplyedit_btn.IsEnabled = true;
                        supplydelete_btn.IsEnabled = true;
                        break;
                    }
                }
            }
            else
            {
                supplyedit_btn.IsEnabled = false;
                supplydelete_btn.IsEnabled = false;
                supplyselected_l.Text = "";
            }
        }

        private void supplyadd_btn_Click(object sender, RoutedEventArgs e)
        {
            AddSupplyWindow asw = new AddSupplyWindow();
            if (asw.ShowDialog() == true)
            {
                try
                {
                    db.supplies.Add(new supplies { client_id = Convert.ToInt16(asw.client_cb.Text), agent_id = Convert.ToInt16(asw.agent_cb.Text), realestate_id = Convert.ToInt16(asw.estate_cb.Text), price = Convert.ToInt32(asw.price_tb.Text), id = (db.supplies.OrderByDescending(s => s.id).First().id) + 1 });
                    supplyList.Add(new supplies { client_id = Convert.ToInt16(asw.client_cb.Text), agent_id = Convert.ToInt16(asw.agent_cb.Text), realestate_id = Convert.ToInt16(asw.estate_cb.Text), price = Convert.ToInt32(asw.price_tb.Text), id = (db.supplies.OrderByDescending(s => s.id).First().id) + 1 });
                    db.SaveChanges();
                    supply_dg.Items.Refresh();
                }
                catch(Exception exc)
                {
                    MessageBox.Show("чет не то " + exc.Message);
                }
            }
        }

        private void supplyedit_btn_Click(object sender, RoutedEventArgs e)
        {
            supplies s = db.supplies.Find(Convert.ToInt16(supplyselected_l.Text));
            EditSupplyWindow esw = new EditSupplyWindow( Convert.ToInt32(s.client_id), Convert.ToInt32(s.agent_id), Convert.ToInt32(s.realestate_id), Convert.ToInt32(s.price) );
            if (esw.ShowDialog() == true)
            {
                s.client_id = Convert.ToInt16(esw.client_cb.Text);
                s.agent_id = Convert.ToInt16(esw.agent_cb.Text);
                s.realestate_id = Convert.ToInt16(esw.estate_cb.Text);
                s.price = Convert.ToInt32(esw.price_tb.Text);
                db.SaveChanges();
                supply_dg.Items.Refresh();
            }
        }

        private void supplydelete_btn_Click(object sender, RoutedEventArgs e)
        {
            supplies s = db.supplies.Find(Convert.ToInt16(supplyselected_l.Text));
            bool isdeleteallowed = true;
            foreach (deals deal in db.deals)
            {
                if (deal.supply_id == s.id)
                {
                    supplyedit_btn.IsEnabled = false;
                    supplydelete_btn.IsEnabled = false;
                    supplyselected_l.Text = "Его удалять нельзя!";
                    isdeleteallowed = false;
                    break;
                }
            }
            if (isdeleteallowed)
            {
                db.supplies.Remove(s);
                db.SaveChanges();
                supplyedit_btn.IsEnabled = false;
                supplydelete_btn.IsEnabled = false;
                supplyselected_l.Text = "Запись удалена";
                supplyList.Remove(s);
                supply_dg.Items.Refresh();
            }
        }

        private void demandadd_btn_Click(object sender, RoutedEventArgs e)
        {
            AddDemandWindow adw = new AddDemandWindow();
            if (adw.ShowDialog() == true)
            {
                try
                {
                    db.demands.Add(new demands { client_id = Convert.ToInt16(adw.client_cb.Text), agent_id = Convert.ToInt16(adw.agent_cb.Text), type_id = (adw.type_cb.SelectedIndex) + 1, address_id = (adw.address_cb.SelectedIndex) + 1, min_price = adw.minprice_tb.Text, max_price = adw.maxprice_tb.Text, min_area = adw.minarea_tb.Text, max_area = adw.maxarea_tb.Text, min_rooms = adw.minroom_tb.Text, max_rooms = adw.maxroom_tb.Text, min_floor = adw.minroom_tb.Text, max_floor = adw.maxroom_tb.Text, id = (db.demands.OrderByDescending(s => s.id).First().id) + 1 });
                    demandList.Add(new demands { client_id = Convert.ToInt16(adw.client_cb.Text), agent_id = Convert.ToInt16(adw.agent_cb.Text), type_id = (adw.type_cb.SelectedIndex) + 1, address_id = (adw.address_cb.SelectedIndex) + 1, min_price = adw.minprice_tb.Text, max_price = adw.maxprice_tb.Text, min_area = adw.minarea_tb.Text, max_area = adw.maxarea_tb.Text, min_rooms = adw.minroom_tb.Text, max_rooms = adw.maxroom_tb.Text, min_floor = adw.minroom_tb.Text, max_floor = adw.maxroom_tb.Text, id = (db.demands.OrderByDescending(s => s.id).First().id) + 1 });
                    db.SaveChanges();
                    demands_dg.Items.Refresh();
                }
                catch
                {

                }
            }
        }

        private void demandedit_btn_Click(object sender, RoutedEventArgs e)
        {
            demands s = db.demands.Find(Convert.ToInt16(demandselected_l.Text));
            EditDemandWindow edw = new EditDemandWindow(s.id);
            if (edw.ShowDialog() == true)
            {
                s.client_id = Convert.ToInt32(edw.client_cb.Text);
                s.agent_id = Convert.ToInt32(edw.agent_cb.Text);
                s.address_id = (edw.address_cb.SelectedIndex) + 1;
                s.type_id = (edw.type_cb.SelectedIndex) + 1;

                s.min_price = edw.minprice_tb.Text;
                s.max_price = edw.maxprice_tb.Text;
                s.min_area = edw.minarea_tb.Text;
                s.max_area = edw.maxarea_tb.Text;
                s.min_rooms = edw.minroom_tb.Text;
                s.max_rooms = edw.maxroom_tb.Text;
                s.min_floor = edw.minfloor_tb.Text;
                s.max_floor = edw.maxfloor_tb.Text;

                db.SaveChanges();
                demands_dg.Items.Refresh();
            }
        }

        private void demanddelete_btn_Click(object sender, RoutedEventArgs e)
        {
            demands s = db.demands.Find(Convert.ToInt16(demandselected_l.Text));
            bool isdeleteallowed = true;
            foreach (deals deal in db.deals)
            {
                if (deal.demand_id == s.id)
                {
                    demandedit_btn.IsEnabled = false;
                    demanddelete_btn.IsEnabled = false;
                    demandselected_l.Text = "Его удалять нельзя!";
                    isdeleteallowed = false;
                    break;
                }
            }
            if (isdeleteallowed)
            {
                db.demands.Remove(s);
                db.SaveChanges();
                demandedit_btn.IsEnabled = false;
                demanddelete_btn.IsEnabled = false;
                demandselected_l.Text = "Запись удалена";
                demandList.Remove(s);
                demands_dg.Items.Refresh();
            }
        }

        private void demands_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (demand_tb.Text != "")
            {
                foreach (demands s in db.demands)
                {
                    if (s.id.ToString().Contains(demand_tb.Text))
                    {
                        demandselected_l.Text = s.id.ToString();
                        demandedit_btn.IsEnabled = true;
                        demanddelete_btn.IsEnabled = true;
                        break;
                    }
                }
            }
            else
            {
                demandedit_btn.IsEnabled = false;
                demanddelete_btn.IsEnabled = false;
                demandselected_l.Text = "";
            }
        }

        private void deals_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (deals_tb.Text != "")
            {
                foreach (deals s in db.deals)
                {
                    if (s.id.ToString().Contains(deals_tb.Text))
                    {
                        dealselected_l.Text = s.id.ToString();
                        dealedit_btn.IsEnabled = true;
                        dealdelete_btn.IsEnabled = true;
                        break;
                    }
                }
            }
            else
            {
                dealedit_btn.IsEnabled = false;
                dealdelete_btn.IsEnabled = false;
                dealselected_l.Text = "";
            }
        }

        private void dealadd_btn_Click(object sender, RoutedEventArgs e)
        {
            AddDealWindow adw = new AddDealWindow();
            if (adw.ShowDialog() == true)
            {
                try
                {
                    db.deals.Add(new deals { supply_id = Convert.ToInt16(adw.supply_cb.Text), demand_id = Convert.ToInt16(adw.demand_cb.Text), id = (db.deals.OrderByDescending(s => s.id).First().id) + 1 });
                    dealList.Add(new deals { supply_id = Convert.ToInt16(adw.supply_cb.Text), demand_id = Convert.ToInt16(adw.demand_cb.Text), id = (db.deals.OrderByDescending(s => s.id).First().id) + 1 });
                    db.SaveChanges();
                    deals_dg.Items.Refresh();
                }
                catch
                {

                }
            }
        }

        private void dealedit_btn_Click(object sender, RoutedEventArgs e)
        {
            deals dl = db.deals.Find(Convert.ToInt16(dealselected_l.Text));
            EditDealWindow edw = new EditDealWindow(dl.id);
            if (edw.ShowDialog() == true)
            {
                dl.supply_id = Convert.ToInt32(edw.supply_cb.Text);
                dl.demand_id = Convert.ToInt32(edw.demand_cb.Text);

                db.SaveChanges();
                deals_dg.Items.Refresh();
            }
        }

        private void dealdelete_btn_Click(object sender, RoutedEventArgs e)
        {
            deals d = db.deals.Find(Convert.ToInt16(dealselected_l.Text));
            db.deals.Remove(d);
            db.SaveChanges();
            dealedit_btn.IsEnabled = false;
            dealdelete_btn.IsEnabled = false;
            dealselected_l.Text = "Запись удалена";
            dealList.Remove(d);
            deals_dg.Items.Refresh();
        }

        private void realestatetype_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                switch (realestatetype_cb.SelectedIndex)
                {
                    case 0:
                        {
                            realestateList.Clear();
                            foreach (realestate re in db.realestate) realestateList.Add(re);
                            realestate_dg.ItemsSource = realestateList;
                            realestate_dg.Items.Refresh();
                            break;
                        }
                    case 1:
                        {
                            realestateList.Clear();
                            foreach (realestate re in db.realestate) if (re.typeid == realestatetype_cb.SelectedIndex) realestateList.Add(re);
                            realestate_dg.ItemsSource = realestateList;
                            realestate_dg.Items.Refresh();
                            break;
                        }
                    case 2:
                        {
                            realestateList.Clear();
                            foreach (realestate re in db.realestate) if (re.typeid == realestatetype_cb.SelectedIndex) realestateList.Add(re);
                            realestate_dg.ItemsSource = realestateList;
                            realestate_dg.Items.Refresh();
                            break;
                        }
                    case 3:
                        {
                            realestateList.Clear();
                            foreach (realestate re in db.realestate) if (re.typeid == realestatetype_cb.SelectedIndex) realestateList.Add(re);
                            realestate_dg.ItemsSource = realestateList;
                            realestate_dg.Items.Refresh();
                            break;
                        }
                }
            }
            
            
        }
    }
}

