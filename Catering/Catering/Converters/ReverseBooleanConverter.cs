﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Catering.Converters
{
    public class ReverseBooleanConverter : IValueConverter
    {
        public object Convert(object value,
                      Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Boolean))
            {
                return value;
            }

            return !((Boolean)value);
        }

        public object ConvertBack(object value,
            Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Boolean))
            {
                return value;
            }

            return !((Boolean)value);
        }
    }
}
