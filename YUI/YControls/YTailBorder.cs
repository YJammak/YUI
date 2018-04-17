using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using YUI.YUtil;

namespace YUI.YControls
{
    /// <summary>
    /// 
    /// </summary>
    public enum Placement
    {
        /// <summary>
        /// 左上
        /// </summary>
        LeftTop,
        /// <summary>
        /// 左中
        /// </summary>
        LeftBottom,
        /// <summary>
        /// 左下
        /// </summary>
        LeftCenter,
        /// <summary>
        /// 右上
        /// </summary>
        RightTop,
        /// <summary>
        /// 右下
        /// </summary>
        RightBottom,
        /// <summary>
        /// 右中
        /// </summary>
        RightCenter,
        /// <summary>
        /// 上左
        /// </summary>
        TopLeft,
        /// <summary>
        /// 上中
        /// </summary>
        TopCenter,
        /// <summary>
        /// 上右
        /// </summary>
        TopRight,
        /// <summary>
        /// 下左
        /// </summary>
        BottomLeft,
        /// <summary>
        /// 下中
        /// </summary>
        BottomCenter,
        /// <summary>
        /// 下右
        /// </summary>
        BottomRight,
    }

    /// <inheritdoc />
    /// <summary>
    /// 带三角的Border
    /// </summary>
    public sealed class YTailBorder : Decorator
    {
        #region 依赖属性

        /// <summary>
        /// 三角位置
        /// </summary>
        public static readonly DependencyProperty PlacementProperty =
            DependencyProperty.Register("Placement", typeof(Placement), typeof(YTailBorder),
            new FrameworkPropertyMetadata(Placement.RightCenter, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsParentArrange));

        /// <summary>
        /// 三角位置
        /// </summary>
        public Placement Placement
        {
            get => (Placement)GetValue(PlacementProperty);
            set => SetValue(PlacementProperty, value);
        }

        /// <summary>
        /// 尾巴的宽度
        /// </summary>
        public static readonly DependencyProperty TailWidthProperty =
            DependencyProperty.Register("TailWidth", typeof(double), typeof(YTailBorder), new PropertyMetadata(10d));

        /// <summary>
        /// 尾巴的宽度，默认值为7
        /// </summary>
        public double TailWidth
        {
            get => (double)GetValue(TailWidthProperty);
            set => SetValue(TailWidthProperty, value);
        }

        /// <summary>
        /// 尾巴的高度
        /// </summary>
        public static readonly DependencyProperty TailHeightProperty =
            DependencyProperty.Register("TailHeight", typeof(double), typeof(YTailBorder), new PropertyMetadata(10d));

        /// <summary>
        /// 尾巴的高度，默认值为10
        /// </summary>
        public double TailHeight
        {
            get => (double)GetValue(TailHeightProperty);
            set => SetValue(TailHeightProperty, value);
        }

        /// <summary>
        /// 巴距离顶部的距离
        /// </summary>
        public static readonly DependencyProperty TailVerticalOffsetProperty =
            DependencyProperty.Register("TailVerticalOffset", typeof(double), typeof(YTailBorder), new PropertyMetadata(13d));
        
        /// <summary>
        /// 尾巴距离顶部的距离，默认值为10
        /// </summary>
        public double TailVerticalOffset
        {
            get => (double)GetValue(TailVerticalOffsetProperty);
            set => SetValue(TailVerticalOffsetProperty, value);
        }

        /// <summary>
        /// 尾巴距离右边缘的距离
        /// </summary>
        public static readonly DependencyProperty TailHorizontalOffsetProperty =
            DependencyProperty.Register("TailHorizontalOffset", typeof(double), typeof(YTailBorder),
                new PropertyMetadata(12d));

        /// <summary>
        /// 尾巴距离右边缘的距离，默认值为10
        /// </summary>
        public double TailHorizontalOffset
        {
            get => (double)GetValue(TailHorizontalOffsetProperty);
            set => SetValue(TailHorizontalOffsetProperty, value);
        }

        /// <summary>
        /// 背景色
        /// </summary>
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(YTailBorder)
                , new PropertyMetadata(new SolidColorBrush(Color.FromRgb(255, 255, 255))));

