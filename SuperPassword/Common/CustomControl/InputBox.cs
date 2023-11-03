using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SuperPassword.Common.CustomControl
{
    class InputBox : TextBox
    {
        static InputBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InputBox), new FrameworkPropertyMetadata(typeof(InputBox)));
        }

        public Brush Color
        {
            get { return (Brush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Brush), typeof(InputBox), new PropertyMetadata(null));

    }
}