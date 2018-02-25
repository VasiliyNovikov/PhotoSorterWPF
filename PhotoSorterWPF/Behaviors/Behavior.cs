using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace PhotoSorterWPF.Behaviors
{
    public class Behavior
    {
        public DependencyObject AssociatedObject { get; private set; }

        protected virtual void Attached() { }

        protected virtual void Detaching() { }

        private static DependencyPropertyKey BehaviorsPropertyKey = DependencyProperty.RegisterAttachedReadOnly("Behaviors", typeof(Collection<Behavior>), typeof(Behavior), new PropertyMetadata());
        public static DependencyProperty BehaviorsProperty = BehaviorsPropertyKey.DependencyProperty;

        public static Collection<Behavior> GetBehaviors(DependencyObject @object)
        {
            var behaviors = (Collection<Behavior>)@object.GetValue(BehaviorsProperty);
            if (behaviors == null)
            {
                behaviors = new BehaviorCollection(@object);
                @object.SetValue(BehaviorsPropertyKey, behaviors);
            }
            return behaviors;
        }

        private class BehaviorCollection : Collection<Behavior>
        {
            private readonly DependencyObject _associatedObject;

            public BehaviorCollection(DependencyObject associatedObject)
            {
                this._associatedObject = associatedObject;
            }

            protected override void InsertItem(int index, Behavior item)
            {
                if (item == null) throw new ArgumentNullException(nameof(item));

                base.InsertItem(index, item);
                item.AssociatedObject = this._associatedObject;
                item.Attached();
            }

            protected override void RemoveItem(int index)
            {
                var behavior = this[index];
                behavior.Detaching();
                behavior.AssociatedObject = null;
                base.RemoveItem(index);
            }

            protected override void ClearItems()
            {
                foreach (var behavior in this)
                {
                    behavior.Detaching();
                    behavior.AssociatedObject = null;
                }
                base.ClearItems();
            }

            protected override void SetItem(int index, Behavior item)
            {
                if (item == null) throw new ArgumentNullException(nameof(item));

                var behavior = this[index];
                if (behavior != item)
                {
                    behavior.Detaching();
                    behavior.AssociatedObject = null;
                }

                base.SetItem(index, item);

                if (behavior != item)
                {
                    item.AssociatedObject = this._associatedObject;
                    item.Attached();
                }
            }
        }
    }

    public abstract class Behavior<T> : Behavior where T : DependencyObject
    {
        public new T AssociatedObject => (T)base.AssociatedObject;
    }
}
