using System.Windows;

namespace SuperPassword.Controls
{
    class Password : DragableBox
    {
        static Password()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Password), new FrameworkPropertyMetadata(typeof(Password)));
        }
    }
}
