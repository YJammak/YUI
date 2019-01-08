using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace YUI.WPF.YControls
{
    /// <summary>
    /// 加载控件
    /// </summary>
    [TemplateVisualState(Name = "Large", GroupName = "SizeStates")]
    [TemplateVisualState(Name = "Small", GroupName = "SizeStates")]
    [TemplateVisualState(Name = "Inactive", GroupName = "ActiveStates")]
    [TemplateVisualState(Name = "Active", GroupName = "ActiveStates")]
    public class YProgressRing : Control
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty BindableWidthProperty = DependencyProperty.Register("BindableWidth", typeof(double), typeof(YProgressRing), new PropertyMetadata(default(double), BindableWidthCallback));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(YProgressRing), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsActiveChanged));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IsLargeProperty = DependencyProperty.Register("IsLarge", typeof(bool), typeof(YProgressRing), new PropertyMetadata(true, IsLargeChangedCallback));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty MaxSideLengthProperty = DependencyProperty.Register("MaxSideLength", typeof(double), typeof(YProgressRing), new PropertyMetadata(default(double)));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty EllipseDiameterProperty = DependencyProperty.Register("EllipseDiameter", typeof(double), typeof(YProgressRing), new PropertyMetadata(default(double)));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty EllipseOffsetProperty = DependencyProperty.Register("EllipseOffset", typeof(Thickness), typeof(YProgressRing), new PropertyMetadata(default(Thickness)));

        private List<Action> _deferredActions = new List<Action>();

        static YProgressRing()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YProgressRing), new FrameworkPropertyMetadata(typeof(YProgressRing)));
            VisibilityProperty.OverrideMetadata(typeof(YProgressRing),
                new FrameworkPropertyMetadata(
                    (ringObject, e) =>
                    {
                        if (e.NewValue == e.OldValue) return;

                        var ring = (YProgressRing)ringObject;
                        //auto set IsActive to false if we're hiding it.
                        if ((Visibility)e.NewValue != Visibility.Visible)
                        {
                            //sets the value without overriding it's binding (if any).
                            ring.SetCurrentValue(IsActiveProperty, false);
                        }
                        else
                        {
                            // #1105 don't forget to re-activate
                            ring.IsActive = true;
                        }
                    }));
        }
        /// <summary>
        /// 
        /// </summary>
        public YProgressRing()
        {
            SizeChanged += OnSizeChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        public double MaxSideLength
        {
            get => (double)GetValue(MaxSideLengthProperty);
            private set => SetValue(MaxSideLengthProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public double EllipseDiameter
        {
            get => (double)GetValue(EllipseDiameterProperty);
            private set => SetValue(EllipseDiameterProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public Thickness EllipseOffset
        {
            get => (Thickness)GetValue(EllipseOffsetProperty);
            private set => SetValue(EllipseOffsetProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public double BindableWidth
        {
            get => (double)GetValue(BindableWidthProperty);
            private set => SetValue(BindableWidthProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsLarge
        {
            get => (bool)GetValue(IsLargeProperty);
            set => SetValue(IsLargeProperty, value);
        }

        private static void BindableWidthCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ring = dependencyObject as YProgressRing;
            if (ring == null)
                return;

            var action = new Action(() =>
            {
                ring.SetEllipseDiameter(
                    (double)dependencyPropertyChangedEventArgs.NewValue);
                ring.SetEllipseOffset(
                    (double)dependencyPropertyChangedEventArgs.NewValue);
                ring.SetMaxSideLength(
                    (double)dependencyPropertyChangedEventArgs.NewValue);
            });

            if (ring._deferredActions != null)
                ring._deferredActions.Add(action);
            else
                action();
        }

        private void SetMaxSideLength(double width)
        {
            MaxSideLength = width <= 20 ? 20 : width;
        }

        private void SetEllipseDiameter(double width)
        {
            EllipseDiameter = width / 8;
        }

        private void SetEllipseOffset(double width)
        {
            EllipseOffset = new Thickness(0, width / 2, 0, 0);
        }

        private static void IsLargeChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ring = dependencyObject as YProgressRing;

            ring?.UpdateLargeState();
        }

        private void UpdateLargeState()
        {
            Action action;

            if (IsLarge)
                action = () => VisualStateManager.GoToState(this, "Large", true);
            else
                action = () => VisualStateManager.GoToState(this, "Small", true);

            if (_deferredActions != null)
                _deferredActions.Add(action);

            else
                action();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            BindableWidth = ActualWidth;
        }

        private static void IsActiveChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ring = dependencyObject as YProgressRing;

            ring?.UpdateActiveState();
        }

        private void UpdateActiveState()
        {
            Action action;

            if (IsActive)
                action = () => VisualStateManager.GoToState(this, "Active", true);
            else
                action = () => VisualStateManager.GoToState(this, "Inactive", true);

            if (_deferredActions != null)
                _deferredActions.Add(action);

            else
                action();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnApplyTemplate()
        {
            //make sure the states get updated
            UpdateLargeState();
            UpdateActiveState();
            base.OnApplyTemplate();
            if (_deferredActions != null)
                foreach (var action in _deferredActions)
                    action();
            _deferredActions = null;
        }
    }

    internal class WidthToMaxSideLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double)) return null;

            var width = (double)value;
            return width <= 20 ? 20 : width;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
