using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using WcfExtensions.Exceptions;

namespace WcfExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionServiceBehavior
        : Attribute, IServiceBehavior, IServiceActionProvider
    {
        private readonly Action incomingAction;
        private readonly Action outgoingAction;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="incomingActionMethod"></param>
        /// <param name="outgoingActionMethod"></param>
        public ActionServiceBehavior(Type provider, string incomingActionMethod, string outgoingActionMethod)
        {
            if (provider == null)
                throw new ServiceArgumentException("The provider for getting ISessionFactory object cannot be null.", "provider");

            if (string.IsNullOrEmpty(incomingActionMethod))
                throw new ServiceArgumentException("The incoming action method cannot be null.", "incomingActionMethod");

            if (string.IsNullOrEmpty(outgoingActionMethod))
                throw new ServiceArgumentException("The outgoing action method cannot be null.", "outgoingActionMethod");

            const BindingFlags flags = BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            MethodInfo incomingMethod = provider.GetMethod(incomingActionMethod, flags);
            MethodInfo outgoingMethod = provider.GetMethod(outgoingActionMethod, flags);

            if (incomingMethod == null)
                throw new ServiceArgumentException(string.Format("No incoming method found, name method: {0}", incomingActionMethod), "incomingActionMethod");

            if (outgoingActionMethod == null)
                throw new ServiceArgumentException(string.Format("No outgoing method found, name method: {0}", outgoingActionMethod), "outgoingActionMethod");

            try
            {
                this.incomingAction = (Action) Delegate.CreateDelegate(typeof(Action), incomingMethod, true);
                this.outgoingAction = (Action) Delegate.CreateDelegate(typeof(Action), outgoingMethod, true);
            }
            catch (Exception ex)
            {
                throw new ServiceBehaviorException("An inner exception occurs when the calling instance tried to compile own action for inspectors, for details see innerException.", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="incomingAction"></param>
        /// <param name="outgoingAction"></param>
        public ActionServiceBehavior(Action incomingAction, Action outgoingAction)
        {
            if (incomingAction == null)
                throw new ServiceArgumentException("Incoming sction cannot be null.", "incomingAction");

            if (outgoingAction == null)
                throw new ServiceArgumentException("Outgoing sction cannot be null.", "outgoingAction");

            this.incomingAction = incomingAction;
            this.outgoingAction = outgoingAction;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        /// <param name="endpoints"></param>
        /// <param name="bindingParameters"></param>
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            // nothing to do..
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (var endpoint in channelDispatcher.Endpoints)
                {
                    endpoint.DispatchRuntime.MessageInspectors
                        .Add(new ActionMessageInspector(this));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            // nothing to do..
        }

        /// <summary>
        /// 
        /// </summary>
        Action IServiceActionProvider.IncomingAction
        {
            get { return this.incomingAction; }
        }

        /// <summary>
        /// 
        /// </summary>
        Action IServiceActionProvider.OutgoingAction
        {
            get { return this.outgoingAction; }
        }
    }
}
