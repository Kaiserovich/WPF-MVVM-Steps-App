using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Steps_App.Models
{

    [Serializable]
    public class User : INotifyPropertyChanged
    {
        public string Name { get; }


        [XmlIgnore]
        public List<DayInfo> Steps { get; }

        public int MinSteps { get; set; }
        public int MaxSteps { get; set; }

        public int AverageSteps { get; set; }
        [XmlIgnore]
        public bool DifferentOfAverageSelectedUser { get; set; }


        public User(string name)
        {
            Name = name;
            Steps = new List<DayInfo>();

        }
        public User()
        {
            Steps = new List<DayInfo>();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        
    }
}
