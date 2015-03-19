using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfExtensions.Configuration
{
    using System.Configuration;

    /// <summary>
    /// Class ScriptBehaviorExtensionElement.
    /// </summary>
    /// <typeparam name="TBehavior">The type of the t behavior.</typeparam>
    public class ScriptBehaviorExtensionElement<TBehavior>
        : DynamicBehaviorExtensionElement<TBehavior>
        where TBehavior : class
    {
        /// <summary>
        /// Gets the references.
        /// </summary>
        /// <value>The references.</value>
        [ConfigurationProperty("references", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(GenericConfigCollection<ReferenceInfo>))]
        public GenericConfigCollection<ReferenceInfo> References
        {
            get { return (GenericConfigCollection<ReferenceInfo>)base["references"]; }
        }

        /// <summary>
        /// Creates the behavior.
        /// </summary>
        /// <returns>System.Object.</returns>
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
