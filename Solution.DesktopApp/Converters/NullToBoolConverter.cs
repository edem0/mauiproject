﻿using System;
using System.Globalization;

namespace Solution.DesktopApp.Converters
{
    public class NullToBoolConverter : IValueConverter
    {
        public bool Invert { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = value != null;
            return Invert ? !result : result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
    