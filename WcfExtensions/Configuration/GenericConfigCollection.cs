using System;
using System.Configuration;

namespace WcfExtensions.Configuration
{
    /// <summary>
    /// Class GenericConfigCollection.
    /// </summary>
    /// <typeparam name="TElement">The type of the t element.</typeparam>
    public class GenericConfigCollection<TElement>
        : ConfigurationElementCollection
        where TElement : ConfigurationElement, new()
    {
        private readonly string propertyName;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericConfigCollection{TElement}"/> class.
        /// </summary>
        public GenericConfigCollection()
        {
            string naming = typeof(TElement).Name;
            this.propertyName = string.Format("{0}{1}", naming.Substring(0, 1).ToLower(), naming.Substring(1));
        }

        /// <summary>
        /// Gets the name used to identify this collection of elements in the configuration file when overridden in a derived class.
        /// </summary>
        /// <value>The name of the element.</value>
        protected override string ElementName
        {
            get
            {
                return this.propertyName;
            }
        }

        /// <summary>
        /// Indicates whether the specified <see cref="T:System.Configuration.ConfigurationElement" /> exists in the <see cref="T:System.Configuration.ConfigurationElementCollection" />.
        /// </summary>
        /// <param name="elementName">The name of the element to verify.</param>
        /// <returns>true if the element exists in the collection; otherwise, false. The default is false.</returns>
        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(this.propertyName, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Configuration.ConfigurationElementCollection" /> object is read only.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Configuration.ConfigurationElementCollection" /> object is read only; otherwise, false.</returns>
        public override bool IsReadOnly()
        {
            return false;
        }

        /// <summary>
        /// Adds the specified custom.
        /// </summary>
        /// <param name="custom">The custom.</param>
        public void Add(TElement custom)
        {
            this.BaseAdd(custom);
        }

        /// <summary>
        /// Adds a configuration element to the <see cref="T:System.Configuration.ConfigurationElementCollection" />.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to add.</param>
        protected override void BaseAdd(ConfigurationElement element)
        {
            this.BaseAdd(element, false);
        }

        /// <summary>
        /// Gets the type of the <see cref="T:System.Configuration.ConfigurationElementCollection" />.
        /// </summary>
        /// <value>The type of the collection.</value>
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }

        /// <summary>
        /// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </summary>
        /// <returns>A new <see cref="T:System.Configuration.ConfigurationElement" />.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new TElement();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element when overridden in a derived class.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to return the key for.</param>
        /// <returns>An <see cref="T:System.Object" /> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement" />.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return element.GetHashCode();
        }

        /// <summary>
        /// Gets or sets the <see cref="TElement"/> at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>TElement.</returns>
        public TElement this[int index]
        {
            get
            {
                return (TElement)BaseGet(index);
            }
            set
            {
                if (this.BaseGet(index) != null)
                {
                    this.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>System.Int32.</returns>
        public int IndexOf(TElement element)
        {
            return this.BaseIndexOf(element);
        }

        /// <summary>
        /// Removes the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        public void Remove(TElement url)
        {
            if (this.BaseIndexOf(url) >= 0)
                this.BaseRemove(url.GetHashCode());
        }

        /// <summary>
        /// Removes at.
        /// </summary>
        /// <param name="index">The index.</param>
        public void RemoveAt(int index)
        {
            this.BaseRemoveAt(index);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            this.BaseClear();
        }
    }
}
