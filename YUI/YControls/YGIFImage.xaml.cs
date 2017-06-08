using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace YUI.YControls
{
    /// <summary>
    /// YGIFImage.xaml 的交互逻辑
    /// </summary>
    [TemplatePart(Name = "PART_PalyControl", Type = typeof(MediaElement))]
    public class YGIFImage : UserControl
    {
        static YGIFImage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YGIFImage), new FrameworkPropertyMetadata(typeof(YGIFImage)));
        }

        private MediaElement Element { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source", typeof(Uri), typeof(YGIFImage), new PropertyMetadata(default(Uri)));

        /// <summary>
        /// 
        /// </summary>
        public Uri Source
        {
            get => (Uri) GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(
            "Stretch", typeof(Stretch), typeof(YGIFImage), new PropertyMetadata(Stretch.Uniform));

        /// <summary>
        /// 
        /// </summary>
        public Stretch Stretch
        {
            get => (Stretch) GetValue(StretchProperty);
            set => SetValue(StretchProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Element = GetTemplateChild("PART_PalyControl") as MediaElement;

            if (Element == null) return;

            var binding = new Binding
            {
                Source = this,
                Path = new PropertyPath(SourceProperty),
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            BindingOperations.SetBinding(Element, MediaElement.SourceProperty, binding);

            binding = new Binding
            {
                Source = this,
                Path = new PropertyPath(StretchProperty),
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            BindingOperations.SetBinding(Element, MediaElement.StretchProperty, binding);

            Element.LoadedBehavior = MediaState.Manual;
            Element.MediaEnded += ElementOnMediaEnded;
            Element.Play();
        }

        private static void ElementOnMediaEnded(object sender, RoutedEventArgs routedEventArgs)
        {
            var media = (MediaElement)sender;
            media.Position = TimeSpan.FromMilliseconds(1);
            media.Play();
        }
    }
}
