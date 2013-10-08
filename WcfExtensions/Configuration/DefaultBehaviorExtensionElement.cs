using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;

namespace WcfExtensions.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBehavior"></typeparam>
    public class DefaultBehaviorExtensionElement<TBehavior>
        : BehaviorExtensionElement
        where TBehavior : class
    {

        protected override object CreateBehavior()
        {
            return Activator.CreateInstance(this.BehaviorType, true);
        }


        public override Type BehaviorType
        {
            get { return typeof(TBehavior); }
        }
    }
}
