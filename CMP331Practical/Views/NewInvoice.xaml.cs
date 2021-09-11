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
    /// Interaction logic for NewInvoice.xaml
    /// </summary>
    public partial class NewInvoice : Window
    {

        IRepository<Property> propertyContext;
        IRepository<Invoice> invoiceContext;

        User loggedInUser;

        public NewInvoice(User loggedInUser, string selectedPropertyId)
        {
            this.loggedInUser = loggedInUser;
            InitializeComponent();

            this.invoiceContext = ContainerHelper.Container.Resolve<IRepository<Invoice>>();
            this.propertyContext = ContainerHelper.Container.Resolve<IRepository<Property>>();
            List<Property> propertyList = propertyContext.Collection().ToList();

            cmbProperty.ItemsSource = propertyList;
            cmbProperty.DisplayMemberPath = "AddressLine1";
            cmbProperty.SelectedValuePath = "Id";

            cmbProperty.SelectedValue = selectedPropertyId;
        }


        private void Dashboard(object sender, RoutedEventArgs e)
        {
            Dashboard d = new Dashboard(loggedInUser);
            d.Show();
            this.Close();
        }

        private async void SaveRecord(object sender, RoutedEventArgs e)
        {
            // TODO save record
            if (cmbProperty.SelectedItem == null || dtpDueDate.Value == null)
            {
                MessageBox.Show("Please select a Property and DueDate");
            }
            else
            {
                // get the property in question
                Property p = (Property) cmbProperty.SelectedItem;
                Invoice invoice = new Invoice(float.Parse(p.MonthlyRent.TrimStart('£')), (bool)chkPaid.IsChecked, (DateTime)dtpDueDate.Value, p.Id);
                invoiceContext.Insert(invoice);
                await invoiceContext.Commit();
                MessageBox.Show("Record Created!", "Creation Successful!");
                ReportingScreen rs = new ReportingScreen(loggedInUser, p.Id);
                this.Hide();
                rs.Show();
            }
        }
    }
}
