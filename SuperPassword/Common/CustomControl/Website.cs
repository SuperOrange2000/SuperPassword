using System.Windows;

namespace SuperPassword.Common.CustomControl
{
    public class Website : DragableBox
    {
        static Website()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Website), new FrameworkPropertyMetadata(typeof(Website)));
        }
    }
}
