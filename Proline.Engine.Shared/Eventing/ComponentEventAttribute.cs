using System;

namespace Proline.Engine.Eventing
{
    public class ComponentEventAttribute : Attribute
    {
        private string _name;

        public ComponentEventAttribute(string name)
        {
            _name = name;
        }
    }
}
