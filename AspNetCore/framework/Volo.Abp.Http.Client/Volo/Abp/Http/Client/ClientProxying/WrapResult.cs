namespace Volo.Abp.Http.Client.ClientProxying
{
    public class WrapResult<T>
    {
        public T Result { get; set; }
        public bool Success { get; set; }
        public RemoteServiceErrorInfo Error { get; set; }
        public bool UnAuthorizedRequest { get; set; }
        public bool __abp { get; set; }
    }
}
