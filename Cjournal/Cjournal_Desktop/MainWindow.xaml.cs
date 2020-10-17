using System;
using System.Collections.Generic;
using System.Configuration;
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
using Cjournal_Desktop.Models;
using Cjournal_Desktop.Views;
using System.Runtime.CompilerServices;
using Cjournal_Desktop.Scripts.Interfaces;
using Cjournal_Desktop.Scripts;

namespace Cjournal_Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IDataAccess dataAccess = new SQLiteDataAccess();

        public MainWindow()
        {
            InitializeComponent();

            initMainContent();
        }

        private void initMainContent()
        {
            // display login control if users exist
            //  if not: display create user control

            if (usersExist())
            {
                dispayLogin();
            }
            else
            {
                displayCreateUser();
            }
        }

        private void login(object sender, UserModel userData)
        {
            // display the dashboard control with the users data
            displayDashboard(userData);
        }

        // check if a user exists
        private bool usersExist()
        {
            return dataAccess.getUsers().Count > 0;
        }


        // methods for changing the main content control
        private void dispayLogin()
        {
            LoginControl control = new LoginControl();
            control.OnLogin += login;
            control.OnCreateNewUser += displayCreateUser;

            mainContent.Content = control;
        }
        private void displayCreateUser()
        {
            CreateUserControl control = new CreateUserControl();
            control.OnUserCreated += dispayLogin;
            control.OnCancel += dispayLogin;

            mainContent.Content = control;
        }
        private void displayDashboard(UserModel userData)
        {
            DashboardControl control = new DashboardControl(userData);
            control.OnLogout += dispayLogin;

            mainContent.Content = control;
        }


        // handle minimize window button click
        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        // handle close window button click
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
