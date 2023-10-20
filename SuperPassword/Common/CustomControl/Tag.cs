using System.Windows;

namespace SuperPassword.Common.CustomControl
{
    class Tag : DragableBox
    {
        static Tag()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Tag), new FrameworkPropertyMetadata(typeof(Tag)));

        }

        public string Color
        {
            get { return (string)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(string), typeof(Tag), new PropertyMetadata(""));


    }
}
