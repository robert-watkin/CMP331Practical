using CMP331Practical.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CMP331Practical.Models;
using CMP331Practical.Contracts;
using CMP331Practical.Views;
using Unity;

namespace CMP331Practical
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IRepository<User> userContext;
        IRepository<Role> roleContext;

        List<Role> roleList;
        string roleSystemAdmin, roleLettingAgent, rolePlumber, roleElectrician, roleHeating, roleGas;


        public MainWindow()
        {
            this.userContext = ContainerHelper.Container.Resolve<IRepository<User>>();
            this.roleContext = ContainerHelper.Container.Resolve<IRepository<Role>>();


            InitializeComponent();
        }

        public void Login(object sender, RoutedEventArgs e)
        {
            List<User> users = userContext.Collection().ToList();
            User loggedInUser;
            bool valid = false;
            foreach (User u in users)
            {
                if (u.Email.Equals(txtEmail.Text.ToLower()) && u.Password.Equals(pwdPassword.Password))
                {
                    loggedInUser = u;

                    Dashboard d = new Dashboard(loggedInUser);
                    d.Show();
                    this.Close();
                    valid = true;
                }
            }
            if (!valid)
                MessageBox.Show("Incorrect Email or Password. Please Try Again.");
        }

        private void SetRoles()
        {
            roleList = roleContext.Collection().ToList();

            foreach (Role r in roleList)
            {
                switch (r.Name)
                {
                    case "System Admin":
                        roleSystemAdmin = r.Id;
                        break;
                    case "Letting Agent":
                        roleLettingAgent = r.Id;
                        break;
                    case "Maintainance - Plumber":
                        rolePlumber = r.Id;
                        break;
                    case "Maintainance - Electrician":
                        roleElectrician = r.Id;
                        break;
                    case "Maintainance - Heating":
                        roleHeating = r.Id;
                        break;
                    case "Maintainance - Gas":
                        roleGas = r.Id;
                        break;
                }
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
