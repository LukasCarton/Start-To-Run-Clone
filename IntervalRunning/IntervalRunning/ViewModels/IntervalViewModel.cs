using IntervalRunning.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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

        Stopwatch stopwatch;

        public IntervalViewModel(List<Interval> selectedIntervals)
        {
            Intervals = new ObservableCollection<Interval>(selectedIntervals);
            stopwatch = new Stopwatch();
            StartRunningCommand = new Command(async() =>
            {
                int counter = 0;
                foreach (Interval interval in Intervals) 
                {
                    int time = interval.interval_lopen * 1000;
                    await Task.Delay(time);
                    Debug.WriteLine($"--------- after lopen, counter: {counter}");
                    time = interval.interval_wandel * 1000;

                    await Task.Delay(time);
                    Debug.WriteLine($"--------- after wandel, counter: {counter}");

                    counter++;
                }
            });
        }
    }
}
