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
using CMP331Practical.Models;
using CMP331Practical.Views;

namespace CMP331Practical.Views
{
    /// <summary>
    /// Interaction logic for NewInvoice.xaml
    /// </summary>
    public partial class NewInvoice : Window
    {

        User loggedInUser;
        public NewInvoice(User loggedInUser)
        {
            this.loggedInUser = loggedInUser;

            InitializeComponent();
        }

        private void Dashboard(object sender, RoutedEventArgs e)
        {
            Dashboard d = new Dashboard(loggedInUser);
            d.Show();
            this.Close();
        }

        private void SaveRecord(object sender, RoutedEventArgs e)
        {
            // TODO save record
        }
    }
}
