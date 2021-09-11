using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
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
    /// Interaction logic for PropertyManagement.xaml
    /// </summary>
    public partial class PropertyManagement : Window
    {
        IRepository<Property> propertyContext;
        IRepository<User> userContext;
        IRepository<Role> roleContext;

        private User loggedInUser;

        List<Property> propertyList;
        Property selectedProperty;
        int propertyListSize = 0;
        int position = 0;
        string[] maintainanceList = { "Electrical", "Heating", "Gas", "Plumbing", "None" };
        Role currentRole;

        public PropertyManagement(User loggedInUser, Role currentRole)
        {
            this.loggedInUser = loggedInUser;
            this.currentRole = currentRole;

            this.propertyContext = ContainerHelper.Container.Resolve<IRepository<Property>>();
            this.userContext = ContainerHelper.Container.Resolve<IRepository<User>>();
            this.roleContext = ContainerHelper.Container.Resolve<IRepository<Role>>();

            InitializeComponent();

            RefreshData();
            SetButtonEnabled();
        }

        private void SetButtonEnabled()
        {
            if (currentRole.Name == "Letting Agent")
            {
                btnNew.IsEnabled = false;
                btnDelete.IsEnabled = false;
                txtAddressLine1.IsEnabled = false;
                txtAddressLine2.IsEnabled = false;
                txtPostCode.IsEnabled = false;
                cmbLettingAgent.IsEnabled = false;
                chkAvailable.IsEnabled = false;
                monthlyRent.IsEnabled = false;
                bedrooms.IsEnabled = false;
                bathrooms.IsEnabled = false;

            }
        }

        private void RefreshData()
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
                if (split[0].Equals("Maintainance")){
                    maintainanceRoles.Add(r.Id);
                }
                else if(r.Name.Equals("Letting Agent"))
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

            propertyList = propertyContext.Collection().ToList();
            propertyListSize = propertyList.Count();

            selectedProperty = propertyList.FirstOrDefault();
            position = propertyList.IndexOf(selectedProperty);

            cmbRequiredMaintainance.ItemsSource = maintainanceList;

            string[] propertyTypes = { "Detached", "Semi-detached", "Terraced", "Flat" };
            cmbPropertyType.ItemsSource = propertyTypes;

            if (selectedProperty == null){
                lblId.Content = "";
                chkAvailable.IsChecked = false;
                txtAddressLine1.Text = "";
                txtAddressLine2.Text = "";
                txtPostCode.Text = "";
                monthlyRent.Text = "";
                dtpQuarterly.Value = DateTime.Now;
                dtpAnnualGasInspection.Value = DateTime.Now;
                dtpFiveYearElectricalInspection.Value = DateTime.Now;
                cmbLettingAgent.SelectedValue = null;
                cmbMaintainanceStaff.SelectedValue = null;
                cmbRequiredMaintainance.SelectedValue = null;
                bedrooms.Text = "0";
                bathrooms.Text = "0";
                cmbPropertyType = null;

                return; 
            }

            // set values on the page
            SetValues();
        }

        private void Dashboard(object sender, RoutedEventArgs e)
        {
            Dashboard d = new Dashboard(loggedInUser);
            d.Show();
            this.Close();
        }

        private void FirstRecord(object sender, RoutedEventArgs e)
        {
            if (selectedProperty == null) { return; }

            // select the first record
            selectedProperty = propertyList.FirstOrDefault();
            position = propertyList.IndexOf(selectedProperty);
            lblId.Content = selectedProperty.Id;
            SetValues();
        }

        private void PreviousRecord(object sender, RoutedEventArgs e)
        {
            if (selectedProperty == null) { return; }

            // select the previous record
            if (position != 0)
            {
                selectedProperty = propertyList[position-1];
                position = propertyList.IndexOf(selectedProperty);
                SetValues();
            }
        }

        private void NextRecord(object sender, RoutedEventArgs e)
        {
            if (selectedProperty == null) { return; }

            Console.WriteLine("Next Record");
            Console.WriteLine(position);

            // select the next record
            if (position != propertyListSize - 1)
            {
                Console.WriteLine("Executing");
                position++;
                selectedProperty = propertyList[position];
                SetValues();
            }

            Console.WriteLine(position);

        }

        private void LastRecord(object sender, RoutedEventArgs e)
        {
            if (selectedProperty == null) { return; }

            // select the last record
            if (position != propertyListSize - 1)
            {
                position = propertyListSize - 1;
                selectedProperty = propertyList[position];
                SetValues();
            }
        }

        private async void SaveRecord(object sender, RoutedEventArgs e)
        {
            if (selectedProperty == null) { return; }

            // Regex for postcode
            Regex r = new Regex("([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\\s?[0-9][A-Za-z]{2})");
            if (txtPostCode.Text != null && !r.IsMatch(txtPostCode.Text)){
                MessageBox.Show("Post Code is not Formatted Correctly", "Please Try Again", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
                

            // save record
            selectedProperty.Available = (bool) chkAvailable.IsChecked;
            selectedProperty.AddressLine1 = txtAddressLine1.Text;
            selectedProperty.AddressLine2 = txtAddressLine2.Text;
            selectedProperty.PostCode = txtPostCode.Text;
            selectedProperty.MonthlyRent = monthlyRent.Text;
            selectedProperty.QuarterlyInspection = (DateTime) dtpQuarterly.Value;
            selectedProperty.AnnualGasInspection = (DateTime) dtpAnnualGasInspection.Value;
            selectedProperty.FiveYearElectricalInspection = (DateTime) dtpFiveYearElectricalInspection.Value;
            selectedProperty.LettingAgentId = cmbLettingAgent.SelectedValue.ToString();
            selectedProperty.MaintainanceStaffId = cmbMaintainanceStaff.SelectedValue.ToString();
            selectedProperty.RequiredMaintainance = cmbRequiredMaintainance.SelectedValue.ToString();
            selectedProperty.Bathrooms = int.Parse(bathrooms.Text);
            selectedProperty.Bedrooms = int.Parse(bedrooms.Text);
            selectedProperty.Type = cmbPropertyType.SelectedItem.ToString();

            await propertyContext.Commit();
            MessageBox.Show("Record Saved!", "Save Successful!");
        }

        private void SetValues()
        {
            lblId.Content = selectedProperty.Id;
            chkAvailable.IsChecked = selectedProperty.Available;
            txtAddressLine1.Text = selectedProperty.AddressLine1;
            txtAddressLine2.Text = selectedProperty.AddressLine2;
            txtPostCode.Text = selectedProperty.PostCode;
            monthlyRent.Text = selectedProperty.MonthlyRent.ToString();
            dtpQuarterly.Value = selectedProperty.QuarterlyInspection;
            dtpAnnualGasInspection.Value = selectedProperty.AnnualGasInspection;
            dtpFiveYearElectricalInspection.Value = selectedProperty.FiveYearElectricalInspection;
            cmbLettingAgent.SelectedValue = selectedProperty.LettingAgentId;
            cmbMaintainanceStaff.SelectedValue = selectedProperty.MaintainanceStaffId;
            cmbRequiredMaintainance.SelectedValue = selectedProperty.RequiredMaintainance;
            bedrooms.Text = selectedProperty.Bedrooms.ToString();
            bathrooms.Text = selectedProperty.Bathrooms.ToString();
            cmbPropertyType.SelectedItem = selectedProperty.Type;
        }

        private async void Delete(object sender, RoutedEventArgs e)
        {
            if (selectedProperty == null) { return; }

            if (MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                propertyContext.Delete(selectedProperty.Id);
                await propertyContext.Commit();
                MessageBox.Show("Record deleted!", "Delete Successful!");
                RefreshData();
            }
        }

        private void New(object sender, RoutedEventArgs e)
        {
            // new property
            NewProperty np = new NewProperty(loggedInUser, currentRole);
            this.Close();
            np.Show();
        }
    }
}
