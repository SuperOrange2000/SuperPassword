using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SuperPassword.UI.Converters
{
    public sealed class MutiParametersConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            return values;
        }
    }
}

