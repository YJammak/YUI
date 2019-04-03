using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
// ReSharper disable InconsistentNaming

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
        public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register("TextWrapping", typeof(bool), 
            typeof(YTextBlock), new PropertyMetadata(default(bool), (o, args) => (o as YTextBlock)?.InvalidateVisual()));

        /// <summary>
        /// 
        /// </summary>
        public bool TextWrapping
        {
            get => (bool) GetValue(TextWrappingProperty);
            set => SetValue(TextWrappingProperty, value);
        }

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

        private List<YFormattedTextInfo> FormattedTexts { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public YTextBlock()
        {
            VerticalAlignment = VerticalAlignment.Stretch;
            HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size constraint)
        {
            MeasureSize = CreateText();

            var width = Math.Max(MinWidth, Math.Min(MeasureSize.Width, Math.Min(double.IsNaN(Width) ? constraint.Width : Width, MaxWidth)));
            var height = Math.Max(MinHeight, Math.Min(MeasureSize.Height, Math.Min(double.IsNaN(Height) ? constraint.Height : Height, MaxHeight)));

            return new Size(width, height);
        }

        private Size MeasureSize { get; set; }
        /// <inheritdoc />
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            if (!TextWrapping)
                return base.ArrangeOverride(arrangeBounds);

            if (Orientation == Orientation.Horizontal && arrangeBounds.Width >= MeasureSize.Width)
                return base.ArrangeOverride(arrangeBounds);

            if (Orientation == Orientation.Vertical && arrangeBounds.Height >= MeasureSize.Height)
                return base.ArrangeOverride(arrangeBounds);

            return Arrange(arrangeBounds);
        }

        private Size Arrange(Size arrangeBounds)
        {
            return GetArrangeText(Text ?? "", arrangeBounds);
        }

        private Size GetArrangeText(string str, Size arrangeBounds)
        {
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    return GetHorizontalArrangeText(str, arrangeBounds);
                case Orientation.Vertical:
                    return GetVerticalArrangeText(str, arrangeBounds);
                default:
                    return new Size();
            }
        }

        private Size GetHorizontalArrangeText(string str, Size arrangeBounds)
        {
            var lines = str.Split(new[] { "\n", "\r", "\r\n" }, StringSplitOptions.None);

            var width = 0.0;
            var height = 0.0;
            var y = 0.0;
            var currentLineNumber = 0;

            FormattedTexts = new List<YFormattedTextInfo>();

            foreach (var currentLine in lines)
            {
                var lineWidth = 0.0;
                var currentLineIndex = 0;

                for (var j = 0; j < currentLine.Length; j++)
                {
                    var character = currentLine[j].ToString();

                    var formattedText = new FormattedText(character, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                        new Typeface(FontFamily, FontStyle, FontWeight, FontStretch), FontSize, Brushes.Black);

                    if (currentLineIndex == 0 && currentLineNumber != 0)
                        y = height + LineSpacing;

                    var x = currentLineIndex == 0 ? 0.0 : lineWidth + CharacterSpacing;

                    lineWidth = x + formattedText.Width;
                    var tempWidth = Math.Max(width, lineWidth);

                    if (tempWidth + Padding.Left + Padding.Right > arrangeBounds.Width)
                    {
                        if (currentLineIndex == 0)
                        {
                            currentLineNumber++;
                        }
                        else
                        {
                            currentLineIndex = 0;
                            currentLineNumber++;
                            j--;
                            continue;
                        }
                    }

                    width = tempWidth;
                    height = Math.Max(height, formattedText.Height + y);

                    var point = new Point(x, y);

                    var formattedInfo = new YFormattedTextInfo
                    {
                        FormattedText = formattedText,
                        Point = point,
                        Text = character,
                    };

                    FormattedTexts.Add(formattedInfo);

                    currentLineIndex++;
                }

                currentLineNumber++;
            }

            return new Size(arrangeBounds.Width, height + Padding.Top + Padding.Bottom);
        }

        private Size GetVerticalArrangeText(string str, Size arrangeBounds)
        {
            var lines = str.Split(new[] { "\n", "\r", "\r\n" }, StringSplitOptions.None);

            var width = 0.0;
            var height = 0.0;
            var x = 0.0;
            var currentLineNumber = 0;

            FormattedTexts = new List<YFormattedTextInfo>();

            foreach (var currentLine in lines)
            {
                var lineHeight = 0.0;
                var currentLineIndex = 0;

                for (var j = 0; j < currentLine.Length; j++)
                {
                    var character = currentLine[j].ToString();

                    var formattedText = new FormattedText(character, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                        new Typeface(FontFamily, FontStyle, FontWeight, FontStretch), FontSize, Brushes.Black);

                    if (currentLineIndex == 0 && currentLineNumber != 0)
                        x = width + LineSpacing;

                    var y = currentLineIndex == 0 ? 0.0 : lineHeight + CharacterSpacing;

                    lineHeight = y + formattedText.Height;

                    var tempHeight = Math.Max(height, lineHeight);

                    if (tempHeight + Padding.Top + Padding.Bottom > arrangeBounds.Height)
                    {
                        if (currentLineIndex == 0)
                        {
                            currentLineNumber++;
                        }
                        else
                        {
                            currentLineIndex = 0;
                            currentLineNumber++;
                            j--;
                            continue;
                        }
                    }

                    height = tempHeight;
                    width = Math.Max(width, formattedText.Width + x);

                    var point = new Point(x, y);

                    var formattedInfo = new YFormattedTextInfo
                    {
                        FormattedText = formattedText,
                        Point = point,
                        Text = character,
                    };

                    FormattedTexts.Add(formattedInfo);

                    currentLineIndex++;
                }

                currentLineNumber++;
            }

            return new Size(width + Padding.Left + Padding.Right, arrangeBounds.Height);
        }

        /// <summary>
        /// Create the outline geometry based on the formatted text.
        /// </summary>
        private Size CreateText()
        {
            return GetFormattedText(Text ?? "");
        }

        private Size GetFormattedText(string str)
        {
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    return GetHorizontalFormattedText(str);
                case Orientation.Vertical:
                    return GetVerticalFormattedText(str);
                default:
                    return new Size();
            }
        }

        private Size GetVerticalFormattedText(string str)
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
                        x = width + LineSpacing;

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
            
            return new Size(width + Padding.Left + Padding.Right, height + Padding.Top + Padding.Bottom);
        }

        private Size GetHorizontalFormattedText(string str)
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
                        y = height + LineSpacing;

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

            return new Size(width + Padding.Left + Padding.Right, height + Padding.Top + Padding.Bottom);
        }

        /// <inheritdoc />
        /// <summary>
        /// OnRender override draws the geometry of the text and optional highlight.
        /// </summary>
        /// <param name="drawingContext">Drawing context of the OutlineText control.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            foreach (var info in FormattedTexts)
            {
                var point = new Point(Padding.Left + info.Point.X, Padding.Top + info.Point.Y);
                drawingContext.DrawText(info.FormattedText, point);
                drawingContext.DrawGeometry(Foreground, new Pen(Stroke, StrokeThickness), info.FormattedText.BuildGeometry(point));
            }
        }
    }
}
