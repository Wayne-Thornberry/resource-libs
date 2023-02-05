using HTMLBuilder.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HTMLBuilder.Lib.Elements
{
    public class LinkElement : HTMLElement
    {
        public LinkElement() : base("a")
        {
        }
        private XAttribute _hrefAttribute;
        public string Href
        {
            get
            {
                if (_hrefAttribute == null)
                    return "";
                return _hrefAttribute.Value;
            }
            set
            {
                if (_hrefAttribute == null)
                {
                    _hrefAttribute = new XAttribute("href", value);
                }
                _hrefAttribute.Value = value;
                this.SetAttributeValue("href", value);
            }
        }
        public string Text
        {
            get { return Value; }
            set { Value = value; }
        }
    }
}
