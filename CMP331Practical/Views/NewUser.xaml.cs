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
    /// Interaction logic for NewUser.xaml
    /// </summary>
    public partial class NewUser : Window
    {

        private User loggedInUser;
        public NewUser(User loggedInUser)
        {
            this.loggedInUser = loggedInUser;

            // initialise WPF components
            InitializeComponent();
        }

        private void SaveRecord(object sender, RoutedEventArgs e)
        {
            // TODO save record
        }


        private void Dashboard(object sender, RoutedEventArgs e)
        {
            Dashboard d = new Dashboard(loggedInUser);
            d.Show();
            this.Close();
        }
    }
}
