using System.Collections.Generic;

namespace YUI.WPF.YControls
{
    /// <summary>
    /// 自动完成控件关键词接口
    /// </summary>
    public interface IYAutoCompleteBoxKeys
    {
        /// <summary>
        /// 关键词类
        /// </summary>
        IEnumerable<string> Keywords { get; set; }
    }
}
