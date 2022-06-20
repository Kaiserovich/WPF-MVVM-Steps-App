using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Steps_App.Models
{
    [XmlRoot(ElementName = "DayInfo")]
    public class DayInfo
    {
        public int Day { get; }
        public int Steps { get; }

        public int Rank { get; }
        public string Status { get; }

        public DayInfo (int day, int steps, int rank, string status)
        {
            Day = day;
            Steps = steps;
            Rank = rank;
            Status = status;
        }
        public DayInfo()
        {
            Day = this.Day;
        }
    }
}