        /// <summary>
        /// 背景色，默认值为#FFFFFF，白色
        /// </summary>
        public Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        /// <summary>
        /// 内边距
        /// </summary>
        public static readonly DependencyProperty PaddingProperty =
            DependencyProperty.Register("Padding", typeof(Thickness), typeof(YTailBorder)
                , new PropertyMetadata(new Thickness(10, 5, 10, 5)));

        /// <summary>
        /// 内边距
        /// </summary>
        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        public static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(YTailBorder)
                , new PropertyMetadata(default(Brush)));

        /// <summary>
        /// 边框颜色
        /// </summary>
        public Brush BorderBrush
        {
            get => (Brush)GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }

        /// <summary>
        /// 边框厚度
        /// </summary>
        public static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(YTailBorder), new PropertyMetadata(new Thickness(1d)));
        
        /// <summary>
        /// 边框厚度
        /// </summary>
        public Thickness BorderThickness
        {
            get => (Thickness)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        /// <summary>
        /// 边框圆角
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius)
                , typeof(YTailBorder), new PropertyMetadata(new CornerRadius(0)));

        /// <summary>
        /// 边框圆角
        /// </summary>
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        #endregion

        #region 方法重写
        /// <summary>
        /// 该方法用于测量整个控件的大小
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns>控件的大小</returns>
        protected override Size MeasureOverride(Size constraint)
        {
            var padding = Padding;

            var result = new Size();
            if (Child == null) return result;

            //测量子控件的大小
            Child.Measure(constraint);

            //三角形在左边与右边的，整个容器的宽度则为：里面子控件的宽度 + 设置的padding + 三角形的宽度
            //三角形在上面与下面的，整个容器的高度则为：里面子控件的高度 + 设置的padding + 三角形的高度
            switch (Placement)
            {
                case Placement.LeftTop:
                case Placement.LeftBottom:
                case Placement.LeftCenter:
                case Placement.RightTop:
                case Placement.RightBottom:
                case Placement.RightCenter:
                    result.Width = Child.DesiredSize.Width + padding.Left + padding.Right + TailWidth;
                    result.Height = Child.DesiredSize.Height + padding.Top + padding.Bottom;
                    break;
                case Placement.TopLeft:
                case Placement.TopCenter:
                case Placement.TopRight:
                case Placement.BottomLeft:
                case Placement.BottomCenter:
                case Placement.BottomRight:
                    result.Width = Child.DesiredSize.Width + padding.Left + padding.Right;
                    result.Height = Child.DesiredSize.Height + padding.Top + padding.Bottom + TailHeight;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 设置子控件的大小与位置
        /// </summary>
        /// <param name="arrangeSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var padding = Padding;
            if (Child == null) return arrangeSize;

            switch (Placement)
            {
                case Placement.LeftTop:
                case Placement.LeftBottom:
                case Placement.LeftCenter:
                    Child.Arrange(new Rect(new Point(padding.Left + TailWidth, padding.Top), Child.DesiredSize));
                    break;
                case Placement.RightTop:
                case Placement.RightBottom:
                case Placement.RightCenter:
                    Child.Arrange(new Rect(new Point(padding.Left, padding.Top), Child.DesiredSize));
                    break;
                case Placement.TopLeft:
                case Placement.TopRight:
                case Placement.TopCenter:
                    Child.Arrange(new Rect(new Point(padding.Left, TailHeight + padding.Top), Child.DesiredSize));
                    break;
                case Placement.BottomLeft:
                case Placement.BottomRight:
                case Placement.BottomCenter:
                    Child.Arrange(new Rect(new Point(padding.Left, padding.Top), Child.DesiredSize));
                    break;
            }
            return arrangeSize;
        }

        /// <summary>
        /// 绘制控件
        /// </summary>
        /// <param name="drawingContext"></param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (Child == null) return;

            Geometry cg = null;
            var brush = Background;
            //DpiScale dpi = base.getd();
            var pen = new Pen
            {
                Brush = BorderBrush,
                Thickness = RoundLayoutValue(BorderThickness.Left, YSystemHelper.GetDpiInfo().DpiXScale)
            };

            //pen.Thickness = BorderThickness * 0.5;

            switch (Placement)
            {
                case Placement.LeftTop:
                case Placement.LeftBottom:
                case Placement.LeftCenter:
                    //生成小尾巴在左侧的图形和底色
                    cg = CreateGeometryTailAtLeft();
                    break;
                case Placement.RightTop:
                case Placement.RightCenter:
                case Placement.RightBottom:
                    //生成小尾巴在右侧的图形和底色
                    cg = CreateGeometryTailAtRight();
                    break;
                case Placement.TopLeft:
                case Placement.TopCenter:
                case Placement.TopRight:
                    //生成小尾巴在右侧的图形和底色
                    cg = CreateGeometryTailAtTop();
                    break;
                case Placement.BottomLeft:
                case Placement.BottomCenter:
                case Placement.BottomRight:
                    //生成小尾巴在右侧的图形和底色
                    cg = CreateGeometryTailAtBottom();
                    break;
            }
            var guideLines = new GuidelineSet();
            drawingContext.PushGuidelineSet(guideLines);
            drawingContext.DrawGeometry(brush, pen, cg);
        }
        #endregion

        private static double RoundLayoutValue(double value, double dpiScale)
        {
            double num;
            if (!AreClose(dpiScale, 1.0))
            {
                num = Math.Round(value * dpiScale) / dpiScale;
                if (double.IsInfinity(num) || AreClose(num, 1.7976931348623157E+308))
                {
                    num = value;
                }
            }
            else
            {
                num = Math.Round(value);
            }
            return num;
        }

        private static bool AreClose(double value1, double value2)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (value1 == value2)
            {
                return true;
            }

            var num = (Math.Abs(value1) + Math.Abs(value2) + 10.0) * 2.2204460492503131E-16;
            var num2 = value1 - value2;
            return -num < num2 && num > num2;
        }

        #region 私有方法

        private Geometry CreateGeometryTailAtRight()
        {
            var result = new CombinedGeometry();

            var verticalOffset = TailVerticalOffset;
            //三角形默认居中
            switch (Placement)
            {
                case Placement.RightTop:
                    //不做任何处理
                    break;
                case Placement.LeftBottom:
                    verticalOffset = ActualHeight - TailHeight - TailVerticalOffset;
                    break;
                case Placement.LeftCenter:
                    verticalOffset = (ActualHeight - TailHeight) / 2;
                    break;
            }

            #region 绘制三角形
            var arcPoint1 = new Point(ActualWidth - TailWidth, verticalOffset);
            var arcPoint2 = new Point(ActualWidth, verticalOffset + TailHeight / 2);
            var arcPoint3 = new Point(ActualWidth - TailWidth, verticalOffset + TailHeight);

            var as1_2 = new LineSegment(arcPoint2, false);
            var as2_3 = new LineSegment(arcPoint3, false);

            var pf1 = new PathFigure
            {
                IsClosed = false,
                StartPoint = arcPoint1
            };
            pf1.Segments.Add(as1_2);
            pf1.Segments.Add(as2_3);

            var pg1 = new PathGeometry();
            pg1.Figures.Add(pf1);
            #endregion

            #region 绘制矩形边框
            var rg2 = new RectangleGeometry(new Rect(0, 0, ActualWidth - TailWidth, ActualHeight)
                , CornerRadius.TopLeft, CornerRadius.BottomRight, new TranslateTransform(0.5, 0.5));
            #endregion

            #region 合并两个图形
            result.Geometry1 = pg1;
            result.Geometry2 = rg2;
            result.GeometryCombineMode = GeometryCombineMode.Union;
            #endregion

            return result;
        }

        private Geometry CreateGeometryTailAtLeft()
        {
            var result = new CombinedGeometry();

            var verticalOffset = TailVerticalOffset;
            switch (Placement)
            {
                case Placement.LeftTop:
                    //不做任何处理
                    break;
                case Placement.LeftBottom:
                    verticalOffset = ActualHeight - TailHeight - TailVerticalOffset;
                    break;
                case Placement.LeftCenter:
                    verticalOffset = (ActualHeight - TailHeight) / 2;
                    break;
            }

            #region 绘制三角形
            var arcPoint1 = new Point(TailWidth, verticalOffset);
            var arcPoint2 = new Point(0, verticalOffset + TailHeight / 2);
            var arcPoint3 = new Point(TailWidth, verticalOffset + TailHeight);

            var as1_2 = new LineSegment(arcPoint2, false);
            var as2_3 = new LineSegment(arcPoint3, false);

            var pf = new PathFigure
            {
                IsClosed = false,
                StartPoint = arcPoint1
            };
            pf.Segments.Add(as1_2);
            pf.Segments.Add(as2_3);

            var g1 = new PathGeometry();
            g1.Figures.Add(pf);
            #endregion

            #region 绘制矩形边框
            var g2 = new RectangleGeometry(new Rect(TailWidth, 0, ActualWidth - TailWidth, ActualHeight)
                , CornerRadius.TopLeft, CornerRadius.BottomRight);
            #endregion

            #region 合并两个图形
            result.Geometry1 = g1;
            result.Geometry2 = g2;
            result.GeometryCombineMode = GeometryCombineMode.Union;
            #endregion

            return result;
        }

        private Geometry CreateGeometryTailAtTop()
        {
            var result = new CombinedGeometry();

            var horizontalOffset = TailHorizontalOffset;
            switch (Placement)
            {
                case Placement.TopLeft:
                    break;
                case Placement.TopCenter:
                    horizontalOffset = (ActualWidth - TailWidth) / 2;
                    break;
                case Placement.TopRight:
                    horizontalOffset = ActualWidth - TailWidth - TailHorizontalOffset;
                    break;
            }

            #region 绘制三角形
            var anglePoint1 = new Point(horizontalOffset, TailHeight);
            var anglePoint2 = new Point(horizontalOffset + (TailWidth / 2), 0);
            var anglePoint3 = new Point(horizontalOffset + TailWidth, TailHeight);

            var as1_2 = new LineSegment(anglePoint2, true);
            var as2_3 = new LineSegment(anglePoint3, true);

            var pf = new PathFigure
            {
                IsClosed = false,
                StartPoint = anglePoint1
            };
            pf.Segments.Add(as1_2);
            pf.Segments.Add(as2_3);

            var g1 = new PathGeometry();
            g1.Figures.Add(pf);
            #endregion

            #region 绘制矩形边框
            var g2 = new RectangleGeometry(new Rect(0, TailHeight, ActualWidth, ActualHeight - TailHeight)
                , CornerRadius.TopLeft, CornerRadius.BottomRight);
            #endregion

            #region 合并
            result.Geometry1 = g1;
            result.Geometry2 = g2;
            result.GeometryCombineMode = GeometryCombineMode.Union;
            #endregion

            return result;
        }

        private Geometry CreateGeometryTailAtBottom()
        {
            var result = new CombinedGeometry();

            var horizontalOffset = TailHorizontalOffset;
            switch (Placement)
            {
                case Placement.BottomLeft:
                    break;
                case Placement.BottomCenter:
                    horizontalOffset = (ActualWidth - TailWidth) / 2;
                    break;
                case Placement.BottomRight:
                    horizontalOffset = ActualWidth - TailWidth - TailHorizontalOffset;
                    break;
            }


            #region 绘制三角形
            var anglePoint1 = new Point(horizontalOffset, ActualHeight - TailHeight);
            var anglePoint2 = new Point(horizontalOffset + TailWidth / 2, ActualHeight);
            var anglePoint3 = new Point(horizontalOffset + TailWidth, ActualHeight - TailHeight);

            var as1_2 = new LineSegment(anglePoint2, true);
            var as2_3 = new LineSegment(anglePoint3, true);

            var pf = new PathFigure
            {
                IsClosed = false,
                StartPoint = anglePoint1
            };
            pf.Segments.Add(as1_2);
            pf.Segments.Add(as2_3);

            var g1 = new PathGeometry();
            g1.Figures.Add(pf);
            #endregion

            #region 绘制矩形边框
            var g2 = new RectangleGeometry(new Rect(0, 0, ActualWidth, ActualHeight - TailHeight)
                , CornerRadius.TopLeft, CornerRadius.BottomRight);
            #endregion

            #region 合并
            result.Geometry1 = g1;
            result.Geometry2 = g2;
            result.GeometryCombineMode = GeometryCombineMode.Union;
            #endregion

            return result;
        }
        #endregion
    }
}
