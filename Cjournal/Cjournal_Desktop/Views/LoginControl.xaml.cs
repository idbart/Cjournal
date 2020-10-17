using System;
using System.Collections.Generic;
using System.Text;
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
using Cjournal_Desktop.Scripts;
using Cjournal_Desktop.Scripts.Interfaces;
using Cjournal_Desktop.Scripts.Types;
using Cjournal_Desktop.Views;

namespace Cjournal_Desktop.Views
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        // events
        public event LoginHandler OnLogin;
        public event Action OnCreateNewUser;

        private readonly IDataAccess dataAccess = new SQLiteDataAccess();

        public LoginControl()
        {
            InitializeComponent();

            // gets a list of users and sets the list view to display them
            usersList.ItemsSource = dataAccess.getUsers();
            usersList.SelectionChanged += UsersList_SelectionChanged;
        }

        private void UsersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // invoke the OnLogin event when the user selects a user on the list
            OnLogin?.Invoke(this, (UserModel)e.AddedItems[0]);
        }

        private void newUserButton_Click(object sender, RoutedEventArgs e)
        {
            // fire the OnCreateNewUser event when the user clicks the button
            OnCreateNewUser?.Invoke();
        }
    }
}
