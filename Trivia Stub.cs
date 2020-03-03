using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivia
{
    class Trivia
    {
        private String fileNameOfTriviaQuestions; //file with list of trivia questions
        private String fileNameOfHints; //file with list of hints

        //constructors
        public Trivia(String nameTrivia,)
        {
            fileNameOfTriviaQuestions = nameTrivia;
        }

        public GetHints(String fileNameOfHints)
        {
            String nameHints = fileNameOfHints;
        }

        public String PlayerMovesForward(String nameHints)
        {
            //randomly generated numbers to access hints from file
            //hint of type String is returned to player
            String strHint = "Hint: What is yellow and sets in the west";
            return strHint;
        }

        public Boolean PlayerEncountersObstacle(String nameTrivia)
        {
            //Ask user 5 Trivia questions from file
            //Calculate how many user got right
            //If user gets at least 3 right, return true->player moves to next room
            //If user gets less than 3 right, return false->game ends, player loses
            return true;
        }

        public Boolean PlayerEcnountersWumpus(String nameTrivia)
        {
            //Ask user 5 Trivia questions from file
            //Calculate how many user got right
            //If user gets at least 3 right, return true->player wins the game
            //If user gets less than 3 right, return false->game ends, player loses
            return true;
        }

        public Boolean PlayerShootsArrows(String nameTrivia)
        {
            //Ask user 1 trivia question
            //If user answers correctly, return true->Player can shoot arrow
            return true;
        }
    }
}
