namespace nu.wumpus.project
{
    using System;
    using System.IO;
    
    class HighScore
    {
        private string folder = @"C:\test";
        private string fileName = "myLeaderBoard.txt";
        private System.IO.FileStream fs;
        private string pathString;
        public HighScore()
            {
            pathString = System.IO.Path.Combine(folder, fileName);
            System.IO.FileStream fs = System.IO.File.Create(pathString);
            }
        
        public int returnHighScore()
        {
            int calculatedHighScore = 0;
            //calculate highScore
            return calculatedHighScore;
        }
        public void makeFile()
        {
            
        //nothing being returned
        }
        public string updateFile()
        {
            return fs.ToString();
        }

        public static void main(string[] args)
        {
            HighScore highScore = new HighScore();
            Console.WriteLine(highScore.updateFile());
        }
    }
}