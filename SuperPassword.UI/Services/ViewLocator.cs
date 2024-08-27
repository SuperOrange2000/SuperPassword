using Avalonia.Controls;
using Avalonia.Controls.Templates;
using SuperPassword.UI.ViewModels;
using System;

namespace SuperPassword.UI.Services;
public class ViewLocator : IDataTemplate
{
    public bool SupportsRecycling => false;

    public Control? Build(object? data)
    {
        string? name = data?.GetType()?.FullName?.Replace("ViewModel", "View");
        if (name == null) return new TextBlock { Text = "Not Found: " + name };

        Type? type = Type.GetType(name);

        if (type != null)
            return (Control?)Activator.CreateInstance(type);
        else
            return new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}