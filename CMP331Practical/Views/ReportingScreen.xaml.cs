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
using CMP331Practical.Data;
using CMP331Practical.Contracts;
using Unity;

namespace CMP331Practical.Views
{
    /// <summary>
    /// Interaction logic for ReportingScreen.xaml
    /// </summary>
    public partial class ReportingScreen : Window
    {
        IRepository<Property> propertyContext;
        IRepository<Invoice> invoiceContext;
        IRepository<Role> roleContext;
        IRepository<User> userContext;

        User loggedInUser;
        string selectedPropertyId;

        public ReportingScreen(User loggedInUser, string selectedPropertyId)
        {
            this.loggedInUser = loggedInUser;
            this.selectedPropertyId = selectedPropertyId;

            Console.WriteLine("Property ID: " + selectedPropertyId);

            this.propertyContext = ContainerHelper.Container.Resolve<IRepository<Property>>();
            this.roleContext = ContainerHelper.Container.Resolve<IRepository<Role>>();
            this.userContext = ContainerHelper.Container.Resolve<IRepository<User>>();
            this.invoiceContext = ContainerHelper.Container.Resolve<IRepository<Invoice>>();

            
            InitializeComponent();
            SetCurrentPropertyInfo();
            RefreshInvoiceData();
        }

        private void RefreshInvoiceData()
        {
            List<Invoice> invoiceList = invoiceContext.Collection().ToList();
            Invoices.ItemsSource = invoiceList;
        }

        private void SetCurrentPropertyInfo()
        {
            List<Property> propertyList = propertyContext.Collection().ToList();

            foreach (Property property in propertyList)
            {
                if (property.Id.Equals(selectedPropertyId))
                {
                    // set property info
                    lblAddress.Content = property.AddressLine1 + "\n" + (property.AddressLine2 != "" ? property.AddressLine2 + "\n" : "") + property.PostCode;
                    lblMaintainance.Content = "Required: " + property.RequiredMaintainance + "\n" +
                        "Quarterly Inspection" + property.QuarterlyInspection.ToString() + "\n" +
                        "Annual Gas Inspection" + property.AnnualGasInspection.ToString() + "\n" +
                        "Five Year Electrical Inspection" + property.FiveYearElectricalInspection.ToString();

                    List<Role> roleList = roleContext.Collection().ToList();
                    List<User> userList = userContext.Collection().ToList();
                    

                    User agent;
                    User maintainanceStaff = null;
                    string maintainanceRole = "";

                    // get the correct user information for the agent and maintainance staff
                    foreach (User user in userList)
                    {
                        if (user.Id == property.LettingAgentId) 
                        {
                            agent = user;
                        }
                        else if(user.Id == property.MaintainanceStaffId)
                        {
                            maintainanceStaff = user;
                        }
                    }

                    // get the role name of the maintainance staff
                    foreach (Role role in roleList)
                    {
                        if (role.Id == maintainanceStaff.RoleId)
                        {
                            maintainanceRole = role.Name;
                            break;
                        }
                    }

                    // display content
                    lblLettingAgent.Content = (property.LettingAgentId != "" ? loggedInUser.Firstname + " " + loggedInUser.Lastname : "None");
                    lblMaintainanceStaff.Content = (property.MaintainanceStaffId != "" ? maintainanceStaff.Firstname + " " + maintainanceStaff.Lastname + ": " + maintainanceRole : "None");
                    break;
                }
            }
        }

        private void NewInvoice(object sender, RoutedEventArgs e)
        {
            NewInvoice ni = new NewInvoice(loggedInUser, selectedPropertyId);
            ni.Show();
            this.Close();
        }

        private async void Delete(object sender, RoutedEventArgs e)
        {
            string invoiceId = ((Button)sender).Tag.ToString();

            if (MessageBox.Show("Are you sure you want to delete this invoice?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                invoiceContext.Delete(invoiceId);
                await invoiceContext.Commit();
                MessageBox.Show("Invoice deleted!", "Delete Successful!");
            }
            RefreshInvoiceData();
        }

        private async void MarkAsPaid(object sender, RoutedEventArgs e)
        {
            string invoiceId = ((Button)sender).Tag.ToString();
            List<Invoice> invoiceList = invoiceContext.Collection().ToList();

            foreach (Invoice invoice in invoiceList)
            {
                if (invoice.Id == invoiceId)
                {
                    invoice.IsPaid = true;
                    await invoiceContext.Commit();
                    RefreshInvoiceData();
                }
            }
        }

        private void Dashboard(object sender, RoutedEventArgs e)
        {
            Dashboard d = new Dashboard(loggedInUser);
            d.Show();
            this.Close();
        }
    }
}
