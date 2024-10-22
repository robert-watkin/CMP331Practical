﻿using System;
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
using System.Security.Cryptography;
using Unity;

namespace CMP331Practical.Views
{
    /// <summary>
    /// Interaction logic for NewUser.xaml
    /// </summary>
    public partial class NewUser : Window
    {
        // variable declaration
        IRepository<User> userContext;
        IRepository<Role> roleContext;
        private User loggedInUser;

        // constructor
        public NewUser(User loggedInUser)
        {
            // load data
            this.loggedInUser = loggedInUser;
            this.userContext = ContainerHelper.Container.Resolve<IRepository<User>>();
            this.roleContext = ContainerHelper.Container.Resolve<IRepository<Role>>();

            // initialise WPF components
            InitializeComponent();

            // get list of roles
            List<Role> roleList = roleContext.Collection().ToList();

            // set role selection options
            cmbRole.ItemsSource = roleList;
            cmbRole.DisplayMemberPath = "Name";
            cmbRole.SelectedValuePath = "Id";
            cmbRole.SelectedItem = null;

        }

        private async void SaveRecord(object sender, RoutedEventArgs e)
        {
            // validation
            if (txtPassword.Password.Length < 8)
            {
                MessageBox.Show("Password Must be at Least 8 Characters Long", null, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Email Address is not Valid", null, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // check for null values
            if (txtFirstName.Text.Equals("") || txtLastName.Text.Equals("") || txtEmail.Text.Equals("") || txtPassword.Password.Equals("") || cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Please enter First Name, Last Name, Email, Password and Select Role");
            }
            else
            {
                // create new user and save
                User user = new User(txtFirstName.Text, txtLastName.Text, txtEmail.Text, MD5Password.getMd5Hash(txtPassword.Password), cmbRole.SelectedValue.ToString());
                userContext.Insert(user);
                await userContext.Commit();
                MessageBox.Show("Record Created!", "Creation Successful!");
                UserManagement um = new UserManagement(loggedInUser);
                this.Hide();
                um.Show();
            }
        }

        private bool IsValidEmail(string email)
        {
            // validate the email
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

        private void Dashboard(object sender, RoutedEventArgs e)
        {
            // open and display the dashboard
            Dashboard d = new Dashboard(loggedInUser);
            d.Show();
            this.Close();
        }
    }
}
