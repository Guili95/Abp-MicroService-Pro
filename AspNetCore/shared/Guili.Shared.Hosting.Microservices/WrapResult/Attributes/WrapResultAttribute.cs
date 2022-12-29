using System;

namespace Guili.Shared.Hosting.Microservices.WrapResult.Attributes
{
    /// <summary>
    /// 用于确定ABP应如何在web层包装响应。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public class WrapResultAttribute : Attribute
    {
        /// <summary>
        /// 成功后包装结果
        /// </summary>
        public bool WrapOnSuccess { get; set; }

        /// <summary>
        /// 错误时包装结果
        /// </summary>
        public bool WrapOnError { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="wrapOnSuccess">成功后包装结果</param>
        /// <param name="wrapOnError">错误时包装结果</param>
        public WrapResultAttribute(bool wrapOnSuccess = true, bool wrapOnError = true)
        {
            WrapOnSuccess = wrapOnSuccess;
            WrapOnError = wrapOnError;
        }
    }
}
