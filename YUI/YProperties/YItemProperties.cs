using System.Windows;
using System.Windows.Media;

namespace YUI.YProperties
{
    public static partial class YAttachProperty
    {
        #region YItemSelectedForegroundProperty 被选中前景色
        /// <summary>
        /// 鼠标悬浮前景色附加属性
        /// </summary>
        public static readonly DependencyProperty YItemSelectedForegroundProperty = DependencyProperty.RegisterAttached(
            "YItemSelectedForeground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
        /// <summary>
        /// 设置被选中前景色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYItemSelectedForeground(DependencyObject element, Brush value)
        {
            element.SetValue(YItemSelectedForegroundProperty, value);
        }
        /// <summary>
        /// 获取被选中前景色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYItemSelectedForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(YItemSelectedForegroundProperty);
        }

        #endregion

        #region YItemSelectedBackgroundProperty 被选中背景色
        /// <summary>
        /// 鼠标悬浮前景色附加属性
        /// </summary>
        public static readonly DependencyProperty YItemSelectedBackgroundProperty = DependencyProperty.RegisterAttached(
            "YItemSelectedBackground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
        /// <summary>
        /// 设置被选中背景色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYItemSelectedBackground(DependencyObject element, Brush value)
        {
            element.SetValue(YItemSelectedBackgroundProperty, value);
        }
        /// <summary>
        /// 获取被选中背景色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYItemSelectedBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(YItemSelectedBackgroundProperty);
        }

        #endregion

        #region YItemCurrentForegroundProperty 被选中前景色
        /// <summary>
        /// 鼠标悬浮前景色附加属性
        /// </summary>
        public static readonly DependencyProperty YItemCurrentForegroundProperty = DependencyProperty.RegisterAttached(
            "YItemCurrentForeground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
        /// <summary>
        /// 设置被选中前景色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYItemCurrentForeground(DependencyObject element, Brush value)
        {
            element.SetValue(YItemCurrentForegroundProperty, value);
        }
        /// <summary>
        /// 获取被选中前景色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYItemCurrentForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(YItemCurrentForegroundProperty);
        }

        #endregion

        #region YItemCurrentBackgroundProperty 被选中背景色
        /// <summary>
        /// 鼠标悬浮前景色附加属性
        /// </summary>
        public static readonly DependencyProperty YItemCurrentBackgroundProperty = DependencyProperty.RegisterAttached(
            "YItemCurrentBackground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
        /// <summary>
        /// 设置被选中背景色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYItemCurrentBackground(DependencyObject element, Brush value)
        {
            element.SetValue(YItemCurrentBackgroundProperty, value);
        }
        /// <summary>
        /// 获取被选中背景色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYItemCurrentBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(YItemCurrentBackgroundProperty);
        }

        #endregion

        #region YItemSelectedUnActivedForegroundProperty 被选中未激活前景色
        /// <summary>
        /// 鼠标悬浮前景色附加属性
        /// </summary>
        public static readonly DependencyProperty YItemSelectedUnActivedForegroundProperty = DependencyProperty.RegisterAttached(
            "YItemSelectedUnActivedForeground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
        /// <summary>
        /// 设置被选中未激活前景色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYItemSelectedUnActivedForeground(DependencyObject element, Brush value)
        {
            element.SetValue(YItemSelectedUnActivedForegroundProperty, value);
        }
        /// <summary>
        /// 获取被选中未激活前景色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYItemSelectedUnActivedForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(YItemSelectedUnActivedForegroundProperty);
        }

        #endregion

        #region YItemSelectedUnActivedBackgroundProperty 被选中未激活背景色
        /// <summary>
        /// 鼠标悬浮前景色附加属性
        /// </summary>
        public static readonly DependencyProperty YItemSelectedUnActivedBackgroundProperty = DependencyProperty.RegisterAttached(
            "YItemSelectedUnActivedBackground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
        /// <summary>
        /// 设置被选中未激活背景色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYItemSelectedUnActivedBackground(DependencyObject element, Brush value)
        {
            element.SetValue(YItemSelectedUnActivedBackgroundProperty, value);
        }
        /// <summary>
        /// 获取被选中未激活背景色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYItemSelectedUnActivedBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(YItemSelectedUnActivedBackgroundProperty);
        }

        #endregion

        #region YItemMouseOverForegroundProperty 子选项鼠标悬浮前景色
        /// <summary>
        /// 子选项鼠标悬浮前景色附加属性
        /// </summary>
        public static readonly DependencyProperty YItemMouseOverForegroundProperty = DependencyProperty.RegisterAttached(
            "YItemMouseOverForeground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
        /// <summary>
        /// 设置子选项鼠标悬浮前景色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYItemMouseOverForeground(DependencyObject element, Brush value)
        {
            element.SetValue(YItemMouseOverForegroundProperty, value);
        }
        /// <summary>
        /// 获取子选项鼠标悬浮前景色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYItemMouseOverForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(YItemMouseOverForegroundProperty);
        }

        #endregion

        #region YItemMouseOverBackgroundProperty 子选项鼠标悬浮背景色
        /// <summary>
        /// 子选项鼠标悬浮背景色附加属性
        /// </summary>
        public static readonly DependencyProperty YItemMouseOverBackgroundProperty = DependencyProperty.RegisterAttached(
            "YItemMouseOverBackground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
        /// <summary>
        /// 设置子选项鼠标悬浮背景色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYItemMouseOverBackground(DependencyObject element, Brush value)
        {
            element.SetValue(YItemMouseOverBackgroundProperty, value);
        }
        /// <summary>
        /// 获取子选项鼠标悬浮背景色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYItemMouseOverBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(YItemMouseOverBackgroundProperty);
        }

        #endregion
    }
}
