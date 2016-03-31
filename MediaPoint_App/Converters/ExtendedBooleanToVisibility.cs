using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows;

namespace MediaPoint.Converters
{
	public class ExtendedBooleanToVisibility : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			bool val;

            try
            {
                val = (bool)value;
            }
            catch
            {
                bool.TryParse(value.ToString(), out val);
            }

			bool invert = parameter != null ? bool.Parse(parameter.ToString()) : false;

			if (invert) val = !val;

			if (val)
			{
				return Visibility.Visible;
			}
			else
			{
				return Visibility.Collapsed;
			}

		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}
	}
}
