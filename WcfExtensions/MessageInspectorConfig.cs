using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace WcfExtensions
{
    public class MessageInspectorConfig
        : ConfigurationSection
    {
        private Type behaviorType;
        private Type providerType;


        [ConfigurationProperty("incomingAction", IsRequired = true)]
        public string IncomingAction
        {
            get { return (string)this["incomingAction"]; }
            set { this["incomingAction"] = value; }
        }

        [ConfigurationProperty("outgoingAction", IsRequired = true)]
        public string OutgoingAction
        {
            get { return (string)this["outgoingAction"]; }
            set { this["outgoingAction"] = value; }
        }

        [ConfigurationProperty("behaviorType", IsRequired = true)]
        protected string TypeBehavior
        {
            get { return (string)this["behaviorType"]; }
            set { this["behaviorType"] = value; }
        }

        [ConfigurationProperty("providerType", IsRequired = true)]
        protected string TypeProvider
        {
            get { return (string)this["providerType"]; }
            set { this["providerType"] = value; }
        }

        public Type BehaviorType
        {
            get { return this.behaviorType; }
        }

        public Type ProviderType
        {
            get { return this.providerType; }
        }

        protected override void PostDeserialize()
        {
            base.PostDeserialize();
            this.behaviorType = Type.GetType(this.TypeBehavior, true);
            this.providerType = Type.GetType(this.TypeProvider, true);
        }
    }
}
