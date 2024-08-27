using Avalonia.Data.Converters;
using SuperPassword.Commom.ObjectModel;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SuperPassword.UI.Converters
{
    public class DictionaryKeyValueConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value.Count >= 2 && value[0] is string sourceText && value[1] is ObservableDictionary<string, bool> dict)
            {
                return dict[sourceText];
            }
            return null;
        }
    }
}
