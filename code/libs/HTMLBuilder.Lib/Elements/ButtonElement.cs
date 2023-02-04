using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HTMLBuilder.Tool
{
    public class ButtonElement : HTMLElement
    {
        public string Text { get
            {
                return this.Value;
            }  set
            {
                this.Value = value;
            }
        }

        public delegate void ClickedEventHandler(object sender, ClickEventArgs e);
        public event ClickedEventHandler ClickedEvent;

        public ButtonElement() : base("button")
        {
            this.SetAttributeValue("type", "button");
            this.SetAttributeValue("onClick", "onClickHandler()");
        }
    }
}
