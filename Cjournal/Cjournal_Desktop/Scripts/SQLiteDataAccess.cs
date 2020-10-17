using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Data;
using System.Data.SQLite;
using Cjournal_Desktop.Scripts.Interfaces;
using System.Configuration;
using Dapper;
using System.Windows;
using System.Linq;
using System.Net.Http.Headers;
using System.Windows.Media.Animation;
using Cjournal_Desktop.Models;

namespace Cjournal_Desktop.Scripts
{
    // handels all data access with the local SQLite database using Dapper 
    //  methods are self explanatory
    public class SQLiteDataAccess : IDataAccess
    {
        // get a new connection to the database
        private IDbConnection getDBConnection()
        {
            try
            {
                 return new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
            }
            catch (Exception exe)
            {
                return null;
            }
        }
        // execute querys on the database
        private bool Execute(string query, object paramsObj)
        {
            using (IDbConnection conn = this.getDBConnection())
            {
                int affectedRows = conn.Execute(query, paramsObj);
                return affectedRows > 0;
            }
        }
        // retrieve data from the database
        private async Task<List<T>> Query<T>(string query, object paramsObj = null)
        {
            using(IDbConnection conn = this.getDBConnection())
            {
                IEnumerable<T> result = await conn.QueryAsync<T>(query, paramsObj);
                return result.ToList();
            }
        }

        public bool createExercise(ExerciseModel exercise)
        {
            string query = "INSERT INTO exercises (name) VALUES (@name)";
            object paramObj = new { name = exercise.name };

            return this.Execute(query, paramObj);
        }

        public bool createJournalEntry(EntryModel entry)
        {
            string query = "INSERT INTO entries (uid, date, resting_heart_rate, exercise, speed, resistance, time_in_THR_zone) VALUES (@uid, @date, @rhr, @exercise, @speed, @resistance, @timeInTHRZone)";
            object paramObj = new { 
                uid = entry.uid, 
                date = entry.date.ToString(), 
                rhr = entry.resting_heart_rate, 
                exercise = entry.exercise,
                speed = entry.speed,
                resistance = entry.resistance,
                timeInTHRZone = entry.time_in_THR_zone
            };

            return this.Execute(query, paramObj);
        }

        public bool createUser(UserModel user)
        {
            string query = "INSERT INTO users (name, birthday) VALUES (@username, @birthday)";
            object paramObj = new { username = user.name, birthday = user.birthday.ToString() };

            return this.Execute(query, paramObj);
        }



        public List<EntryModel> getAllJournalEntries()
        {
            string query = "SELECT * FROM entries";

            return this.Query<EntryModel>(query).Result;
        }

        public ExerciseModel getExercise(int id)
        {
            string query = "SELECT * FROM exercises WHERE id=@id";
            object paramObj = new { id = id };

            return this.Query<ExerciseModel>(query, paramObj).Result[0];
        }

        public List<ExerciseModel> getExercises()
        {
            string query = "SELECT * FROM exercises";

            return this.Query<ExerciseModel>(query).Result;
        }

        public List<EntryModel> getJournalEntriesForUser(int uid)
        {
            string query = "SELECT * FROM entries WHERE uid=@uid";
            object paramObj = new { uid = uid };

            return this.Query<EntryModel>(query, paramObj).Result;
        }

        public EntryModel getJournalEntry(int id)
        {
            string query = "SELECT * FROM entries WHERE id=@id";
            object paramObj = new { id = id };

            return this.Query<EntryModel>(query, paramObj).Result[0];
        }

        public UserModel getUser(int id)
        {
            string query = "SELECT * FROM users WHERE id = @uid";
            object paramObj = new { uid = id };

            return this.Query<UserModel>(query, paramObj).Result[0];
        }

        public List<UserModel> getUsers()
        {
            string query = "SELECT * FROM users";
            return this.Query<UserModel>(query).Result;
        }

        public bool deleteJournalEntry(int id)
        {
            string query = "DELETE FROM entries WHERE id=@id";
            object paramObj = new { id = id };

            return this.Execute(query, paramObj);
        }
    }
}
