using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipLiteLibrary.Models
{
    public class GridSpotModel
    {
        public string  SpotLetter { get; set; }
        public int SpotNumber { get; set; }
        public GridSpotStatus Status { get; set; } = GridSpotStatus.Empty; // initial value assigned 

        //enum: 0 = empty, 1 = ship, 2 = miss, 3 = hitt, 4 = sunk
    }
}
