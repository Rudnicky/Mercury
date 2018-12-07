using System;
using System.Windows;
using System.Windows.Input;

namespace MercuryMessenger.Behaviors
{
    public class WindowBehavior
    {
        #region Unloaded
        public static readonly DependencyProperty UnloadedCommandProperty =
            DependencyProperty.RegisterAttached("UnloadedCommand",
                typeof(ICommand),
                typeof(WindowBehavior),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(UnloadedInvokedCallback)));

        private static void UnloadedInvokedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                element.Unloaded += Element_Unloaded;
            }
        }

        private static void Element_Unloaded(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                ICommand command = GetUnloadedCommand(element);
                if (command != null)
                {
                    command.Execute(new EventArgs());
                }
            }
        }

        public static void SetUnloadedCommand(UIElement element, ICommand value)
        {
            element.SetValue(UnloadedCommandProperty, value);
        }

        public static ICommand GetUnloadedCommand(UIElement element)
        {
            return (ICommand)element.GetValue(UnloadedCommandProperty);
        }
        #endregion

        #region Closing
        public static readonly DependencyProperty ClosingCommandProperty =
            DependencyProperty.RegisterAttached("ClosingCommand",
                typeof(ICommand),
                typeof(WindowBehavior),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(ClosingInvokedCallback)));

        private static void ClosingInvokedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window element)
            {
                element.Closing += Element_Closing;
            }
        }

        private static void Element_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (sender is Window element)
            {
                ICommand command = GetClosingCommand(element);
                if (command != null)
                {
                    command.Execute(new EventArgs());
                }
            }
        }

        public static void SetClosingCommand(Window element, ICommand value)
        {
            element.SetValue(ClosingCommandProperty, value);
        }

        public static ICommand GetClosingCommand(Window element)
        {
            return (ICommand)element.GetValue(ClosingCommandProperty);
        }
        #endregion
    }
}
