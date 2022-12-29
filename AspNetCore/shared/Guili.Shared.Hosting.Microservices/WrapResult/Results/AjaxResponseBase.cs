using Volo.Abp.Http;

namespace Guili.Shared.Hosting.Microservices.WrapResult.Results
{
    public abstract class AjaxResponseBase
    {

        /// <summary>
        /// 指示结果的成功状态
        /// 如果为false,则设置 <see cref="Error"/> 
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误信息 (仅当 <see cref="Success"/> =false 时设置）
        /// </summary>
        public RemoteServiceErrorInfo Error { get; set; }

        /// <summary>
        /// 未经授权的请求
        /// </summary>
        public bool UnAuthorizedRequest { get; set; }

        /// <summary>
        /// 是否是由ABP Api包装的响应
        /// </summary>
        public bool __abp { get; } = true;
    }
}
