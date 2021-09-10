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
using CMP331Practical.Data;
using CMP331Practical.Contracts;
using Unity;

namespace CMP331Practical.Views
{
    /// <summary>
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : Window
    {
        IRepository<User> userContext;
        IRepository<Role> roleContext;
        private User loggedInUser;

        List<User> usersList;
        User selectedUser;
        int userListSize = 0;
        int position = 0;

        public UserManagement(User loggedInUser)
        {
            
            this.loggedInUser = loggedInUser;

            // resolve data contexts
            this.userContext = ContainerHelper.Container.Resolve<IRepository<User>>();
            this.roleContext = ContainerHelper.Container.Resolve<IRepository<Role>>();

            // Initialises WPF components
            InitializeComponent();

            // Calls data refresh
            RefreshData();

        }

        private void RefreshData()
        {
            List<Role> roleList = roleContext.Collection().ToList();
            cmbRole.ItemsSource = roleList;
            cmbRole.DisplayMemberPath = "Name";
            cmbRole.SelectedValuePath = "Id";

            usersList = userContext.Collection().ToList();
            userListSize = usersList.Count();

            selectedUser = usersList.FirstOrDefault();
            position = usersList.IndexOf(selectedUser);

            if (selectedUser == null)
            {
                lblId.Content = "";
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtEmail.Text = "";
                cmbRole.SelectedValue = null;
                txtPassword.Text = "";
                return;
            }

            lblId.Content = selectedUser.Id;
            SetValues();
        }

        private void FirstRecord(object sender, RoutedEventArgs e)
        {
            selectedUser = usersList.FirstOrDefault();
            position = usersList.IndexOf(selectedUser);
            SetValues();
        }

        private void PreviousRecord(object sender, RoutedEventArgs e)
        {
            if (position != 0)
            {
                selectedUser = usersList[position-1];
                position = usersList.IndexOf(selectedUser);
                SetValues();
            }
        }

        private void NextRecord(object sender, RoutedEventArgs e)
        {
            if (position != userListSize - 1)
            {
                position++;
                selectedUser = usersList[position];
                SetValues();
            }
        }

        private void LastRecord(object sender, RoutedEventArgs e)
        {
            if (position != userListSize - 1)
            {
                position = userListSize - 1;
                selectedUser = usersList[position];
                SetValues();
            }
        }

        private async void SaveRecord(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Text.Length < 8)
            {
                MessageBox.Show("Password Must be at Least 8 Characters Long", null, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Email Address is not Valid", null, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            selectedUser.Firstname = txtFirstName.Text;
            selectedUser.Lastname = txtLastName.Text;
            selectedUser.Email = txtEmail.Text;
            selectedUser.RoleId = cmbRole.SelectedValue.ToString();
            if (txtPassword.Text != null && txtPassword.Text != "")
            {
                selectedUser.Password = MD5Password.getMd5Hash(txtPassword.Text);
            }
            await userContext.Commit();
            MessageBox.Show("Record Saved!", "Save Successful!");
        }

        private void Dashboard(object sender, RoutedEventArgs e)
        {
            Dashboard d = new Dashboard(loggedInUser);
            d.Show();
            this.Close();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            this.Hide();
            NewUser nu = new NewUser(loggedInUser);
            nu.Show();
        }

        private void SetValues()
        { 
            // set values on screen based on the selected user
            lblId.Content = selectedUser.Id;
            txtFirstName.Text = selectedUser.Firstname;
            txtLastName.Text = selectedUser.Lastname;
            txtEmail.Text = selectedUser.Email;
            cmbRole.SelectedValue = selectedUser.RoleId;
            txtPassword.Text = "";
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private async void Delete(object sender, RoutedEventArgs e)
        {
            if (selectedUser.Id == loggedInUser.Id)
            {
                MessageBox.Show("You Cannot Delete the Account You're Logged In With",null, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                userContext.Delete(selectedUser.Id);
                await userContext.Commit();
                MessageBox.Show("Record deleted!", "Delete Successful!");
                RefreshData();
            }
        }
    }
}
