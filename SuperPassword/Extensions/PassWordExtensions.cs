using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace SuperPassword.Extensions
{
    public class PasswordExtensions
    {
        public static string GetPassWord(DependencyObject obj)
        {
            return (string)obj.GetValue(PassWordProperty);
        }

        public static void SetPassWord(DependencyObject obj, string value)
        {
            obj.SetValue(PassWordProperty, value);
        }

        public static readonly DependencyProperty PassWordProperty =
            DependencyProperty.RegisterAttached("PassWord", typeof(string), typeof(PasswordExtensions), new FrameworkPropertyMetadata(string.Empty, OnPassWordPropertyChanged));

        static void OnPassWordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var passWord = sender as PasswordBox;
            string password = (string)e.NewValue;

            if (passWord != null && passWord.Password != password)
                passWord.Password = password;
        }
    }

    public class PasswordBehavior : Behavior<PasswordBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PasswordChanged += AssociatedObject_PasswordChanged;
        }

        private void AssociatedObject_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            string password = PasswordExtensions.GetPassWord(passwordBox);

            if (passwordBox != null && passwordBox.Password != password)
                PasswordExtensions.SetPassWord(passwordBox, passwordBox.Password);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PasswordChanged -= AssociatedObject_PasswordChanged;
        }
    }

}
