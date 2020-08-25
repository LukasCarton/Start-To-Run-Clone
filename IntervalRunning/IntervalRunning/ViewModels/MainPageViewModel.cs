using IntervalRunning.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace IntervalRunning.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Command StartRunningCommand { get; }

        public ObservableCollection<Intervals> Intervals { get; set; }

        public MainPageViewModel()
        {
            Intervals = new ObservableCollection<Intervals>();
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


            StartRunningCommand = new Command(() =>
            {
                Console.WriteLine("----------- Start tracking... ------------");

            });
        }
    }
}
