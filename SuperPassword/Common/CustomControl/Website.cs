using System.Windows;

namespace SuperPassword.Common.CustomControl
{
    public class Website : EditableText
    {
        static Website()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Website), new FrameworkPropertyMetadata(typeof(Website)));
        }
    }
}
