using System.Windows;
using System.Windows.Controls;

namespace YUI.WPF.YControls
{
    /// <inheritdoc />
    /// <summary>
    /// A control featuring a range of loading indicating animations.
    /// https://github.com/zeluisping/LoadingIndicators.WPF
    /// </summary>
    [TemplatePart(Name = "Border", Type = typeof(Border))]
    public class YLoadingIndicator : Control
    {
        /// <summary>
        /// Identifies the <see cref="YLoadingIndicator.SpeedRatio"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SpeedRatioProperty =
            DependencyProperty.Register("SpeedRatio", typeof(double), typeof(YLoadingIndicator), new PropertyMetadata(1d, (o, e) => {
                var li = (YLoadingIndicator)o;

                if (li.PART_Border == null || li.IsActive == false)
                {
                    return;
                }

                var groups = VisualStateManager.GetVisualStateGroups(li.PART_Border);

                if (groups == null)
                    return;

                foreach (VisualStateGroup group in groups)
                {
                    if (group.Name != "ActiveStates") continue;

                    foreach (VisualState state in group.States)
                    {
                        if (state.Name == "Active")
                        {
                            state.Storyboard.SetSpeedRatio(li.PART_Border, (double)e.NewValue);
                        }
                    }
                }
            }));

        /// <summary>
        /// Identifies the <see cref="YLoadingIndicator.IsActive"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(YLoadingIndicator), new PropertyMetadata(true, (o, e) => {
                var li = (YLoadingIndicator)o;

                if (li.PART_Border == null)
                {
                    return;
                }

                if ((bool)e.NewValue == false)
                {
                    VisualStateManager.GoToElementState(li.PART_Border, "Inactive", false);
                    li.PART_Border.Visibility = Visibility.Collapsed;
                }
                else
                {
                    VisualStateManager.GoToElementState(li.PART_Border, "Active", false);
                    li.PART_Border.Visibility = Visibility.Visible;

                    var groups = VisualStateManager.GetVisualStateGroups(li.PART_Border);

                    if (groups == null)
                        return;

                    foreach (VisualStateGroup group in groups)
                    {
                        if (group.Name != "ActiveStates") continue;

                        foreach (VisualState state in group.States)
                        {
                            if (state.Name == "Active")
                            {
                                state.Storyboard.SetSpeedRatio(li.PART_Border, li.SpeedRatio);
                            }
                        }
                    }
                }
            }));

        // Variables
        /// <summary>
        /// 
        /// </summary>
        protected Border PART_Border;

        /// <summary>
        /// Get/set the speed ratio of the animation.
        /// </summary>
        public double SpeedRatio
        {
            get => (double)GetValue(SpeedRatioProperty);
            set => SetValue(SpeedRatioProperty, value);
        }

        /// <summary>
        /// Get/set whether the loading indicator is active.
        /// </summary>
        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code
        /// or internal processes call System.Windows.FrameworkElement.ApplyTemplate().
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_Border = (Border)GetTemplateChild("PART_Border");

            if (PART_Border == null)
                return;

            VisualStateManager.GoToElementState(PART_Border, (IsActive ? "Active" : "Inactive"), false);

            var groups = VisualStateManager.GetVisualStateGroups(PART_Border);

            if (groups != null)
            {
                foreach (VisualStateGroup group in groups)
                {
                    if (group.Name != "ActiveStates") continue;

                    foreach (VisualState state in group.States)
                    {
                        if (state.Name == "Active")
                        {
                            state.Storyboard.SetSpeedRatio(PART_Border, SpeedRatio);
                        }
                    }
                }
            }

            PART_Border.Visibility = (IsActive ? Visibility.Visible : Visibility.Collapsed);
        }
    }
}
