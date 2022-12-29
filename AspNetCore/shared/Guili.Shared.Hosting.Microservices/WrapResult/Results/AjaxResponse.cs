using System;
using Volo.Abp.Http;

namespace Guili.Shared.Hosting.Microservices.WrapResult.Results
{
    [Serializable]
    public class AjaxResponse : AjaxResponse<object>
    {
        public AjaxResponse() : base()
        {
        }

        public AjaxResponse(bool success) : base(success)
        {

        }

        public AjaxResponse(object result) : base(result)
        {

        }

        public AjaxResponse(RemoteServiceErrorInfo error, bool unAuthorizedRequest = false) : base(error, unAuthorizedRequest)
        {

        }
    }
}
