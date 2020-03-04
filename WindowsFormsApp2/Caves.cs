using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nu.wumpus.project
{
    class Caves
    {
        //instance variables and class constants
        public static int[] special = { 1, 7, 13, 19, 25, 6, 12, 18, 24, 30 };
        private StreamWriter writer;
        private StreamReader reader;
        private int playerLoc;
        private int startingLoc;
        private ArrayList[] door = new ArrayList[30] { new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList(), new ArrayList()};
        private Boolean[] wumpus = new bool[30];
        private Boolean[] bats = new bool[30];
        private Boolean[] pits = new bool[30];


        //constructors
        public Caves(String filename)
        {
            if (File.Exists(filename))
            {
                ImportFrom(filename);
            } else
            {
                GenerateDoors();
                GenerateHazards();
                ExportTo(filename);
            }
            
        }
        public Caves()
        {
            if (File.Exists("Save.txt"))
            {
                ImportFrom("Save.txt");
            }
            else
            {
                GenerateDoors();
                GenerateHazards();
                ExportTo("Save.txt");
            }
        }

        //accessors
        public int[] GetDoors(int r)
        {
            int[] ret = new int[door[r - 1].Count];
            door[r - 1].CopyTo(ret);
            return ret;
        }
        public int GetPlayerLocation()
        {
            return playerLoc;
        }
        public String[] GetSurroundingHazard(int r)
        {
            ArrayList hazards = new ArrayList();
            int[] connect = ConnectedRooms(r);
            foreach (int i in connect)
            {
                if (GetCurrentHazard(i) != null)
                {
                    hazards.Add(GetCurrentHazard(i));
                }
            }
            string[] str = (String[])hazards.ToArray(typeof(String));
            return str;
        }
        public String GetCurrentHazard(int r)
        {
            if (wumpus[r-1] == true)
            {
                return ("wumpus");
            }
            if (bats[r-1] == true)
            {
                return ("bat");
            }
            if (pits[r-1] == true)
            {
                return ("pit");
            }
            return null;
        }
        public String GetHint()
        {
            Random rand = new Random();
            int choice = rand.Next(0, 100);
            if (choice < 30)
            {
                for (int i = 0; i < 30; i++)
                {
                    if (wumpus[i] == true)
                    {
                        int[] near = ConnectedRooms(i + 1);
                        return ("The wumpus is next to room " + near[rand.Next(0, 6)]);
                    }
                }
            } else if (choice < 60)
            {
                int[] temp = new int[2];
                temp[0] = -1;
                temp[1] = -1;
                for (int i = 0; i < 30; i++)
                {
                    if (pits[i] == true)
                    {
                        if (temp[0] != -1)
                        {
                            temp[0] = i + 1;
                        } else
                        {
                            temp[1] = i + 1;
                        }
                    }
                }
                if (choice < 50)
                {
                    return ("theres a pit in room " + temp[rand.Next(0, 2)]);
                }
                return ("theres a pit in room " + temp[0] + " and " + temp[1]);
            }else if (choice < 90)
            {
                int[] temp = new int[2];
                temp[0] = -1;
                temp[1] = -1;
                for (int i = 0; i < 30; i++)
                {
                    if (bats[i] == true)
                    {
                        if (temp[0] != -1)
                        {
                            temp[0] = i + 1;
                        }
                        else
                        {
                            temp[1] = i + 1;
                        }
                    }
                }
                if (choice < 80)
                {
                    return ("theres a bat in room " + temp[rand.Next(0, 2)]);
                }
                return ("theres a bat in room " + temp[0] + " and " + temp[1]);
            } else
            {
                for (int i = 0; i < 30; i++)
                {
                    if (wumpus[i] == true)
                    {
                        return ("The wumpus is in room " + (i + 1));
                    }
                }
            }
            return "error";
        }

        //mutators
        public Boolean MovePlayer(int target)
        {
            if (GetDoors(playerLoc).Contains(target)) 
            {
                playerLoc = target;
                return true;

            }
            return false;
        }
        private void ForceMovePlayer(int target)
        {
            playerLoc = target;

        }

        public void EncounterBat(int r)
        {
            Random rand = new Random();
            int candidate = rand.Next(1, 31);
            while (bats[candidate-1] | pits[candidate - 1] | wumpus[candidate - 1] | candidate == r)
            {
                candidate = rand.Next(1, 31);
            }
            bats[r - 1] = false;
            bats[candidate - 1] = true;
            ForceMovePlayer(rand.Next(1, 31));
            
        }

        public void EncounterPit()
        {
            ForceMovePlayer(startingLoc); 
        }
        public void EncounterWumpus(int r)
        {
            Random rand = new Random();
            int candidate = rand.Next(1, 31);
            int[] near = ConnectedRooms(r);
            while (bats[candidate - 1] | pits[candidate - 1] | wumpus[candidate - 1] | near.Contains(candidate) | candidate == r)
            {
                candidate = rand.Next(1, 31);
            }
            wumpus[r - 1] = false;
            wumpus[candidate - 1] = true;

        }
        

        //quality of life methods
        private int[] ConnectedRooms(int r)
        {
            int[] ret = new int[6];
            ret[0] = Range(r+1);
            ret[1] = Range(r-1);
            ret[2] = Range(r+6);
            ret[3] = Range(r-6);
            if (special.Contains(r))
            {
                ret[4] = Range(r + 5);
                ret[5] = Range(r - 5);
            } else
            {
                if ((r % 2) == 1)
                {
                    ret[4] = Range(r - 7);
                    ret[5] = Range(r - 5);
                } else
                {
                    ret[4] = Range(r + 7);
                    ret[5] = Range(r + 5);
                }
            }
            return ret;
        }
        private int Range(int a)
        {
            if (a > 30) return (a - 30);
            if (a < 1) return (a + 30);
            return a;
        }
        public void ImportFrom(String filename)
        {
            reader = new StreamReader(filename);
            String str;
            String[] bits;
            playerLoc = int.Parse(reader.ReadLine());

            for (int i = 0; i <= 29; i++)
            {
                str = reader.ReadLine();
                bits = str.Split(' ');
                wumpus[i] = Boolean.Parse(bits[0]);
                bats[i] = Boolean.Parse(bits[1]);
                pits[i] = Boolean.Parse(bits[2]);
                for (int j = 3; j < bits.Length; j++)
                {
                    door[i].Add(int.Parse(bits[j]));
                }
            }
            reader.Close();
            
        }
        public void ExportTo(String filename)
        {
            writer = new StreamWriter(filename);
            writer.WriteLine(playerLoc);
            for (int i = 0; i < 30; i++)
            {
                writer.Write(wumpus[i]);
                writer.Write(' ');
                writer.Write(bats[i]);
                writer.Write(' ');
                writer.Write(pits[i]);
                foreach (int d in door[i])
                {
                    writer.Write(' ');
                    writer.Write(d);
                }
                writer.WriteLine();
            }

            writer.Close();
        }

        private Boolean AllConnect()
        {
            ArrayList done = new ArrayList();
            ArrayList saw = new ArrayList();
            saw.Add(1);
            int[] possibleDoors;
            while (saw.Count > 0)
            {
                possibleDoors = GetDoors((int)saw[0]);
                foreach (int i in possibleDoors)
                {
                    if (!saw.Contains(i) && !done.Contains(i))
                    {
                        saw.Add(i);
                    }
                }
                done.Add(saw[0]);
                saw.RemoveAt(0);

                if ((done.Count + saw.Count) == 30)
                {
                    return true;
                }
            }
            Console.WriteLine("reached end (done.Count + saw.Count) = " + (done.Count + saw.Count));
            return ((done.Count + saw.Count) == 30);


        }
        private void GenerateDoors()
        {
            Random rand = new Random();
            
            int[] connect;
            for (int i = 1; i <= 30; i++)
            {
                connect = ConnectedRooms(i);
                int cap = rand.Next(1, 3);
                for (int j = 1; j <= cap; j++)
                {
                    if (door[i - 1].Count < 3)
                    { 
                        int r = connect[rand.Next(0, 6)];
                        while (door[i-1].Contains(r))
                        {
                            r = connect[rand.Next(0, 6)];
                        }
                        if (door[r - 1].Count < 3)
                        {
                            PlaceDoor(i, r);
                        }
                    }
                }
            }
            
            while (!AllConnect())
            {
                int r = rand.Next(1, 31);
                if (door[r-1].Count < 3)
                { 
                    connect = ConnectedRooms(r);
                    int r2 = connect[rand.Next(0, 6)];
                    while (door[r-1].Contains(r2))
                    {
                        r2 = connect[rand.Next(0, 6)];
                    }
                    PlaceDoor(r, connect[r2]);
                }
               
            }

            foreach (ArrayList a in door)
            {
                a.Sort();
            }

        }
        private void PlaceDoor(int a, int b)
        {
            door[a-1].Add(b);
            door[b-1].Add(a);
            Console.WriteLine("added door " + a + " " + b);
        }
        private void GenerateHazards()
        {
            Random rand = new Random();
            ArrayList taken = new ArrayList();
            //gen player start
            int r = rand.Next(1, 31);
            playerLoc = r;
            startingLoc = r;
            taken.Add(r);
            //gen wumpus
            rand.Next(1, 31);
            while (taken.Contains(r))
            {
                r = rand.Next(1, 31);
            }
            wumpus[r - 1] = true;
            taken.Add(r);
            //gen bat1
            rand.Next(1, 31);
            while (taken.Contains(r))
            {
                r = rand.Next(1, 31);
            }
            bats[r - 1] = true;
            taken.Add(r);
            //gen bat1
            rand.Next(1, 31);
            while (taken.Contains(r))
            {
                r = rand.Next(1, 31);
            }
            bats[r - 1] = true;
            taken.Add(r);
            //gen pit1
            rand.Next(1, 31);
            while (taken.Contains(r))
            {
                r = rand.Next(1, 31);
            }
            pits[r - 1] = true;
            taken.Add(r);
            //gen pit2
            rand.Next(1, 31);
            while (taken.Contains(r))
            {
                r = rand.Next(1, 31);
            }
            pits[r - 1] = true;
            taken.Add(r);
        }
        
    }
}
