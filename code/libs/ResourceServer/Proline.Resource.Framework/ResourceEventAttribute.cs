﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Framework
{
    public class ResourceEventAttribute : Attribute
    {
        public ResourceEventAttribute(string name, string type)
        {
            Name = name;
            Type = type;
        }
        public string Name { get; }
        public string Type { get; }
    }
}