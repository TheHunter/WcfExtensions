using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using WcfExtensions.Exceptions;

namespace WcfExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionMessageInspector
        : IDispatchMessageInspector
    {
        private readonly IServiceActionProvider provider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        public ActionMessageInspector(IServiceActionProvider provider)
        {
            if (provider == null)
                throw new ServiceArgumentException("The service action provider cannot be null.", "provider");

            this.provider = provider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <param name="instanceContext"></param>
        /// <returns></returns>
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            provider.IncomingAction.Invoke();
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            provider.OutgoingAction.Invoke();
        }

        
    }
}
