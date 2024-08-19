using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Interactivity;
using Avalonia.Xaml.Interactions.Core;
using Avalonia.Xaml.Interactivity;
using System;

namespace SuperPassword.UI.Interactions;

/// <summary>
/// Executes a specified <see cref="System.Windows.Input.ICommand"/> when invoked. 
/// </summary>
public class InvokeCommandActionWithArgs : InvokeCommandAction
{

    public static readonly StyledProperty<IMultiValueConverter?> ParametersConverterProperty = AvaloniaProperty.Register<InvokeCommandActionWithArgs, IMultiValueConverter?>("ParametersConverter");
    public IMultiValueConverter? ParametersConverter
    {
        get => GetValue(ParametersConverterProperty);
        set => SetValue(ParametersConverterProperty, value);
    }

    /// <summary>
    /// Executes the action.
    /// </summary>
    /// <param name="sender">The <see cref="object"/> that is passed to the action by the behavior. Generally this is <seealso cref="IBehavior.AssociatedObject"/> or a target object.</param>
    /// <param name="parameter">The value of this parameter is determined by the caller.</param>
    /// <returns>True if the command is successfully executed; else false.</returns>
    public override object Execute(object? sender, object? parameter)
    {
        if (IsEnabled != true || Command is null)
        {
            return false;
        }

        object? resolvedParameter = default;
        bool IsSetCommandParameter = IsSet(CommandParameterProperty);

        if (IsSetCommandParameter && PassEventArgsToCommand && ParametersConverter is not null)
        {
            resolvedParameter = ParametersConverter.Convert(
                [parameter, CommandParameter],
                typeof(Tuple<RoutedEventArgs, object?>),
                null,
                System.Globalization.CultureInfo.CurrentCulture);
        }
        else if (IsSet(CommandParameterProperty))
        {
            resolvedParameter = CommandParameter;
        }
        else if (InputConverter is not null)
        {
            resolvedParameter = InputConverter.Convert(
                parameter,
                typeof(object),
                InputConverterParameter,
                InputConverterLanguage is not null
                    ?
                    new System.Globalization.CultureInfo(InputConverterLanguage)
                    : System.Globalization.CultureInfo.CurrentCulture);
        }
        else if (PassEventArgsToCommand)

        {
            resolvedParameter = parameter;
        }

        if (!Command.CanExecute(resolvedParameter))
        {
            return false;
        }

        Command.Execute(resolvedParameter);
        return true;
    }
}