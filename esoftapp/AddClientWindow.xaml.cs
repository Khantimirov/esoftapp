using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
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
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        esoftEntities db = new esoftEntities();
        public AddClientWindow()
        {
            InitializeComponent();
        }

        private void tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (phone_tb.Text.Length > 0 || email_tb.Text.Length > 0) confirm_btn.IsEnabled = true;
            else confirm_btn.IsEnabled = false;
        }

        private void confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
