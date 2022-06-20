using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using CsvHelper;
using System.Windows.Markup;

namespace Steps_App.Models
{
    internal class UserManager
    {
        public void WriteXML(User user)
        {
            using (FileStream fs = new FileStream($"{user.Name}.xml", FileMode.Create, FileAccess.Write))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(User));
                formatter.Serialize(fs, user);

            }

        }
        public void WriteCvs(User user)
        {
            TextWriter textWriter = File.CreateText($"{user.Name}.tsv");
            var csvWriter = new CsvWriter(textWriter, System.Globalization.CultureInfo.CurrentCulture);

            csvWriter.WriteField("Name");
            csvWriter.WriteField("AverageSteps");
            csvWriter.WriteField("MaxSteps");
            csvWriter.WriteField("MinSteps");
            csvWriter.NextRecord();
            csvWriter.WriteField(user.Name);
            csvWriter.WriteField(user.AverageSteps);
            csvWriter.WriteField(user.MaxSteps);
            csvWriter.WriteField(user.MinSteps);
            csvWriter.NextRecord();
            csvWriter.WriteRecords(user.Steps);
            textWriter.Flush();
        }
        public  void JsonSerializ(User user)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter($"{user.Name}.json"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, user);
            }
        }
        public ObservableCollection<User> DetectedUsersWhoMaxAndMinStepsDifferentOfAverageSelectedUser(ObservableCollection<User> users, User selectedUser)
        {
            foreach (var user in users)
            {
                if (user.MinSteps <= selectedUser.AverageSteps * 0.8 || user.MaxSteps >= selectedUser.AverageSteps * 1.2)
                {
                    user.DifferentOfAverageSelectedUser = true;
                }
                else
                    user.DifferentOfAverageSelectedUser = false;
            }
            return users;
        }
        public ObservableCollection<User> FormUsersList()
        {
            ObservableCollection<User> Users = new ObservableCollection<User>();
            foreach (var data in LoadData())
            {
                //data.Status = ""
                if (!Users.Any(x => x.Name == data.User))
                {
                    var user = new User(data.User);
                    user.Steps.Add(new DayInfo(data.Day, data.Steps, data.Rank, data.Status));
                    Users.Add(user);
                }
                else
                {
                    var user = Users.FirstOrDefault(x => x.Name == data.User);
                    user.Steps.Add(new DayInfo(data.Day, data.Steps, data.Rank, data.Status));
                }
            }
            
            Users = InitSpecificSteps(Users);
            Users = DetectedUsersWhoMaxAndMinStepsDifferentOfAverageSelectedUser(Users, Users[0]);

            return Users;
        }

        private ObservableCollection<User> InitSpecificSteps(ObservableCollection<User> users)
        {
            foreach (var user in users)
            {
                int minStep = int.MaxValue;
                int maxStep = int.MinValue;
                int averageStep = 0;

                foreach (var userDay in user.Steps)
                {
                    if (minStep > userDay.Steps)
                        minStep = userDay.Steps;

                    if (maxStep < userDay.Steps)
                        maxStep = userDay.Steps;

                    averageStep += userDay.Steps;

                }
                user.MinSteps = minStep;
                user.MaxSteps = maxStep;
                user.AverageSteps = averageStep / user.Steps.Count();
            }
            return users;
        }

        private IEnumerable<UserDay> LoadData()
        {
            int startDay = 1;
            string fileName;
            for (int i = startDay; File.Exists($"TestData/day{i}.json") != false; i++)
            {
                fileName = $"TestData/day{i}.json";
                using (var reader = File.OpenText(fileName))
                {
                    var fileText = reader.ReadToEnd();
                    var result = JsonConvert.DeserializeObject<UserDay[]>(fileText);
                    foreach (var item in result)
                    {
                        item.SetDay(i);
                        yield return item;
                    }
                }
            }
        }
    }
}

