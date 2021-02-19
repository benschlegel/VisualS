using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using VisualS.Core.Models;
using VisualS.Core.Services;
using VisualS.Helpers;

namespace VisualS.ViewModels
{
    public class SortingDisplayViewModel : Observable
    {
        public ObservableCollection<BarItem> Source { get; } = new ObservableCollection<BarItem>();

        public SortingDisplayViewModel()
        {
            BarDataStore.Instance.PropertyChanged += Instance_PropertyChanged;
        }

        private void Instance_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case nameof(BarDataStore.MaxValue):
                    //BarDataStore.Instance.MaxValue;
                    break;
                case nameof(BarDataStore.MaxBars):
                    //BarDataStore.Instance.MaxValue;
                    break;
            }
        }

        public Task LoadDataAsync()
        {
            Source.Clear();

            // TODO WTS: Replace this with your actual data
            //var data = await SampleDataService.GetChartDataAsync();
            //foreach (var item in data)
            //{
            //    Source.Add(item);
            //}


            var data = SampleDataService.GenerateRandomBars(BarDataStore.Instance.MaxBars, BarDataStore.Instance.MaxValue);
            foreach(var item in data)
            {
                Source.Add(item);
            }

            return Task.CompletedTask;
        }
    }
}
