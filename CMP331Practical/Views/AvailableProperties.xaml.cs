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
using CMP331Practical.Contracts;
using Unity;

namespace CMP331Practical.Views
{
    /// <summary>
    /// Interaction logic for AvailableProperties.xaml
    /// </summary>
    public partial class AvailableProperties : Window
    {

        private User loggedInUser;
        private List<Property> allProperties;

        IRepository<Property> propertyContext;

        public AvailableProperties(User loggedInUser)
        {
            this.loggedInUser = loggedInUser;
            InitializeComponent();

            LoadProperties();
        }

        private void LoadProperties()
        {
            this.propertyContext = ContainerHelper.Container.Resolve<IRepository<Property>>();
            allProperties = propertyContext.Collection().ToList();

            List<Property> availableProperties = new List<Property>();
            foreach (Property property in allProperties)
            {
                if (property.Available == true)
                {
                    if (txtSearch.Text != null && txtSearch.Text != "")
                    {
                        if (property.AddressLine1.ToLower().Contains(txtSearch.Text.ToLower()) || property.PostCode.ToLower().Contains(txtSearch.Text.ToLower()))
                        {
                            availableProperties.Add(property);
                        }
                    }
                    else
                    {
                        availableProperties.Add(property);
                    }
                }
            }

            Results.ItemsSource = availableProperties;
        }

        private void Dashboard(object sender, RoutedEventArgs e)
        {
            Dashboard d = new Dashboard(loggedInUser);
            d.Show();
            this.Close();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            LoadProperties();
        }
    }
}
