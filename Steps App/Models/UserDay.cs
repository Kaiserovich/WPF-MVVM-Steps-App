using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steps_App.Models
{
    [Serializable]
    internal class UserDay
    {
        public string User { get; }
        public int Rank { get; }
        public string Status { get; }
        public int Steps { get; }

        public int Day { get; private set; }
        public void SetDay(int day)
        { 
            Day = day; 
        }

        public UserDay(string user, int steps, int rank, string status)
        {
            User = user;
            Steps = steps;
            Rank = rank;
            Status = status;
        }
    }
}
