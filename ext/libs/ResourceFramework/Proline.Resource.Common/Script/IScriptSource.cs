﻿using Proline.Resource.Common.CFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Common.Script
{
    public interface IScriptSource
    {
        IFXConsole Console { get; }
        IFXEvent EventTrigger { get; }
        IFXEventHandlerDictionary EventHandler { get; }
        IFXResource Resource { get; }
        IFXTask Task { get; }
    }
}
