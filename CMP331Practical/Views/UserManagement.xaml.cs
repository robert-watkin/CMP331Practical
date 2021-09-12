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
        // variable declaration
        IRepository<User> userContext;
        IRepository<Role> roleContext;
        private User loggedInUser;

        List<User> usersList;
        User selectedUser;
        int userListSize = 0;
        int position = 0;

        // constructor
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
            // set role selection options
            List<Role> roleList = roleContext.Collection().ToList();
            cmbRole.ItemsSource = roleList;
            cmbRole.DisplayMemberPath = "Name";
            cmbRole.SelectedValuePath = "Id";

            // get user list and list size
            usersList = userContext.Collection().ToList();
            userListSize = usersList.Count();

            // set selected user
            selectedUser = usersList.FirstOrDefault();
            position = usersList.IndexOf(selectedUser);

            // set values to blank if no uer is selected
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

            // call function
            SetValues();
        }

        private void FirstRecord(object sender, RoutedEventArgs e)
        {
            // select the first user
            selectedUser = usersList.FirstOrDefault();
            position = usersList.IndexOf(selectedUser);
            SetValues();
        }

        private void PreviousRecord(object sender, RoutedEventArgs e)
        {
            // if not already at the first record
            if (position != 0)
            {
                // select the previous record
                selectedUser = usersList[position-1];
                position = usersList.IndexOf(selectedUser);
                SetValues();
            }
        }

        private void NextRecord(object sender, RoutedEventArgs e)
        {
            // if not at the last record
            if (position != userListSize - 1)
            {
                // navigate to the next record
                position++;
                selectedUser = usersList[position];
                SetValues();
            }
        }

        private void LastRecord(object sender, RoutedEventArgs e)
        {
            // if not at the last record
            if (position != userListSize - 1)
            {
                // navigate to the last record
                position = userListSize - 1;
                selectedUser = usersList[position];
                SetValues();
            }
        }

        private async void SaveRecord(object sender, RoutedEventArgs e)
        {
            // validation
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

            // set selectedUser values
            selectedUser.Firstname = txtFirstName.Text;
            selectedUser.Lastname = txtLastName.Text;
            selectedUser.Email = txtEmail.Text;
            selectedUser.RoleId = cmbRole.SelectedValue.ToString();
            if (txtPassword.Text != null && txtPassword.Text != "")
            {
                selectedUser.Password = MD5Password.getMd5Hash(txtPassword.Text);
            }
            await userContext.Commit(); // save
            MessageBox.Show("Record Saved!", "Save Successful!");
        }

        private void Dashboard(object sender, RoutedEventArgs e)
        {
            // open and display dashboard
            Dashboard d = new Dashboard(loggedInUser);
            d.Show();
            this.Close();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            // open and display new user screen
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
            // validate email
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
            // check the account is not the logged in account
            if (selectedUser.Id == loggedInUser.Id)
            {
                MessageBox.Show("You Cannot Delete the Account You're Logged In With",null, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // confirm delete
            if (MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                // delete
                userContext.Delete(selectedUser.Id);
                await userContext.Commit();
                MessageBox.Show("Record deleted!", "Delete Successful!");
                RefreshData();
            }
        }
    }
}
