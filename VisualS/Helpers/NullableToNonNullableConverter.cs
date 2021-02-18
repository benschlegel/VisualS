using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace VisualS.Helpers
{
    [TypeConverter(typeof(int?))]
    class NullableToNonNullableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (int) value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value ?? 0;
        }
    }
}
