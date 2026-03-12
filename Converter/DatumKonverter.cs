using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Notizbuch
{
    public class DatumKonverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var notizDatum = (DateTime)value;
            string para = parameter as string;

            if (targetType == typeof(string))
            {
                return notizDatum.ToShortDateString();
            }

            if (targetType == typeof(Brush))
            {
                if (para == "vordergrund")
                {
                    return notizDatum < DateTime.Today
                        ? Brushes.DarkBlue
                        : Brushes.Red;
                }

                if (para == "hintergrund")
                {
                    return notizDatum < DateTime.Today
                        ? Brushes.Orange
                        : Brushes.LightGreen;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}