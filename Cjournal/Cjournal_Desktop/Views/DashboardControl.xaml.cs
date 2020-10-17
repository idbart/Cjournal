using Cjournal_Desktop.Models;
using Cjournal_Desktop.Views.Dashboard_Views;
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

namespace Cjournal_Desktop.Views
{
    /// <summary>
    /// Interaction logic for DashboardControl.xaml
    /// </summary>
    public partial class DashboardControl : UserControl
    {
        // events
        public event Action OnLogout;

        // props
        private UserModel user;

        public DashboardControl(UserModel userData)
        {
            user = userData;
            InitializeComponent();


            usernameText.Text = user.name;
            displayJournal();
        }

        // methods for setting the main content
        private void displayJournal()
        {
            journalButton.IsEnabled = false;
            statsButton.IsEnabled = true;

            JournalControl control = new JournalControl(user);
            dashMainContent.Content = control;
        }
        private void displayStats()
        {
            statsButton.IsEnabled = false;
            journalButton.IsEnabled = true;

            StatsControl control = new StatsControl();
            dashMainContent.Content = control;
        }

        // fire the OnLogout event when the user click the logout button
        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.OnLogout?.Invoke();
        }

        private void journalButton_Click(object sender, RoutedEventArgs e)
        {
            displayJournal();
        }

        private void statsButton_Click(object sender, RoutedEventArgs e)
        {
            displayStats();
        }
    }
}
