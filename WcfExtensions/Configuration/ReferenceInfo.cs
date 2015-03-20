using System.Configuration;

namespace WcfExtensions.Configuration
{
    /// <summary>
    /// Class ReferenceInfo.
    /// </summary>
    public class ReferenceInfo
        : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        /// <value>The reference.</value>
        [ConfigurationProperty("namespace", IsRequired = true)]
        public string Namespace
        {
            get { return this["namespace"] as string; }
            set { this["namespace"] = value; }
        }
    }
}
