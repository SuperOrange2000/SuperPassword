using System.Windows;

namespace SuperPassword.Common.CustomControl
{
    public class Site : DragableBox
    {
        static Site()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Site), new FrameworkPropertyMetadata(typeof(Site)));
        }
    }
}
