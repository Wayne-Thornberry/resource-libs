﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Core.Server.MidLayer.Data
{
    public class PlayerDenial
    {

    }

    public class PlayerProfile
    {
        public long PlayerId { get; set; }
        public bool IsDenied { get; set; }
        public PlayerDenial Denial { get; set; }
    }
}
