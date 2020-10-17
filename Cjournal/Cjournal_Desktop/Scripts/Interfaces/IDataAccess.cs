using System;
using System.Collections.Generic;
using System.Text;
using Cjournal_Desktop.Models;

namespace Cjournal_Desktop.Scripts.Interfaces
{
    public interface IDataAccess
    {
        public List<UserModel> getUsers();
        public UserModel getUser(int id);
        public bool createUser(UserModel user);

        public List<EntryModel> getAllJournalEntries();
        public EntryModel getJournalEntry(int id);
        public List<EntryModel> getJournalEntriesForUser(int uid);
        public bool createJournalEntry(EntryModel entry);
        public bool deleteJournalEntry(int id);

        public List<ExerciseModel> getExercises();
        public ExerciseModel getExercise(int id);
        public bool createExercise(ExerciseModel exercise);
    }
}
