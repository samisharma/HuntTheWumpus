// Created by Lina Albadawi
// Per 3 AP Comp Sci


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameGI
{
    class gameGICode
    {
        public gameGICode()
        {

        }

        public void homeScreenDisplay()
        { //GameControl , highscore 

        }
        public void draw()
        {

        }
        public void hintsDisplay(String hintsFound)
        { // gameLocations, 

        }
        public void displayContent(int GameControl, int GameLocation)
        { //what the user sees

        }
        public void displayRoom(int roomLocation) // i think this type needs to change
        { //gameLocations

        }

        public void displayBats(int batsLocation, int newPlayerLocation)
        { //Gamelocations -- would a bool be needed here?

        }
        public void displayBottomlessPit(int pitLocation, bool ecapePit)
        {//GameLocations, triviaMangment

        }
        public void displayPurchase(int arrowBuying, int seacretBuying)
        { // triviamangment
          //arrows
          //seacrets
        }
        public void Arrow(int triviaManagment, int gameLocations) //rename these
        { //triviaMangment, gameLocations
          // i believe this can include shooting arrows as well but that may need to be made into a new method
        }
        public void Secret(String secretsEarned) //trivia managment -- make it an array maybe
        {

        }
        public void displayInventory(int arrowNum, int coinNum)
        {//player

        }
        public void displayHighScore(int playerScore, int pastPlayerScores) // maybe make pastPlayerScores an array or a string 
        { //HighScoreMangament and maybe GameControl                        //note to ask highScore about this

        }
        public void displayPlayer(int playerLocations)
        {//gameLocations -- a coordinate? 
            //player image itself
            //move player display
        }
        public void wumpusDisplay(int wumpusLocation, bool wonBattle)
        { //from gamelocations, triviamanagment
          // additional note, create an inbattle image here. if battle lost -- game over -- false, if won -- game continues -- true
        }


    }
}
