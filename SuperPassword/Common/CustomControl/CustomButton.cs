using System.Windows;
using System.Windows.Controls;

namespace SuperPassword.Common.CustomControl
{
    class CustomButton : Button
    {
        static CustomButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomButton), new FrameworkPropertyMetadata(typeof(CustomButton)));
        }

        public bool Dragable
        {
            get { return (bool)GetValue(DragableProperty); }
            set { SetValue(DragableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Dragable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DragableProperty =
            DependencyProperty.Register("Dragable", typeof(bool), typeof(CustomButton), new PropertyMetadata(false));


        public int CornerRadius
        {
            get { return (int)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(int), typeof(CustomButton), new PropertyMetadata(0));


    }
}
