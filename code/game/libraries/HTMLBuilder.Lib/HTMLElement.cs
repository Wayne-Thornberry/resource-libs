using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HTMLBuilder.Tool
{
    public class HTMLElement : XElement
    {
        public string Class
        {
            get
            {
                return this.Attribute("class").Value;
            }
            set
            {
                this.SetAttributeValue("class", value);
            }
        }
        public string Id
        {
            get
            {
                return this.Attribute("id").Value;
            }
            set
            {
                this.SetAttributeValue("id", value);
            }
        }
        public HTMLElement(XName name) : base(name)
        {
        }
    }
}
