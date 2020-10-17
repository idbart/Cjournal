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
    /// Interaction logic for JournalEntryControl.xaml
    /// </summary>
    public partial class JournalEntryControl : UserControl
    {
        // events
        public event Action OnCancel;

        private readonly IDataAccess dataAccess = new SQLiteDataAccess();

        public JournalEntryControl(EntryModel entry)
        {
            InitializeComponent();

            // set the date picker to the current date by default
            dateSelector.SelectedDate = DateTime.Now;

            // setup the options for the exercises combobox

            refreshExercises();

            if(entry != null)
            {
                setControls(entry);
            }
        }

        // refresh the list of exercises in the input
        private void refreshExercises()
        {
            exerciseSelector.ItemsSource = dataAccess.getExercises();
            exerciseSelector.SelectedIndex = 0;
        }

        // set the values for all the controls with an object
        //  mainly for when this control is being used to edit a journal entry
        private void setControls(EntryModel entry)
        {
            dateSelector.SelectedDate = entry.date;
            rhrInput.setInputValue(entry.resting_heart_rate);
            exerciseSelector.SelectedIndex = entry.exercise - 1; // maybe fix this later
            speedInput.setInputValue(entry.speed);
            resistanceInput.setInputValue(entry.resistance);
            timeInTHRInput.setInputValue(entry.time_in_THR_zone);
        }

        // for the parent class to get all the current user inputed data in one object
        public EntryModel getDataModel()
        {
            EntryModel model = new EntryModel();

            model.date = (DateTime)dateSelector.SelectedDate;
            model.resting_heart_rate = rhrInput.getInputValue();

            ExerciseModel exercise = (ExerciseModel)exerciseSelector.SelectedItem;
            model.exercise = exercise.id;

            model.speed = speedInput.getInputValue();
            model.resistance = resistanceInput.getInputValue();
            model.time_in_THR_zone = timeInTHRInput.getInputValue();

            return model;
        }

        private void closeDialog()
        {
            dialogHost.IsOpen = false;
            newExerciseNameInput.Text = string.Empty;

            refreshExercises();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            OnCancel?.Invoke();
        }

        private void addExerciseButton_Click(object sender, RoutedEventArgs e)
        {
            dialogHost.IsOpen = true;
        }

        private void dialogCancelButton_Click(object sender, RoutedEventArgs e)
        {
            closeDialog();
        }

        private void dialogAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(newExerciseNameInput.Text))
            {
                if(dataAccess.createExercise(new ExerciseModel() { name = newExerciseNameInput.Text }))
                {
                    closeDialog();
                }
            }
        }
    }
}
