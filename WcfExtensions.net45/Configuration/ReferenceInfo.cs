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
        [ConfigurationProperty("reference", IsRequired = true)]
        public string Reference { get; set; }
    }
}
