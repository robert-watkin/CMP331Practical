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
using CMP331Practical.Contracts;
using CMP331Practical.Views;
using CMP331Practical.Models;

namespace CMP331Practical.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Dashboard : Window
    {

        public User loggedInUser;

        public Dashboard(User loggedInUser)
        {
            InitializeComponent();
            LoadNotifications();
            this.loggedInUser = loggedInUser;
        }


        private void SignOut(object sender, RoutedEventArgs e)
        {
            MainWindow l = new MainWindow();
            l.Show();
            this.Close();
        }

        private void LoadNotifications()
        {
            // TODO outstanding payments - X maintainance staff
            // TODO outstanding inspections
        }

        private void PropertyManagement(object sender, RoutedEventArgs e)
        {
            PropertyManagement pm = new PropertyManagement(loggedInUser);
            pm.Show();
            this.Close();
        }

        private void AvailableProperties(object sender, RoutedEventArgs e)
        {
            // TODO available properties
        }

        private void UserManagement(object sender, RoutedEventArgs e)
        {
            // TODO user management
        }
    }
}
