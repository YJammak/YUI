using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUI.YUtil
{
    /// <summary>
    /// MD5辅助类
    /// </summary>
    public static class YMD5Helper
    {
        /// <summary>
        /// 获取文件MD5字符串
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        public static string GetFileMD5(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"{filePath}不存在");

            try
            {
                var file = new FileStream(filePath, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                var retVal = md5.ComputeHash(file);
                file.Close();

                return BitConverter.ToString(retVal).Replace("-", "");
            }
            catch (Exception ex)
            {
                throw new Exception("获取文件MD5失败,error:" + ex.Message);
            }
        }
    }
}
