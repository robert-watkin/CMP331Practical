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
using Unity;

namespace CMP331Practical.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Dashboard : Window
    {

        IRepository<Role> roleContext;
        IRepository<Property> propertyContext;
        IRepository<Invoice> invoiceContext;
        IRepository<User> userContext;
        private User loggedInUser;
        Role currentRole = null;

        public List<Property> assignedProperties;
        public List<Notification> notifications;

        public Dashboard(User loggedInUser)
        {

            this.roleContext = ContainerHelper.Container.Resolve<IRepository<Role>>();
            this.propertyContext = ContainerHelper.Container.Resolve<IRepository<Property>>();
            this.userContext = ContainerHelper.Container.Resolve<IRepository<User>>();
            this.invoiceContext = ContainerHelper.Container.Resolve<IRepository<Invoice>>();


            InitializeComponent();
            this.loggedInUser = loggedInUser;
            

            // set name and role on dashboard
            txtUserName.Content = "Name: " + this.loggedInUser.Firstname + " " + this.loggedInUser.Lastname;

            // get the users current role name
            List<Role> roleList = roleContext.Collection().ToList();
            foreach (Role r in roleList)
            {
                if (r.Id == loggedInUser.RoleId)
                {
                    currentRole = r;
                }
            }

            txtUserRole.Content = "Role: " + currentRole.Name;

            
            LoadAssignedProperties();
            LoadNotifications();
            SetButtonEnabled();
        }

        private void SetButtonEnabled()
        {
            if (currentRole.Name.Contains("Maintainance"))
            {
                btnAvailableProperties.IsEnabled = false;
                btnPropertyManagement.IsEnabled = false;
                btnUserManagement.IsEnabled = false;
            }
            else if (currentRole.Name == "Letting Agent")
            {
                btnUserManagement.IsEnabled = false;
            }
        }

        private void LoadAssignedProperties()
        {
            assignedProperties = new List<Property>();
            List<Property> allProperties = propertyContext.Collection().ToList();

            if (currentRole.Name == "System Admin") // all properties displayed for system admin
            {
                assignedProperties = allProperties;
            }
            else if (currentRole.Name == "Letting Agent") // only properties assigned to letting agents displayed to the letting agent
            {
                Console.WriteLine("Letting Agent");
                foreach (Property p in allProperties)
                {
                    Console.WriteLine(p.LettingAgentId + " == " + loggedInUser.Id);
                    if (p.LettingAgentId == loggedInUser.Id)
                    {
                        Console.WriteLine("True\n");
                        assignedProperties.Add(p);
                    }
                }
            }
            else // only properties assigned to maintainance staff displayed to maintainance staff
            {
                foreach (Property p in allProperties)
                {
                    if (p.MaintainanceStaffId == loggedInUser.Id)
                    {
                        assignedProperties.Add(p);
                    }
                }
            }


            AssignedProperties.ItemsSource = assignedProperties;
        }

        private void SignOut(object sender, RoutedEventArgs e)
        {
            MainWindow l = new MainWindow();
            l.Show();
            this.Close();
        }

        private void LoadNotifications()
        {
            Console.WriteLine("Loading Notifications...");
            notifications = new List<Notification>();
            List<User> allUsers = userContext.Collection().ToList();
            List<Role> roleList = roleContext.Collection().ToList();
            List<Invoice> invoiceList = invoiceContext.Collection().ToList();

            // loop through each property
            foreach (Property property in assignedProperties)
            {
                Console.WriteLine(property.AddressLine1 + " notifications:");

                if (!currentRole.Name.Contains("Maintainance")){
                    // maintainance required but no or incorrect maintainance staff
                    if (property.RequiredMaintainance != "None")
                    {

                        // no staff assigned
                        if(property.MaintainanceStaffId == "")
                        {
                            Notification n = new Notification(property.AddressLine1, property.RequiredMaintainance + " Maintainance Required but No Maintainance Staff Assigned");
                            notifications.Add(n);
                        }
                        else // incorrect staff assigned
                        {
                            foreach (User user in allUsers)
                            {
                                if (user.Id == property.MaintainanceStaffId)
                                {
                                    foreach (Role role in roleList)
                                    {
                                        if (role.Id == user.RoleId)
                                        {
                                            string[] split = role.Name.Split(' ');
                                            Console.WriteLine(split[2] + " == " + property.RequiredMaintainance);
                                            if (!split[2].Equals(property.RequiredMaintainance))
                                            {
                                                Notification n = new Notification(property.AddressLine1, property.RequiredMaintainance + " Maintainance Required but Incorrect Maintainance Staff Assigned");
                                                notifications.Add(n);
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }


                // TODO outstanding inspections
                if (property.QuarterlyInspection.Date < DateTime.Today)
                {
                    Notification n = new Notification(property.AddressLine1, "Quarterly Inspection Overdue (" + property.QuarterlyInspection.Date + ")");
                    notifications.Add(n);
                }
                if (property.AnnualGasInspection.Date < DateTime.Today)
                {
                    Notification n = new Notification(property.AddressLine1, "Annual Gas Inspection Overdue (" + property.AnnualGasInspection.Date + ")");
                    notifications.Add(n);
                }
                if (property.FiveYearElectricalInspection.Date < DateTime.Today)
                {
                    Notification n = new Notification(property.AddressLine1, "Five Year Electrical Inspection Overdue (" + property.FiveYearElectricalInspection.Date + ")");
                    notifications.Add(n);
                }

                if (!currentRole.Name.Contains("Maintainance")) {
                    // TODO outstanding payments - requires invoices
                    foreach (Invoice invoice in invoiceList)
                    {
                        if (invoice.PropertyId == property.Id)
                        {
                            if (invoice.DueDate <= DateTime.Now && invoice.IsPaid == false)
                            {
                                Notification n = new Notification(property.AddressLine1, "An Invoice Payment is Overdue (" + invoice.DueDate.ToString() + ")");
                                notifications.Add(n);
                            }
                        }
                    }
                }

            }

            Console.WriteLine("Notifications Loaded");
            Notifications.ItemsSource = notifications;
        }

        private void PropertyManagement(object sender, RoutedEventArgs e)
        {
            // open property managmeent window
            PropertyManagement pm = new PropertyManagement(loggedInUser, currentRole);
            pm.Show();
            this.Close();
        }

        private void AvailableProperties(object sender, RoutedEventArgs e)
        {
            // available properties
            AvailableProperties ap = new AvailableProperties(loggedInUser);
            ap.Show();
            this.Close();
        }

        private void ViewReporting(object sender, RoutedEventArgs e)
        {
            if (currentRole.Name.Contains("Maintainance"))
            {
                MessageBox.Show("Maintainance Staff are Not Permitted to View the Reporting Screen");
                return;
            }

                Button button = (Button)sender;
            ReportingScreen rs = new ReportingScreen(loggedInUser, (string) button.Tag);
            rs.Show();
            this.Close();
        }

        private void UserManagement(object sender, RoutedEventArgs e)
        {
            // open user management window
            UserManagement um = new UserManagement(loggedInUser);
            um.Show();
            this.Close();
        }
    }
}
