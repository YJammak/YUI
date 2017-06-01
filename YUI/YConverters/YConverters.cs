using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YUI.YUtil;

namespace YUI.YConverters
{

    /// <summary>
    /// 
    /// </summary>
    public sealed class YConverters
    {
        /// <summary>
        /// bool反转
        /// </summary>
        public static YBooleanReverseConverter YBooleanReverseConverter => YSingleton<YBooleanReverseConverter>.GetInstance();
    }
}
