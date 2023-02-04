using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HTMLBuilder.Tool
{
    public class TextElement : HTMLElement
    { 
        public string Text
        {
            get 
            { 
                return this.Value; 
            }
            set 
            {  
                this.SetValue(value); 
            }
        }
        public TextElement() : base("p")
        {
        }
    }
}
