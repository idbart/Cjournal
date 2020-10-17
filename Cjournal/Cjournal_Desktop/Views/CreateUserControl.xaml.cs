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

namespace Cjournal_Desktop.Views
{
    /// <summary>
    /// Interaction logic for CreateUserControl.xaml
    /// </summary>
    public partial class CreateUserControl : UserControl
    {
        // events
        public event Action OnUserCreated;
        public event Action OnCancel;

        private readonly IDataAccess dataAccess = new SQLiteDataAccess();

        public CreateUserControl()
        {
            InitializeComponent();
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            // try to create a new user when the user clicks the button

            // validate input data
            if(string.IsNullOrEmpty(usernameInput.Text) || dateInput.SelectedDate == null)
            {
                headerText.Content = "Please input a name and date!";
                return;
            }
            else
            {
                // try to create a new user
                //  if it works: fire the OnUserCreated event
                //  if not: change the header textblock
                UserModel newUser = new UserModel() { name = usernameInput.Text, birthday = (DateTime)dateInput.SelectedDate };
                if (dataAccess.createUser(newUser))
                {
                    OnUserCreated?.Invoke();
                }
                else
                {
                    headerText.Content = "ERROR: could not create user";
                }
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            // fire the OnCancel event when the user clicks the cancel button
            OnCancel?.Invoke();
        }
    }
}
