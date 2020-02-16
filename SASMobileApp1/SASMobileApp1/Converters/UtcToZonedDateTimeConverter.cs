using System;
using System.Globalization;
using Xamarin.Forms;

namespace SASMobileApp1.Converters
{
    public class UtcToZonedDateTimeConverter : IValueConverter
    {
        public UtcToZonedDateTimeConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
            string zone = "Europe/London";
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById(zone);
            DateTime dt = TimeZoneInfo.ConvertTimeFromUtc((DateTime)value, tz);
            return dt;
            }
            return null; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
