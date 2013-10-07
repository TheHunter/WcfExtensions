using System;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;

namespace WcfExtensions.Configuration
{
    /// <summary>
    /// A custom class for configuring service extension behaviors.
    /// </summary>
    /// <typeparam name="TResource"></typeparam>
    public class ActionServiceBehaviorElement<TResource>
        : BehaviorExtensionElement
        where TResource : IServiceBehavior
    {
        private readonly Type behaviorType;

        /// <summary>
        /// 
        /// </summary>
        public ActionServiceBehaviorElement()
        {
            this.behaviorType = typeof (TResource); 
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("incomingAction", IsRequired = true)]
        protected string IncomingAction
        {
            get { return (string)this["incomingAction"]; }
            set { this["incomingAction"] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("outgoingAction", IsRequired = true)]
        protected string OutgoingAction
        {
            get { return (string)this["outgoingAction"]; }
            set { this["outgoingAction"] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("providerType", IsRequired = true)]
        protected string TypeProvider
        {
            get { return (string)this["providerType"]; }
            set { this["providerType"] = value; }
        }

        /// <summary>
        /// The method name which will be invoked after receiving the current wcf operation request.
        /// </summary>
        public string IncomingTargetAction { get; private set; }

        /// <summary>
        /// The method name which will be invoked before sending the current wcf operation reply.
        /// </summary>
        public string OutgoingTargetAction { get; private set; }

        /// <summary>
        /// The object type that contains methods for incoming / outgoing messages inspected by internal inspectors.
        /// </summary>
        public Type ProviderType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public override Type BehaviorType
        {
            get { return this.behaviorType; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void PostDeserialize()
        {
            try
            {
                base.PostDeserialize();
                this.IncomingTargetAction = this.IncomingAction;
                this.OutgoingTargetAction = this.OutgoingAction;
                this.ProviderType = Type.GetType(this.TypeProvider, true);
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException("Error when ActionServiceBehaviorElement invoked the PostDeserialize method, for details see innerException", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override object CreateBehavior()
        {
            return Activator.CreateInstance(this.behaviorType, this.ProviderType, this.IncomingTargetAction, this.OutgoingTargetAction);
        }
    }
}