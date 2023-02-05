using HTMLBuilder.Tool;
using System;
using System.Xml.Linq;

namespace HTMLBuilder
{
    public class BodyElement : HTMLElement
    {
        public BodyElement() : base("body")
        {
        }


        public XElement AddScript(string javascript)
        {
            var styleElement = new XElement("script");
            styleElement.Value = javascript;
            this.Add(styleElement);
            return styleElement;
        }
    }
}