using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HTMLBuilder.Tool
{
    public class ListElement : HTMLElement, ICollection<string>
    {
        private List<string> _elements;

        public ListElement() : base("ul")
        {
            _elements = new List<string>();
        }

        public int Count => _elements.Count;

        public bool IsReadOnly => false;

        public void Add(string item)
        {
            _elements.Add(item);
            this.Add(new XElement("li", item));
        } 

        public void Clear()
        {
            _elements.Clear();
        }

        public bool Contains(string item)
        {
            return _elements.Contains(item);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            _elements.CopyTo(array, arrayIndex);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        public bool Remove(string item)
        {
            return _elements.Remove(item); 
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
