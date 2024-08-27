using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace SuperPassword.UI.Models;
public partial class Tag : ObservableObject
{
    [ObservableProperty]
    private string content;

    [ObservableProperty]
    private bool isSelected;

    public Guid TagGuid { get; set; } = Guid.NewGuid();

    public override string ToString()
    {
        return Content;
    }
}
