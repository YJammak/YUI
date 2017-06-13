using System;
using System.Dynamic;
using System.Reflection;

namespace YUI.YUtil
{
    /// <summary>
    /// 
    /// </summary>
    public class YStaticMembersDynamicWrapper : DynamicObject
    {
        private readonly Type _type;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public YStaticMembersDynamicWrapper(Type type) { _type = type; }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var prop = _type.GetProperty(binder.Name, BindingFlags.FlattenHierarchy | BindingFlags.Static | BindingFlags.Public);
            if (prop == null)
            {
                result = null;
                return false;
            }

            result = prop.GetValue(null, null);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="args"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var method = _type.GetMethod(binder.Name, BindingFlags.FlattenHierarchy | BindingFlags.Static | BindingFlags.Public);
            if (method == null)
            {
                result = null;
                return false;
            }

            result = method.Invoke(null, args);
            return true;
        }
    }
}
