using IntervalRunning.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IntervalRunning.ViewModels
{
    public class IntervalViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Interval> Intervals { get; set; }
        public Command StartRunningCommand { get; }

        string action;
        public string Action
        {
            get => action;
            set
            {
                action = value;
                var args = new PropertyChangedEventArgs(nameof(Action));
                PropertyChanged?.Invoke(this, args);
            }
        }

        int intervalCounter;
        public int IntervalCounter
        {
            get => intervalCounter;
            set
            {
                intervalCounter = value;
                var args = new PropertyChangedEventArgs(nameof(IntervalCounter));
                PropertyChanged?.Invoke(this, args);
            }
        }

        /// <summary>
        /// Disable button when pressed
        /// </summary>
        bool isEnabled;
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                var args = new PropertyChangedEventArgs(nameof(IsEnabled));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public IntervalViewModel(List<Interval> selectedIntervals)
        {
            IsEnabled = true;
            //Action = "Press start";
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream audioStream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.BEEP.mp3");
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load(audioStream);

            Intervals = new ObservableCollection<Interval>(selectedIntervals);
            StartRunningCommand = new Command(async() =>
            {
                IsEnabled = false;
                int counter = 1;
                foreach (Interval interval in Intervals) 
                {
                    IntervalCounter = counter;
                    Action = "Running";
                    int time = interval.interval_lopen * 1000 * 60;
                    await Task.Delay(time);
                    player.Play(); // => Sign Stop running, start walking

                    Action = "Walking";
                    time = interval.interval_wandel * 1000 * 60;
                    await Task.Delay(time);
                    player.Play(); // => Sign Stop walking, start running

                    counter++;
                }

                // end
                Action = "End";
            });
        }
    }
}
