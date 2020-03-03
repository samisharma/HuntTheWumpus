using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nu.wumpus.project
{
    class Player
    {
        public Player()
        {

        }

        public Player(string v)
        {
            this.v = v;
        }

        public static int currentRoom = 0;
        public static int newRoom = 0;
        public static int arrows = 3;
        public static int coins = 0;
        public static int turns = 1;
        private string v;

        public void setCoins(int add)
        {
            coins += add;
        }

        // all called by GameControl
        public void PlayerLocation(int newRoom)
        {
            if (newRoom != currentRoom)
            {
                coins++;
                turns++;
            }
            newRoom = currentRoom;
        }
        public Boolean CanPurchaseArrow(int numberOfArrows)
        {
            if (coins > 1)
            {
                arrows += numberOfArrows;
                coins -= numberOfArrows;
                return true;
            }
            return false;
        }
        public Boolean CanPurchaseTrivia(int numberOfTrivia)
        {
            if(coins > 1)
            {
                coins -= numberOfTrivia;
                return true;
            }
            return false;
        }
        public Boolean CanPurchaseSecret(int numberOfSecrets)
        {
            if (coins > 1)
            {
                coins -= numberOfSecrets;
                return true;
            }
            return false;
        }
        public Boolean CanShoot()
        {
            if(arrows > 0)
            {
                arrows--;
                return true;
            }
            return false;
        }
        // GameControl randomly selects Cave, Trivia, or Player to provide a secret
        public String secret(int currentRoom)
        {
            String secret = "You are currently in room " + currentRoom + ".";
            return secret;
        }

        // called by HighScore
        public int GetCoins()
        {
            return coins;
        }
        public int GetArrows()
        {
            return arrows;
        }
        public int GetTurns()
        {
            return turns;
        }
    }
}
