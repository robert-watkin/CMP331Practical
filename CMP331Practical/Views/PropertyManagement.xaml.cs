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
    /// Interaction logic for PropertyManagement.xaml
    /// </summary>
    public partial class PropertyManagement : Window
    {
        private User loggedInUser;
        public PropertyManagement(User loggedInUser)
        {
            InitializeComponent();
            this.loggedInUser = loggedInUser;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Dashboard d = new Dashboard(loggedInUser);
            d.Show();
            this.Close();
        }

        private void FirstRecord(object sender, RoutedEventArgs e)
        {
            // TOOD first record
        }

        private void PreviousRecord(object sender, RoutedEventArgs e)
        {
            // TODO previous record
        }

        private void SaveRecord(object sender, RoutedEventArgs e)
        {
            // TODO save record
        }

        private void DeleteProperty(object sender, RoutedEventArgs e)
        {

        }

        private void New(object sender, RoutedEventArgs e)
        {

        }
    }
}
