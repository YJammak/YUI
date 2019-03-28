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
        public static readonly DependencyProperty LineSpacingProperty = DependencyProperty.Register("LineSpacing", typeof(double), 
            typeof(YTextBlock), new PropertyMetadata(default(double), (o, args) => (o as YTextBlock)?.InvalidateVisual()));

        /// <summary>
        /// 
        /// </summary>
        public double LineSpacing
        {
            get => (double) GetValue(LineSpacingProperty);
            set => SetValue(LineSpacingProperty, value);
        }

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
            var lines = str.Split(new[] { "\n", "\r", "\r\n" }, StringSplitOptions.None);

            var width = 0.0;
            var height = 0.0;
            var x = 0.0;

            FormattedTexts = new List<YFormattedTextInfo>();

            for (var i = 0; i < lines.Length; i++)
            {
                var lineHeight = 0.0;
                var currentLine = lines[i];

                for (var j = 0; j < currentLine.Length; j++)
                {
                    var character = currentLine[j].ToString();

                    var formattedText = new FormattedText(character, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                        new Typeface(FontFamily, FontStyle, FontWeight, FontStretch), FontSize, Brushes.Black);

                    if (j == 0 && i != 0)
                    {
                        x = width;
                        if (i > 0)
                            x += LineSpacing;
                    }

                    var y = j == 0 ? 0.0 : lineHeight + CharacterSpacing;

                    lineHeight = y + formattedText.Height;
                    height = Math.Max(height, lineHeight);
                    width = Math.Max(width, formattedText.Width + x);

                    var point = new Point(x, y);

                    var formattedInfo = new YFormattedTextInfo
                    {
                        FormattedText = formattedText,
                        Point = point,
                        Text = character,
                    };

                    FormattedTexts.Add(formattedInfo);
                }
            }

            Width = width + Padding.Left + Padding.Right;
            Height = height + Padding.Top + Padding.Bottom;
        }

        private void GetHorizontalFormattedText(string str)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (CharacterSpacing == 0.0 || str == "")
            {
                FormattedText = new FormattedText(str, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                    new Typeface(FontFamily, FontStyle, FontWeight, FontStretch), FontSize, Brushes.Black);

                Width = FormattedText.Width + Padding.Left + Padding.Right;
                Height = FormattedText.Height + Padding.Top + Padding.Bottom;
            }
            else
            {
                var lines = str.Split(new[] {"\n", "\r", "\r\n"}, StringSplitOptions.None);

                var width = 0.0;
                var height = 0.0;
                var y = 0.0;

                FormattedTexts = new List<YFormattedTextInfo>();

                for (var i = 0; i < lines.Length; i++)
                {
                    var lineWidth = 0.0;
                    var currentLine = lines[i];

                    for (var j = 0; j < currentLine.Length; j++)
                    {
                        var character = currentLine[j].ToString();

                        var formattedText = new FormattedText(character, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                            new Typeface(FontFamily, FontStyle, FontWeight, FontStretch), FontSize, Brushes.Black);

                        if (j == 0 && i != 0)
                        {
                            y = height;
                            if (i > 0)
                                y += LineSpacing;
                        }

                        var x = j == 0 ? 0.0 : lineWidth + CharacterSpacing;

                        lineWidth = x + formattedText.Width;
                        width = Math.Max(width, lineWidth);
                        height = Math.Max(height, formattedText.Height + y);

                        var point = new Point(x, y);

                        var formattedInfo = new YFormattedTextInfo
                        {
                            FormattedText = formattedText,
                            Point = point,
                            Text = character,
                        };

                        FormattedTexts.Add(formattedInfo);
                    }
                }

                Width = width + Padding.Left + Padding.Right;
                Height = height + Padding.Top + Padding.Bottom;
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
                var point = new Point(Padding.Left, Padding.Top);
                drawingContext.DrawText(FormattedText, point);
                drawingContext.DrawGeometry(Foreground, new Pen(Stroke, StrokeThickness), FormattedText.BuildGeometry(point));
            }
            else
            {
                foreach (var info in FormattedTexts)
                {
                    var point = new Point(Padding.Left + info.Point.X, Padding.Top + info.Point.Y);
                    drawingContext.DrawText(info.FormattedText, point);
                    drawingContext.DrawGeometry(Foreground, new Pen(Stroke, StrokeThickness), info.FormattedText.BuildGeometry(point));
                }
            }
        }
    }
}
