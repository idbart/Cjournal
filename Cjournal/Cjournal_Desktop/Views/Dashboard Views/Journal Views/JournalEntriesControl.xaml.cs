using Cjournal_Desktop.Models;
using Cjournal_Desktop.Scripts;
using Cjournal_Desktop.Scripts.Interfaces;
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

namespace Cjournal_Desktop.Views.Dashboard_Views.Journal_Views
{
    /// <summary>
    /// Interaction logic for JournalEntriesControl.xaml
    /// </summary>
    public partial class JournalEntriesControl : UserControl
    {
        private readonly IDataAccess dataAccess = new SQLiteDataAccess();
        private int userID;

        public JournalEntriesControl(int user)
        {
            InitializeComponent();

            this.userID = user;
            this.refresh();
        }

        // refresh the list of entries
        private void refresh()
        {
            dataView.ItemsSource = dataAccess.getJournalEntriesForUser(userID);
        }

        private void dialogDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // delete the selected entry and refresh the list of entries
            EntryModel selectedEntry = (EntryModel)dataView.SelectedItem;
            if(dataAccess.deleteJournalEntry(selectedEntry.id))
            {
                refresh();
                dialogHost.IsOpen = false;
            }
        }

        private void dialogCancelButton_Click(object sender, RoutedEventArgs e)
        {
            dialogHost.IsOpen = false;
        }

        private void dataView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dialogHost.IsOpen = true;
        }
    }
}
