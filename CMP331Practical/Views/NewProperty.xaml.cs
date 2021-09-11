using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CMP331Practical.Models;
using CMP331Practical.Contracts;
using Unity;

namespace CMP331Practical.Views
{
    /// <summary>
    /// Interaction logic for NewProperty.xaml
    /// </summary>
    public partial class NewProperty : Window
    {
        // TODO adjust monthly rent selection to allow decimals - property management and this

        IRepository<Property> propertyContext;
        IRepository<User> userContext;
        IRepository<Role> roleContext;

        User loggedInUser;
        string[] maintainanceList = { "Electrical", "Heating", "Gas", "Plumbing", "None" };
        Role currentRole;


        public NewProperty(User loggedInUser, Role currentRole)
        {
            this.loggedInUser = loggedInUser;
            this.currentRole = currentRole;
            InitializeComponent();

            this.propertyContext = ContainerHelper.Container.Resolve<IRepository<Property>>();
            this.userContext = ContainerHelper.Container.Resolve<IRepository<User>>();
            this.roleContext = ContainerHelper.Container.Resolve<IRepository<Role>>();

            ComboBoxSetup();
        }

        private void ComboBoxSetup()
        {
            List<User> userList = userContext.Collection().ToList();
            List<Role> roleList = roleContext.Collection().ToList();
            List<string> maintainanceRoles = new List<string>();
            string agentRole = "";

            // find out which role id's represents each role
            foreach (Role r in roleList)
            {
                string[] split = r.Name.Split(' ');
                Console.WriteLine(split[0]);
                if (split[0].Equals("Maintainance"))
                {
                    maintainanceRoles.Add(r.Id);
                }
                else if (r.Name.Equals("Letting Agent"))
                {
                    agentRole = r.Id;
                }
            }

            // sort users into maintainance staff and letting agents
            List<User> maintainanceStaff = new List<User>();
            List<User> lettingAgents = new List<User>();

            foreach (User user in userList)
            {
                if (maintainanceRoles.Contains(user.RoleId))
                {
                    maintainanceStaff.Add(user);
                }
                else if (user.RoleId.Equals(agentRole))
                {
                    lettingAgents.Add(user);
                }
            }

            // adds null option to cmbbox incase not required
            User nullUser = new User(null, null, null, null, null);
            nullUser.Id = "";
            maintainanceStaff.Add(nullUser);
            lettingAgents.Add(nullUser);

            // set combo box values for maintainance selection
            cmbMaintainanceStaff.ItemsSource = maintainanceStaff;
            cmbMaintainanceStaff.SelectedValuePath = "Id";

            // set combo box values for letting agent selection
            cmbLettingAgent.ItemsSource = lettingAgents;
            cmbLettingAgent.SelectedValuePath = "Id";

            cmbRequiredMaintainance.ItemsSource = maintainanceList;

            string[] propertyTypes = { "Detached", "Semi-detached", "Terraced", "Flat" };
            cmbPropertyType.ItemsSource = propertyTypes;
        }

        public async void SaveRecord(object sender, RoutedEventArgs e)
        {

            // Regex for postcode
            Regex r = new Regex("([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\\s?[0-9][A-Za-z]{2})");
            if (txtPostCode.Text != null && !r.IsMatch(txtPostCode.Text))
            {
                MessageBox.Show("Post Code is not Formatted Correctly", "Please Try Again", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // TODO save record
            if (txtAddressLine1.Text.Equals("") || txtPostCode.Text.Equals("") || bedrooms.Text.Equals("") || bathrooms.Text.Equals("") || cmbLettingAgent.SelectedItem == null || cmbMaintainanceStaff.SelectedItem == null || cmbRequiredMaintainance == null || cmbPropertyType == null)
            {
                MessageBox.Show("Please enter Address Line 1, Post Code Number of Bedrooms & Bathrooms. Also Select a Letting Agent, Maintainance Staff, Required Maintainance and the Property Type");
            }
            else
            {
                Property property = new Property((bool) chkAvailable.IsChecked, txtAddressLine1.Text, txtAddressLine2.Text, txtPostCode.Text, monthlyRent.Text, cmbRequiredMaintainance.SelectedValue.ToString(), (DateTime) dtpQuarterly.Value, (DateTime) dtpAnnualGasInspection.Value, (DateTime) dtpFiveYearElectricalInspection.Value, cmbMaintainanceStaff.SelectedValue.ToString(), cmbLettingAgent.SelectedValue.ToString(), (int)bedrooms.Value, (int)bathrooms.Value, cmbPropertyType.SelectedItem.ToString());
                propertyContext.Insert(property);
                await propertyContext.Commit();
                MessageBox.Show("Record Created!", "Creation Successful!");
                PropertyManagement pm = new PropertyManagement(loggedInUser, currentRole);
                this.Hide();
                pm.Show();
            }
        }

        private bool Validate()
        {
            return true;
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
