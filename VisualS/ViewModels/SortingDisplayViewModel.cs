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
        public ObservableCollection<DataPoint> Source { get; } = new ObservableCollection<DataPoint>();

        public SortingDisplayViewModel()
        {
        }

        public async Task LoadDataAsync()
        {
            Source.Clear();

            // TODO WTS: Replace this with your actual data
            var data = await SampleDataService.GetChartDataAsync();
            foreach (var item in data)
            {
                Source.Add(item);
            }
        }
    }
}
