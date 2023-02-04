using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HTMLBuilder.Tool
{
    public class ToolWindow : XElement, IHTMLBuilder
    {  
        public HeadElement Head { get; }
        public BodyElement Body { get; }

        public ToolWindow() : base("html")
        {
            this.Head = new HeadElement();
            this.Body = new BodyElement();

            this.Add(Head);
            this.Add(Body);
        }

        public void AddToHead(XElement element) { Head.Add(element); }

        public void AddToBody(XElement element) { Body.Add(element); }

        public string Build()
        {  
            return this.ToString();
        }

        public void SetupBootstrap()
        {
            var element = Head.AddStyleSheet("https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css");
            element.SetAttributeValue("integrity", "sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm");
            element.SetAttributeValue("crossorigin", "anonymous");

            var js1 = Head.AddSrc("https://code.jquery.com/jquery-3.2.1.slim.min.js");
            js1.SetAttributeValue("integrity", "sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN");
            js1.SetAttributeValue("crossorigin", "anonymous");
            var js2 = Head.AddSrc("hhttps://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js");
            js2.SetAttributeValue("integrity", "sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q");
            js2.SetAttributeValue("crossorigin", "anonymous");
            var js3 = Head.AddSrc("https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.j");
            js3.SetAttributeValue("integrity", "sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl");
            js3.SetAttributeValue("crossorigin", "anonymous");
        }
    }
}
