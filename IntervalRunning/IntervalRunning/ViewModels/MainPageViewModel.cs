using IntervalRunning.Models;
using IntervalRunning.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IntervalRunning.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Command NavigateToIntervalPageCommand { get; }

        public ObservableCollection<Intervals> Intervals { get; set; }

        Intervals selectedInterval;
        public Intervals SelectedInterval
        {
            get => selectedInterval;
            set
            {
                selectedInterval = value;
                var args = new PropertyChangedEventArgs(nameof(SelectedInterval));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public MainPageViewModel()
        {
            Intervals = new ObservableCollection<Intervals>();
            SelectedInterval = null;
            //Load json
            var assembly = typeof(MainPageViewModel).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.intervals.json");

            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                List<Intervals> intervalsList = JsonConvert.DeserializeObject<List<Intervals>>(json);
                foreach (Intervals item in intervalsList)
                {
                    Intervals.Add(item);
                    Console.WriteLine($"---week: {item.week}, day: {item.day}");
                }

            }


            NavigateToIntervalPageCommand = new Command(async () =>
            {
                Console.WriteLine("----------- Start tracking... ------------");
                if (SelectedInterval != null)
                {
                    Console.WriteLine($"week: {SelectedInterval.week}, day: {SelectedInterval.day}");
                    await NavigateToIntervalPage();
                }
                else
                {
                    Console.WriteLine("----------- Nothing selected... ------------");
                }
            });
        }

        private async Task NavigateToIntervalPage()
        {
            var intervalVM = new IntervalViewModel(SelectedInterval.intervals);
            var intervalPage = new IntervalPage();
            intervalPage.BindingContext = intervalVM;

            await Application.Current.MainPage.Navigation.PushAsync(intervalPage);
        }
    }
}
