using Cjournal_Desktop.Models;
using Cjournal_Desktop.Scripts;
using Cjournal_Desktop.Scripts.Interfaces;
using Cjournal_Desktop.Views.Dashboard_Views.Journal_Views;
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

namespace Cjournal_Desktop.Views.Dashboard_Views
{
    /// <summary>
    /// Interaction logic for JournalControl.xaml
    /// </summary>
    public partial class JournalControl : UserControl
    {
        private readonly IDataAccess dataAccess = new SQLiteDataAccess();
        private UserModel user;  

        public JournalControl(UserModel user)
        {
            this.user = user;
            InitializeComponent();

            displayEntries();
        }

        // methods to change the main content control
        private void displayEntries()
        {
            // get the user by thier id and then pass them to the new JournalEntriesControl
            JournalEntriesControl control = new JournalEntriesControl(user.id);

            // set the text and function of the bottom button
            bottomButton.Content = "Add New Entry";
            RoutedEventHandler handler = null;
            handler = (sender, e) => {
                displayEntry();
                bottomButton.Click -= handler;
            };
            bottomButton.Click += handler;

            mainControl.Content = control;
        }
        private void displayEntry(EntryModel entry = null)
        {
            JournalEntryControl control = new JournalEntryControl(entry);

            // set the text and function of the bottom button
            bottomButton.Content = "Save Entry";
            
            if (entry == null)
            {
                // for if the user is creating a new journal entry
                RoutedEventHandler handler = null;
                handler = (sender, e) => {
                    // create a new entry object
                    EntryModel newEntry = control.getDataModel();
                    // set the user id (not done by the JournalEntryControl beacuse it is never given user data)
                    newEntry.uid = user.id;

                    // if a new entry is sucessfully created: show the JournalEntriesControl
                    if(dataAccess.createJournalEntry(newEntry))
                    {
                        displayEntries();
                        bottomButton.Click -= handler;
                    }
                };

                control.OnCancel += () => {
                    displayEntries();
                    bottomButton.Click -= handler;
                };

                bottomButton.Click += handler;
            }
            else
            {
                // for if the user is editing a journal entry
                bottomButton.Click += (sender, e) => {

                };
            }

            mainControl.Content = control;
        }
    }
}
