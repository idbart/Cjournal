using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Cjournal_Desktop.Scripts;
using Cjournal_Desktop.Scripts.Interfaces;

namespace Cjournal_Desktop.Models
{
    // corresponds to an entry in the "entries" table 
    public class EntryModel
    {
        public int id { get; set; }
        public int uid { get; set; }
        public DateTime date { get; set; }
        public int resting_heart_rate { get; set; }
        public int exercise { get; set; }
        public string exerciseName 
        { 
            get
            {
                return this.getExerciseName();
            }
        }
        public int speed { get; set; }
        public int resistance { get; set; }
        public int time_in_THR_zone { get; set; }

        private string getExerciseName()
        {
            IDataAccess dataAccess = new SQLiteDataAccess();
            return dataAccess.getExercise(this.exercise).name;
        }
    }
}
