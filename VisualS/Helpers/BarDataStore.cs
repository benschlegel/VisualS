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

        public readonly static int DefaultMaxValue = 250;
        public readonly static int DefaultMaxBars = 12;

        private int _maxValue;

        public int MaxValue
        {
            get
            {
                return _maxValue;
            }
            set
            {
                if (value > 0)
                {
                    if (value != _maxValue)
                    {
                        Task.Run(async () => await Windows.Storage.ApplicationData.Current.LocalSettings.SaveAsync(nameof(BarDataStore.Instance.MaxValue), value));
                        var SettingValue = Task.Run(async () => await Windows.Storage.ApplicationData.Current.LocalSettings.ReadAsync<int>(nameof(BarDataStore.Instance.MaxValue)));
                    }
                    _maxValue = value;
                    OnPropertyChanged();
                }else
                {
                    _maxValue = DefaultMaxValue;
                }
            }
        }

        private int _maxBars;

        public int MaxBars
        {
            get { return _maxBars; }
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
            //This overwrites values before getting read from settings
            //TODO: Remove ugly workaround for default values in SettingsViewModel
            //MaxValue = 200;
            //MaxBars = 20;
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
