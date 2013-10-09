using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Text;
using WcfExtensions.Exceptions;

namespace WcfExtensions.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBehavior"></typeparam>
    public class DefaultBehaviorExtensionElement<TBehavior>
        : BehaviorExtensionElement, IBehaviorExtensionElement
        where TBehavior : class
    {
        private readonly Type behaviorType;

        /// <summary>
        /// 
        /// </summary>
        public DefaultBehaviorExtensionElement()
        {
            Type extensionType = typeof(TBehavior);

            if (
                    (extensionType.GetInterface("IServiceBehavior", false)
                    ?? extensionType.GetInterface("IEndpointBehavior", false)
                    ) == null
                )
                throw new ServiceBehaviorException("The object type indicated on generic parameter definition of Behavior extension element must implement IServiceBehavior or IEndpointBehavior interface.");

            this.behaviorType = extensionType;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override object CreateBehavior()
        {
            return Activator.CreateInstance(this.BehaviorType, true);
        }

        /// <summary>
        /// 
        /// </summary>
        public override Type BehaviorType
        {
            get { return this.behaviorType; }
        }
    }
}
