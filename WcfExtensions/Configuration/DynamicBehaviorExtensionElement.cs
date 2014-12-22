using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WcfExtensions.Configuration
{
    /// <summary>
    /// A dynamic behavior which is able to get specified properties retrieved on xml configuration.
    /// </summary>
    /// <typeparam name="TBehavior">The type of the behavior.</typeparam>
    public class DynamicBehaviorExtensionElement<TBehavior>
        : DefaultBehaviorExtensionElement<TBehavior>
        where TBehavior : class
    {
        private IDictionary<string, string> attributes;
        private HashSet<PropertyInfo> setters;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicBehaviorExtensionElement{TBehavior}"/> class.
        /// </summary>
        public DynamicBehaviorExtensionElement()
        {
            attributes = new Dictionary<string, string>();
            this.setters = new HashSet<PropertyInfo>(this.BehaviorType.GetProperties().Where(p => p.CanWrite));
        }

        /// <summary>
        /// Gets a value indicating whether an unknown attribute is encountered during deserialization.
        /// </summary>
        /// <param name="name">The name of the unrecognized attribute.</param>
        /// <param name="value">The value of the unrecognized attribute.</param>
        /// <returns>
        /// true when an unknown attribute is encountered while deserializing; otherwise, false.
        /// </returns>
        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            if (!name.Equals("xmlns"))
                attributes.Add(name, value);

            return true;
        }

        /// <summary>
        /// Creates the behavior.
        /// </summary>
        /// <returns></returns>
        protected override object CreateBehavior()
        {
            var behavior = base.CreateBehavior();

            foreach (var element in attributes)
            {
                PropertyInfo prop = setters.FirstOrDefault(n => n.Name.Equals(element.Key, StringComparison.InvariantCultureIgnoreCase));
                Type propType = prop.PropertyType;

                try
                {
                    object val = null;
                    if (propType.Equals(typeof(string)))
                    {
                        val = element.Value;
                    }
                    else if (propType.Equals(typeof(bool)))
                    {
                        val = element.Value.Equals("1") || element.Value.Equals("true", StringComparison.InvariantCultureIgnoreCase);
                    }
                    else if (propType.IsEnum)
                    {
                        val = Enum.Parse(propType, element.Value, true);
                    }
                    else
                    {
                        val = Convert.ChangeType(element.Value, propType);
                    }
                    prop.SetValue(behavior, val, Enumerable.Empty<object>().ToArray());
                }
                catch (Exception)
                {
                }
            }
            return behavior;
        }
    }
}
