using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nu.wumpus.project
{
    class GameControl
    {
        private Boolean movePlayer;
        private Boolean shootsArrows;
        private int roomNumber;
        private int[] doorsArrangement;

        public int playerLocation(int roomNumber)
        {
            if (button4_Click()) {
                return roomNumber;
            }
        }
    }
}
