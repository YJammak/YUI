using YUI.WPF.YUtil;

namespace YUI.WPF.YConverters
{

    /// <summary>
    /// 转换类
    /// </summary>
    public sealed class YConverters
    {
        /// <summary>
        /// bool反转
        /// </summary>
        public static YBooleanReverseConverter YBooleanReverseConverter => YSingleton<YBooleanReverseConverter>.GetInstance();

        /// <summary>
        /// bool转显示
        /// </summary>
        public static YBooleanToVisibilityConverter YBooleanToVisibilityConverter =>
            YSingleton<YBooleanToVisibilityConverter>.GetInstance();

        /// <summary>
        /// bool转显示反向
        /// </summary>
        public static YBooleanToVisibilityReverseConverter YBooleanToVisibilityReverseConverter =>
            YSingleton<YBooleanToVisibilityReverseConverter>.GetInstance();

        /// <summary>
        /// 字符串转显示（为空或nulll隐藏）
        /// </summary>
        public static YStringToVisibilityConverter YStringToVisibilityConverter =>
            YSingleton<YStringToVisibilityConverter>.GetInstance();

        /// <summary>
        /// 对象转显示（为null隐藏）
        /// </summary>
        public static YObjectToVisibilityConverter YObjectToVisibilityConverter =>
            YSingleton<YObjectToVisibilityConverter>.GetInstance();
    }
}
