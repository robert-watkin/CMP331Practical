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
using CMP331Practical.Contracts;

namespace CMP331Practical.Views
{
    /// <summary>
    /// Interaction logic for NewProperty.xaml
    /// </summary>
    public partial class NewProperty : Window
    {

        User loggedInUser;

        public NewProperty(User loggedInUser)
        {
            this.loggedInUser = loggedInUser;
            InitializeComponent();
        }

        public void SaveRecord(object sender, RoutedEventArgs e)
        {
            // TODO save record
        }

        private void Dashboard(object sender, RoutedEventArgs e)
        {
            // return to dashboard
            Dashboard d = new Dashboard(loggedInUser);
            d.Show();
            this.Close();

        }
    }
}
