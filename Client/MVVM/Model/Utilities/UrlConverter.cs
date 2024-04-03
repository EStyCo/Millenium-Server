using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MVVM.Model.Utilities
{
    public class UrlConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool isToggled = (bool)value;

            if (isToggled) BaseUrl.SwitchToLocal();
            else BaseUrl.SwitchToVPS();
                
            return isToggled ? "local" : "VPS";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
