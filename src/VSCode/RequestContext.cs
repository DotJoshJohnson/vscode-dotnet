using System;

using VSCode.JsonRpc;

namespace VSCode
{
    public class RequestContext
    {
        internal RequestContext(RequestMessage request, LanguageServer server)
        {
            Server = server;

            Request = request;
        }

        public event EventHandler RequestCancelled;

        public RequestMessage Request { get; private set; }
        internal LanguageServer Server { get; private set; }
        

        internal void Cancel()
        {
            RequestCancelled?.Invoke(this, new EventArgs());
        }

        public void SendError(object error)
        {
            Server.SendResponseAsync(Request.Id, null, error).Wait();
        }

        public void SendResult(object result)
        {
            Server.SendResponseAsync(Request.Id, result).Wait();
        }
    }

    public class RequestContext<TRequestParams, TResponseResult> : RequestContext
        where TRequestParams : class
    {
        internal RequestContext(RequestMessage request, LanguageServer server)
            : base(request, server)
        { }

        internal RequestContext(RequestContext baseContext)
            : this(baseContext.Request, baseContext.Server)
        { }

        public TRequestParams RequestParameters { get { return Request.Params?.ToObject<TRequestParams>(); } }

        public new void SendResult(TResponseResult result)
        {
            base.SendResult(result);
        }
    }
}
