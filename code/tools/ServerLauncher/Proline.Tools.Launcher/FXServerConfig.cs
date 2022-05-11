using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ServerLauncher.Program
{
    public class KeyElement : ConfigurationElement
    {
        public KeyElement() { }

        public KeyElement(string name, string value)
        {
            Name = name;
            Value = value;
        }

        [ConfigurationProperty("name", DefaultValue = "", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("value", DefaultValue = "File", IsRequired = true, IsKey = false)]
        public string Value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }
    }


    public class ResourceElement : ConfigurationElement
    {
        public ResourceElement() { }

        public ResourceElement(string name)
        {
            Name = name;
        }

        [ConfigurationProperty("name", DefaultValue = "", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }
    }

    public class ResourceCollection : ConfigurationElementCollection
    {
        public ResourceCollection()
        {

        }

        public ResourceElement this[int index]
        {
            get { return (ResourceElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ResourceElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ResourceElement)element).Name;
        }
    }

    public class GroupElement : ConfigurationElement
    {
        public GroupElement() { }

        public GroupElement(string name)
        {
            Name = name;
        }

        [ConfigurationProperty("name", DefaultValue = "", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("type", DefaultValue = "", IsRequired = true, IsKey = true)]
        public string Type
        {
            get { return (string)this["type"]; }
            set { this["type"] = value; }
        }

        [ConfigurationProperty("access", DefaultValue = "", IsRequired = true, IsKey = true)]
        public string Access
        {
            get { return (string)this["access"]; }
            set { this["access"] = value; }
        }
    }

    public class GroupCollection : ConfigurationElementCollection
    {
        public GroupCollection()
        {

        }

        public GroupElement this[int index]
        {
            get { return (GroupElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new GroupElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((GroupElement)element).Name;
        }
    }

    public class PrincipalElement : ConfigurationElement
    {
        public PrincipalElement() { }

        public PrincipalElement(string identifier, string group)
        {
            Identifier = identifier;
            Group = group;
        }

        [ConfigurationProperty("identifier", DefaultValue = "", IsRequired = true, IsKey = true)]
        public string Identifier
        {
            get { return (string)this["identifier"]; }
            set { this["identifier"] = value; }
        }

        [ConfigurationProperty("group", DefaultValue = "", IsRequired = true, IsKey = true)]
        public string Group
        {
            get { return (string)this["group"]; }
            set { this["group"] = value; }
        }
    }

    public class PrincipalCollection : ConfigurationElementCollection
    {
        public PrincipalCollection()
        {

        }

        public PrincipalElement this[int index]
        {
            get { return (PrincipalElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new PrincipalElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PrincipalElement)element).Identifier;
        }
    }

    public class TagElement : ConfigurationElement
    {
        public TagElement() { }

        public TagElement(string name)
        {
            Name = name;
        }

        [ConfigurationProperty("name", DefaultValue = "", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }
    }

    public class TagCollection : ConfigurationElementCollection
    {
        public TagCollection()
        {

        }

        public TagElement this[int index]
        {
            get { return (TagElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new TagElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TagElement)element).Name;
        }
    }

    public class FXServerConfig : ConfigurationSection
    {

        [ConfigurationProperty("resources", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ResourceCollection), AddItemName = "resource")]
        public ResourceCollection Resources
        {
            get
            {
                return (ResourceCollection)base["resources"];
            }
        }

        [ConfigurationProperty("groups", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(GroupCollection), AddItemName = "group")]
        public GroupCollection Groups
        {
            get
            {
                return (GroupCollection)base["groups"];
            }
        }

        [ConfigurationProperty("principals", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(PrincipalCollection), AddItemName = "principal")]
        public PrincipalCollection Principals
        {
            get
            {
                return (PrincipalCollection)base["principals"];
            }
        }

        [ConfigurationProperty("tags", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(TagCollection), AddItemName = "tag")]
        public TagCollection Tags
        {
            get
            {
                return (TagCollection)base["tags"];
            }
        }
    }
}
