using System;
using System.Windows;
using System.Windows.Controls;

namespace SuperPassword.Common.CustomControl
{
    class BaseButton:Button
    {
        static BaseButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BaseButton), new FrameworkPropertyMetadata(typeof(BaseButton)));
        }

        public bool Dragable
        {
            get { return (bool)GetValue(DragableProperty); }
            set { SetValue(DragableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Dragable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DragableProperty =
            DependencyProperty.Register("Dragable", typeof(bool), typeof(BaseButton), new PropertyMetadata(false));


        public int CornerRadius
        {
            get { return (int)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(int), typeof(BaseButton), new PropertyMetadata(0));


    }
}
