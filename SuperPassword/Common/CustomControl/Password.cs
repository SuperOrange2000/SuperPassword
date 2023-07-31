using System.Windows;

namespace SuperPassword.Common.CustomControl
{
    class Password : EditableText
    {
        static Password()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Password), new FrameworkPropertyMetadata(typeof(Password)));
        }
    }
}
