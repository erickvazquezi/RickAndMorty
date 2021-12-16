using System;
using System.Globalization;
using Xamarin.Forms;

namespace RickAndMorthyApp.Utilities
{
    public class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)value)
            {
                case "Alive":
                    return Color.Green;
                case "unknown":
                    return Color.BlueViolet;
                case "Dead":
                    return Color.Red;
                default:
                    return Color.Black;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
