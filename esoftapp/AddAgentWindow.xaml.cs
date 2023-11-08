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
    /// Логика взаимодействия для AddAgentWindow.xaml
    /// </summary>
    public partial class AddAgentWindow : Window
    {
        public AddAgentWindow()
        {
            InitializeComponent();
        }
        private void tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (firstname_tb.Text.Length > 0 && middlename_tb.Text.Length > 0 && lastname_tb.Text.Length > 0) confirm_btn.IsEnabled = true;
            else confirm_btn.IsEnabled = false;
        }

        private void share_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (char c in share_tb.Text) if (!char.IsDigit(c)) share_tb.Text = share_tb.Text.Trim(c);
        }
        private void confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
