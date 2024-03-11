using System.Windows;

namespace SuperPassword.Controls
{
    public class Site : DragableBox
    {
        static Site()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Site), new FrameworkPropertyMetadata(typeof(Site)));
        }
    }
}
