using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace YUI.WPF.YControls
{
    internal class YFormattedTextInfo
    {
        public FormattedText FormattedText { get; set; }

        public string Text { get; set; }

        public Point Point { get; set; }

        public Geometry TextGeometry { get; set; }
    }

    /// <inheritdoc />
    /// <summary>
    /// 文字带描边
    /// </summary>
    public class YTextBlock : Control
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string),
            typeof(YTextBlock), new PropertyMetadata(string.Empty, (o, args) => (o as YTextBlock)?.InvalidateVisual()));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register("Strok", typeof(Brush),
            typeof(YTextBlock), new PropertyMetadata(Brushes.Black, (o, args) => (o as YTextBlock)?.InvalidateVisual()));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register("StrokeThickness", typeof(double),
            typeof(YTextBlock), new PropertyMetadata(default(double), (o, args) => (o as YTextBlock)?.InvalidateVisual()));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty CharacterSpacingProperty = DependencyProperty.Register("CharacterSpacing", typeof(double), 
            typeof(YTextBlock), new PropertyMetadata(default(double), (o, args) => (o as YTextBlock)?.InvalidateVisual()));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), 
            typeof(YTextBlock), new PropertyMetadata(default(Orientation), (o, args) => (o as YTextBlock)?.InvalidateVisual()));

        /// <summary>
        /// 
        /// </summary>
        public Orientation Orientation
        {
            get => (Orientation) GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        /// <summary>
        /// 字间距
        /// </summary>
        public double CharacterSpacing
        {
            get => (double) GetValue(CharacterSpacingProperty);
            set => SetValue(CharacterSpacingProperty, value);
        }

        /// <summary>
        /// 字符串的格式化几何对象
        /// </summary>
        public Geometry TextGeometry { get; private set; }

        /// <summary>
        /// 边缘画刷
        /// </summary>
        public Brush Stroke
        {
            get => GetValue(StrokeProperty) as Brush;
            set => SetValue(StrokeProperty, value);
        }

        /// <summary>
        /// 边缘宽度
        /// </summary>
        public double StrokeThickness
        {
            get => (double)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        /// <summary>
        /// 显示的文字
        /// </summary>
        public string Text
        {
            get => GetValue(TextProperty) as string;
            set => SetValue(TextProperty, value);
        }

        private FormattedText FormattedText { get; set; }

        private List<YFormattedTextInfo> FormattedTexts { get; set; }

        /// <summary>
        /// Create the outline geometry based on the formatted text.
        /// </summary>
        public void CreateText()
        {
            GetFormattedText(Text ?? "");
        }

        private void GetFormattedText(string str)
        {
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    GetHorizontalFormattedText(str);
                    break;
                case Orientation.Vertical:
                    GetVerticalFormattedText(str);
                    break;
            }
        }

        private void GetVerticalFormattedText(string str)
        {
            var width = 0.0;
            var height = 0.0;

            FormattedTexts = new List<YFormattedTextInfo>();

            for (var i = 0; i < str.Length; i++)
            {
                var character = str[i].ToString();

                var formattedText = new FormattedText(character, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                    new Typeface(FontFamily, FontStyle, FontWeight, FontStretch), FontSize, Brushes.Black);

                var y = i == 0 ? 0.0 : height + CharacterSpacing;

                var point = new Point(0, y);

                height = y + formattedText.Height;
                width = Math.Max(width, formattedText.Width);

                var formattedInfo = new YFormattedTextInfo
                {
                    FormattedText = formattedText,
                    Point = point,
                    Text = character,
                    TextGeometry = formattedText.BuildGeometry(new Point(0, y))
                };

                FormattedTexts.Add(formattedInfo);
            }

            Width = width;
            Height = height;
        }

        private void GetHorizontalFormattedText(string str)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (CharacterSpacing == 0.0 || str == "")
            {
                FormattedText = new FormattedText(str, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                    new Typeface(FontFamily, FontStyle, FontWeight, FontStretch), FontSize, Brushes.Black);

                Width = FormattedText.Width;
                Height = FormattedText.Height;

                TextGeometry = FormattedText.BuildGeometry(new Point(0, 0));
            }
            else
            {
                var width = 0.0;
                var height = 0.0;

                FormattedTexts = new List<YFormattedTextInfo>();

                for (var i = 0; i < str.Length; i++)
                {
                    var character = str[i].ToString();

                    var formattedText = new FormattedText(character, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                        new Typeface(FontFamily, FontStyle, FontWeight, FontStretch), FontSize, Brushes.Black);

                    var x = i == 0 ? 0.0 : width + CharacterSpacing;

                    var point = new Point(x, 0);

                    width = x + formattedText.Width;
                    height = Math.Max(height, formattedText.Height);

                    var formattedInfo = new YFormattedTextInfo
                    {
                        FormattedText = formattedText,
                        Point = point,
                        Text = character,
                        TextGeometry = formattedText.BuildGeometry(new Point(x, 0))
                    };

                    FormattedTexts.Add(formattedInfo);
                }

                Width = width;
                Height = height;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// OnRender override draws the geometry of the text and optional highlight.
        /// </summary>
        /// <param name="drawingContext">Drawing context of the OutlineText control.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            CreateText();

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (CharacterSpacing == 0.0 && Orientation == Orientation.Horizontal)
            {
                drawingContext.DrawText(FormattedText, new Point(0, 0));
                drawingContext.DrawGeometry(Foreground, new Pen(Stroke, StrokeThickness), TextGeometry);
            }
            else
            {
                foreach (var info in FormattedTexts)
                {
                    drawingContext.DrawText(info.FormattedText, info.Point);
                    drawingContext.DrawGeometry(Foreground, new Pen(Stroke, StrokeThickness), info.TextGeometry);
                }
            }
        }
    }
}
