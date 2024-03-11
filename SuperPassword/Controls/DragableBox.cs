using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SuperPassword.Controls
{
    public class DragableBox : Control, ICommandSource
    {
        static DragableBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DragableBox), new FrameworkPropertyMetadata(typeof(DragableBox)));
        }


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(DragableBox), new PropertyMetadata(string.Empty));

        public string Content
        {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(string), typeof(DragableBox), new PropertyMetadata(string.Empty));

        public int CornerRadius
        {
            get { return (int)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(int), typeof(DragableBox), new PropertyMetadata(0));


        public Brush HoverBackground
        {
            get { return (Brush)GetValue(HoverBackgroundProperty); }
            set { SetValue(HoverBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HoverBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register("HoverBackground", typeof(Brush), typeof(DragableBox), new PropertyMetadata(null));



        public Brush HoverBorderBrush
        {
            get { return (Brush)GetValue(HoverBorderBrushProperty); }
            set { SetValue(HoverBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HoverBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoverBorderBrushProperty =
            DependencyProperty.Register("HoverBorderBrush", typeof(Brush), typeof(DragableBox), new PropertyMetadata(null));


        public Brush HoverForeground
        {
            get { return (Brush)GetValue(HoverForegroundProperty); }
            set { SetValue(HoverForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HoverForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoverForegroundProperty =
            DependencyProperty.Register("HoverForeground", typeof(Brush), typeof(DragableBox), new PropertyMetadata(null));


        public bool Dragable
        {
            get { return (bool)GetValue(DragableProperty); }
            set { SetValue(DragableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Dragable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DragableProperty =
            DependencyProperty.Register("Dragable", typeof(bool), typeof(DragableBox), new PropertyMetadata(false));

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (Dragable)
                DragDrop.DoDragDrop(this, Content, DragDropEffects.Copy);
        }

        public ICommand Command => throw new NotImplementedException();

        public object CommandParameter => throw new NotImplementedException();

        public IInputElement CommandTarget => throw new NotImplementedException();
    }
}
