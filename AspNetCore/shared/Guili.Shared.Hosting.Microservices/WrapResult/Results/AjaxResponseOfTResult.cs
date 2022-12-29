using System;
using Volo.Abp.Http;

namespace Guili.Shared.Hosting.Microservices.WrapResult.Results
{
    /// <summary>
    /// AJAX 请求 响应结果
    /// </summary>
    [Serializable]
    public class AjaxResponse<TResult> : AjaxResponseBase
    {
        /// <summary>
        /// AJAX请求的实际结果对象。
        /// 如果为true，则设置该值
        /// </summary>
        public TResult Result { get; set; }

        /// <summary>
        /// 构造
        /// <see cref="AjaxResponseBase.Success"/>=true时设置
        /// </summary>
        /// <param name="result"></param>
        public AjaxResponse(TResult result)
        {
            Result = result;
            Success = true;
        }

        /// <summary>
        /// 构造
        /// <see cref="AjaxResponseBase.Success"/> =true
        /// </summary>
        public AjaxResponse()
        {
            Success = true;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="success">结果的成功状态</param>
        public AjaxResponse(bool success)
        {
            Success = success;
        }

        /// <summary>
        /// 创建错误对象
        /// <see cref="AjaxResponseBase.Success"/> =false
        /// </summary>
        /// <param name="error">错误信息</param>
        /// <param name="unAuthorizedRequest">未经授权的请求</param>
        public AjaxResponse(RemoteServiceErrorInfo error, bool unAuthorizedRequest = false)
        {
            Error = error;
            UnAuthorizedRequest = unAuthorizedRequest;
            Success = false;
        }
    }
}
