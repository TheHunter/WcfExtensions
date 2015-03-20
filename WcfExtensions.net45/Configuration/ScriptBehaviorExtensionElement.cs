using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TheHunter.Scripting;
using WcfExtensions.Exceptions;

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
        /// 
        /// </summary>
        [ConfigurationProperty("dynamicCodeCreator", IsRequired = true)]
        protected ConfigTextElement DynamicCodeCreator
        {
            get { return base["dynamicCodeCreator"] as dynamic; }
        }

        /// <summary>
        /// Gets the references.
        /// </summary>
        /// <value>The references.</value>
        [ConfigurationProperty("references", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(GenericConfigCollection<ReferenceInfo>))]
        public GenericConfigCollection<ReferenceInfo> References
        {
            get { return base["references"] as dynamic; }
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
            if (this.BehaviorType == null)
                throw new ServiceBehaviorException("The behavior type cannot be null");

            var entryAss = Assembly.GetEntryAssembly();
            var behAss = this.BehaviorType.Assembly;

            List<Assembly> assemblies = new List<Assembly>
            {
                entryAss,
                behAss
            };

            assemblies.AddRange(entryAss.GetReferencedAssemblies()
                .Select(Assembly.Load));

            assemblies.AddRange(behAss.GetReferencedAssemblies()
                .Select(Assembly.Load));

            ScriptHosting scripHost = new ScriptHosting(this.References.OfType<string>(), assemblies);
            var behavior = scripHost.Execute<TBehavior>(this.DynamicCodeCreator.Value);

            //var behavior = base.CreateBehavior();

            this.InitializeBehavior(behavior);
            return behavior;
        }
    }
}
