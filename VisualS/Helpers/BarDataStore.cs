using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VisualS.Helpers
{
    public class BarDataStore : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //TODO: change to private
        public readonly static int DefaultMaxValue = 250;
        public readonly static int DefaultMaxBars = 12;

        //TODO: Proper initializiation
        private int _maxValue;

        public int MaxValue
        {
            get {
                if(_maxValue == 0) { return DefaultMaxValue; }
                return _maxValue;
            }
            set
            {
                if (value > 0)
                {
                    if (value != _maxValue)
                    {
                        Windows.Storage.ApplicationData.Current.LocalSettings.SaveAsync(nameof(BarDataStore.Instance.MaxValue), value);
                    }
                    _maxValue = value;
                    OnPropertyChanged();
                }
                else
                {
                    _maxValue = DefaultMaxValue;
                }
            }
        }

        private int _maxBars;

        public int MaxBars
        {
            get
            {
                if (_maxValue == 0) { return DefaultMaxBars; }
                return _maxBars;
            }
            set
            {
                if(value > 0)
                {
                    if (value != _maxBars)
                    {
                        Windows.Storage.ApplicationData.Current.LocalSettings.SaveAsync(nameof(MaxBars), value);
                    }
                    _maxBars = value;
                    OnPropertyChanged();
                }else
                {
                    _maxBars = DefaultMaxBars;
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private BarDataStore()
        {
            //Potentially revert to this
            //MaxValue = Windows.Storage.ApplicationData.Current.LocalSettings.ReadAsync<int?>(nameof(MaxValue)).Result ?? DefaultMaxBars;
            //MaxBars = Windows.Storage.ApplicationData.Current.LocalSettings.ReadAsync<int?>(nameof(MaxBars)).Result ?? DefaultMaxBars;
        }

        private BarDataStore(int MaxValue, int MaxBars)
        {
            this.MaxValue = MaxValue;
            this.MaxBars = MaxBars;
        }

        private static BarDataStore _instance;
        public static BarDataStore Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BarDataStore();
                return _instance;
            }
        }

    }
}
