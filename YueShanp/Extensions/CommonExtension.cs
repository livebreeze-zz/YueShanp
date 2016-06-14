using System;

namespace YueShanp
{
    public static class CommonExtension
    {
        /// <summary>
        /// 取得 ROC 年
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public static string ROCYear(this DateTime now)
        {
            return (now.Year - 1911).ToString();
        }
    }
}