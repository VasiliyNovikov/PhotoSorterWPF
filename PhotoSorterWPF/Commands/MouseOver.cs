using PhotoSorterWPF.Behaviors;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PhotoSorterWPF.Commands
{
    public static class Mouse
    {
        public static readonly DependencyProperty MouseOverCommandProperty = DependencyProperty
            .RegisterAttached("MouseOverCommand", typeof(ICommand<bool>), typeof(Mouse), new PropertyMetadata(OnAnyCommandPropertyChanged));

        public static ICommand<bool> GetMouseOverCommand(DependencyObject obj)
        {
            return (ICommand<bool>)obj.GetValue(MouseOverCommandProperty);
        }

        public static void SetMouseOverCommand(DependencyObject obj, ICommand<bool> value)
        {
            obj.SetValue(MouseOverCommandProperty, value);
        }

        private static void OnAnyCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behaviors = Behavior.GetBehaviors(d);
            if (!behaviors.OfType<Behavior>().Any())
                behaviors.Add(new Behavior());
        }

        class Behavior : Behavior<UIElement>
        {
            protected override void Attached()
            {
                AssociatedObject.MouseEnter += OnMouseEnter;
                AssociatedObject.MouseLeave += OnMouseLeave;
            }

            protected override void Detaching()
            {
                AssociatedObject.MouseEnter -= OnMouseEnter;
                AssociatedObject.MouseLeave -= OnMouseLeave;
            }

            private void OnMouseEnter(object sender, MouseEventArgs e)
            {
                GetMouseOverCommand(AssociatedObject)?.Execute(true);
            }
            private void OnMouseLeave(object sender, MouseEventArgs e)
            {
                GetMouseOverCommand(AssociatedObject)?.Execute(false);
            }
        }
    }
}
