using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfExtensions.Configuration
{
    public class ScriptBehaviorExtensionElement<TBehavior>
        : DynamicBehaviorExtensionElement<TBehavior>
        where TBehavior : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override object CreateBehavior()
        {
            /*
            Here I have to insert dynamic code for executing dynamic initializing 
             * about the behavior to instance.
            */
            var behavior = new object();


            this.InitializeBehavior(behavior);
            return behavior;
        }
    }
}
