﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipLiteLibrary.Models
{
    // 0 = empty, 1 = ship, 2 = miss, 3 = hitt, 4 = sunk
    public enum GridSpotStatus
    {
        Empty, 
        Ship,
        Miss,
        Hit,
        Sunk
    }
}
