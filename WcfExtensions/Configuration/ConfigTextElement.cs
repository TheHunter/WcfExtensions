using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml;

namespace WcfExtensions.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigTextElement
        : ConfigurationElement 
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; private set; }

        /// <summary>
        /// Deserializes the element.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="s">if set to <c>true</c> [s].</param>
        protected override void DeserializeElement(XmlReader reader, bool s)
        {
            this.Value = reader.ReadElementContentAs(typeof(string), null) as string;
        }
    }
}
