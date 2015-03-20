using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml;

namespace WcfExtensions.Configuration
{
    
    public class ConfigTextElement
        : ConfigurationElement 
    {
        public string Value { get; private set; }

        protected override void DeserializeElement(XmlReader reader, bool s)
        {
            this.Value = reader.ReadElementContentAs(typeof(string), null) as string;
        }
    }
}
