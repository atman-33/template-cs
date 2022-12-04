using Microsoft.Xaml.Behaviors;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Template.WPF.Services
{
    [TypeConstraint(typeof(Selector))]
    public class BindingSelectedItemsBehavior : Behavior<Selector>
    {
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems",
                                        typeof(IEnumerable),
                                        typeof(BindingSelectedItemsBehavior),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public IEnumerable SelectedItems
        {
            get => (IEnumerable)GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectionChanged += SelectionChanged;
        }
        protected override void OnDetaching()
        {
            AssociatedObject.SelectionChanged -= SelectionChanged;
            base.OnDetaching();
        }
        private void SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            //! MultiSelector.SelectedItems や ListBox.SelectedItems を同じ物として扱いたい。
            dynamic selector = AssociatedObject;
            SelectedItems = Enumerable.ToArray(selector.SelectedItems);
        }
    }
}