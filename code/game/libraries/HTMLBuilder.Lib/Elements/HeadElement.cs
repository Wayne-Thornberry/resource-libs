using HTMLBuilder.Tool;
using System;
using System.Xml.Linq;

namespace HTMLBuilder
{
    public class HeadElement : HTMLElement
    {
        private XElement _title;

        public string Title
        {
            get { return _title == null ? null : _title.Value; }
            set
            {
                if (_title == null)
                {
                    _title = new XElement("title");
                    this.Add(_title);
                }
                _title.Value = value;
            }
        }

        public HeadElement() : base("head")
        {

        }

        internal XElement AddSrc(string link)
        {
            var styleElement = new XElement("script");
            styleElement.SetAttributeValue("src", link);
            styleElement.Value = "";
            this.Add(styleElement);
            return styleElement;
        }

        public XElement AddScript(string javascript)
        {
            var styleElement = new XElement("script");
            styleElement.Value = javascript;
            this.Add(styleElement);
            return styleElement;
        }

        internal XElement AddStyleSheet(string v)
        {
            var styleElement = new XElement("link");
            styleElement.SetAttributeValue("rel", "stylesheet");
            styleElement.SetAttributeValue("href", v);
            styleElement.Value = "";
            this.Add(styleElement);
            return styleElement;
        }

        public XElement AddStyle(string style)
        {
            var styleElement = new XElement("style"); 
            styleElement.Value = style;
            this.Add(styleElement);
            return styleElement;
        }
    }
}