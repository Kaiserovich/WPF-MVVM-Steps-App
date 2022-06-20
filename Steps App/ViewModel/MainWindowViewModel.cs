using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Steps_App.Models;
using LiveCharts;
using Steps_App.View;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using LiveCharts.Configurations;
using System.Windows.Media;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Steps_App.ViewModel
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {   
        UserManager userManager = new UserManager();
        public ObservableCollection<User> UsersList { get; set; }

        public ChartValues<int> GraphDayStepsValue = new ChartValues<int>();
        public CartesianMapper<ChartValues<int>> Mapper { get; set; }
        public SeriesCollection GraphDaySteps { get; set; }
        public ICommand JsonExportCommand { get; set; }
        public ICommand XmlExportCommand { get; set; }
        public ICommand CsvExportCommand { get; set; }


        private User selectUser;
        public User SelectUser
        {
            get { return selectUser; }
            set
            {
                    selectUser = value;
                    OnPropertyChanged("SelectUser");

                    UsersList = userManager.DetectedUsersWhoMaxAndMinStepsDifferentOfAverageSelectedUser(UsersList, selectUser);
                    GraphDaySteps = BuildGrapgDaySteps(selectUser);
                    

            }
        }
        public SeriesCollection BuildGrapgDaySteps(User selectUser)
        {
            GraphDayStepsValue.Clear();
            foreach (var daySteps in selectUser.Steps)
            {
                GraphDayStepsValue.Add(daySteps.Steps);
            }
            
            return GraphDaySteps;
        }

        public MainWindowViewModel()
        {
            UsersList = userManager.FormUsersList();
            selectUser = UsersList[0];

            foreach (var daySteps in selectUser.Steps)
            {
                GraphDayStepsValue.Add(daySteps.Steps);
            }

            GraphDaySteps = new SeriesCollection
            {
            new LineSeries
            {
                Values = GraphDayStepsValue,
                Title = $"Шаги: ",
                DataLabels = true,
                Configuration = Mappers.Xy<int>()
                                   .X((value, index) => index)
                                   .Y((value, index) => value)
                                   .Stroke((value, index) => value == selectUser.MaxSteps || value == selectUser.MinSteps? Brushes.Red : Brushes.Blue)
                                   .Fill((value, index) => value == selectUser.MaxSteps || value == selectUser.MinSteps? Brushes.Red : Brushes.White)

            }


            };
            JsonExportCommand = new DelegateCommand(JsonExport);
            XmlExportCommand = new DelegateCommand(XmlExport);
            CsvExportCommand = new DelegateCommand(CsvExport);
        }

        private void JsonExport(object obj)
        {
            userManager.JsonSerializ(SelectUser);
        }
        private void XmlExport(object obj)
        {
            userManager.WriteXML(SelectUser);
        }
        private void CsvExport(object obj)
        {
            userManager.WriteCvs(SelectUser);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) // if there is any subscribers 
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
