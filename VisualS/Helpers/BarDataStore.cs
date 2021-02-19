using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VisualS.Helpers
{
    public class BarDataStore : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _maxValue;

        public int MaxValue
        {
            get
            {
                OnPropertyChanged();
                return _maxValue;
            }
            set
            {
                if (value > 0)
                {
                    _maxValue = value;
                    OnPropertyChanged();
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
                    _maxBars = value;
                    OnPropertyChanged();
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private BarDataStore()
        {
            MaxValue = 200;
            MaxBars = 20;
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
