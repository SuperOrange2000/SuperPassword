using System.Windows;

namespace SuperPassword.Common.CustomControl
{
    class Tag : EditableText
    {
        static Tag()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Tag), new FrameworkPropertyMetadata(typeof(Tag)));
        }
    }
}
