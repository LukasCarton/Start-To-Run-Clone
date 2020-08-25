using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace IntervalRunning.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Command StartRunningCommand { get; }

        public MainPageViewModel()
        {
            StartRunningCommand = new Command(() =>
            {
                Console.WriteLine("----------- Start tracking... ------------");
            });
        }
    }
}
