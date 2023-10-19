using System.Windows;

namespace SuperPassword.Common.CustomControl
{
    class Password : EditableBox
    {
        static Password()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Password), new FrameworkPropertyMetadata(typeof(Password)));
        }
    }
}
