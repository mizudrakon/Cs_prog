using System;
/*  BEAST:
 *  only moves
 *  uses orientation to decide where 
 * 
 *  ORIENTATION:
 *  beast has 4 states: up, down, left, right
 *  steps that beast can make: TurnLeft, TurnRight, MakeStep
 *  it follows the wall
 * 
 *  INPUT:
 *  int width
 *  int height
 *  map:    X wall
 *          . empty
 *          ^ V < > beast oriented to a specific direction
 *  all handled by LABYRINTH: reads, writes the map, lets you look at your surroundings...
 * 
 *  PROGRAM:
 *  reads input as step 0
 *  for (i = 0; i<20; i++)
 *      beast movement
 *      print map
 *
 *  20 times!
 */
namespace Minotaur
{
    class Orientation //Basically handles leftfrom right
    {
        Labyrinth A;
        public int state;
        public int col, row;
        public char[] orient = { '<', '^', '>', 'v' };
        int[,] changeN = { { -1, -1 }, { -1, 1 }, { 1, 1 }, { 1, -1 } };
        int[,] changeNE = { { -2, 0 }, { 0, 2 }, { 2, 0 }, { 0, -2 } };
        int[,] changeE = { { -1, 1 }, { 1, 1 }, { 1, -1 }, { -1, -1 } };
        int[,] changeSE = { { 0, 2 }, { 2, 0 }, { 0, -2 }, { -2, 0 } };
        public struct Side
        {
            public int r, c;
            public Side(int r, int c)
            {
                this.r = r;
                this.c = c;
            }
        }
        public Side N = new Side(-1,0);
        public Side NE = new Side(-1,1);
        public Side E = new Side(0,1);
        public Side SE = new Side(1,1);
        public Orientation(Labyrinth A)
        {
            this.A = A;
            for (int i = 1; i <= A.LabH(); i++)
            {
                for (int j = 1; j <= A.LabW(); j++)
                {
                    if (A.Look(i, j) == '^' || A.Look(i, j) == '<' || A.Look(i, j) == '>' || A.Look(i, j) == 'v')
                    {
                        col = j;
                        row = i;
                        int num = 0;
                        for (; num < 3; num++)
                            if (orient[num] == A.Look(i, j)) break;
                        state = num;
                    }
                }
            }
            int n = 1;
            while (n != state) {
                n = TurnRight(n);
            }
        }

        public int TurnRight(int i)
        {
            if (i == 3) i = 0;
            else i++;
            N.r += changeN[i, 0];
            N.c += changeN[i, 1];
            NE.r += changeNE[i, 0];
            NE.c += changeNE[i, 1];
            E.r += changeE[i, 0];
            E.c += changeE[i, 1];
            SE.r += changeSE[i, 0];
            SE.c += changeSE[i, 1];
            return i;
        }
        public int TurnLeft(int i)
        {
            N.r -= changeN[i, 0];
            N.c -= changeN[i, 1];
            NE.r -= changeNE[i, 0];
            NE.c -= changeNE[i, 1];
            E.r -= changeE[i, 0];
            E.c -= changeE[i, 1];
            SE.r -= changeSE[i, 0];
            SE.c -= changeSE[i, 1];
            if (i == 0) i = 3;
            else i--;
            return i;
        }
    } //END ORIENTATION


    class Beast //Should handle position and orientatio of the Beast at every turn.
    {
        string name;
        Labyrinth A;
        Orientation O;
        int last = 0;
        public Beast(Labyrinth A, Orientation O, string name) {
            this.A = A;
            this.O = O;
            this.name = name;
        }

        private void Step(int r1, int r2, int c1, int c2) {
            A.Edit(r1, c1, '.');                    //last Beast position
            A.Edit(r2, c2, O.orient[O.state]);    //new Beast position
            O.row = r2;
            O.col = c2;
        }
        public void Move(){
            int R = O.row;
            int C = O.col;

            if (A.Look(R + O.E.r, C + O.E.c) != '.')
            {
                if (A.Look(R + O.N.r, C + O.N.c) != '.')
                {
                    O.state = O.TurnLeft(O.state);
                    Step(R, R, C, C);
                    last++;
                }
                else 
                { 
                    Step(R, R + O.N.r, C, C + O.N.c);
                    last = 0;
                }
            }
            else 
            {
                if ((A.Look(R + O.SE.r, C + O.SE.c) != '.') && (last == 0))
                {
                    O.state = O.TurnRight(O.state);
                    Step(R, R, C, C);
                    last++;

                }
                else if ((A.Look(R + O.NE.r, C + O.NE.c) != '.') && (A.Look(R + O.N.r, C + O.N.c) == '.'))
                {
                    Step(R, R + O.N.r, C, C + O.N.c);
                    last = 0;
                }
                else if (A.Look(R + O.N.r, C + O.N.c) != '.')
                {
                    if (A.Look(R - O.N.r, C - O.N.c) == '.')
                    {
                        O.state = O.TurnLeft(O.state);
                        Step(R, R, C, C);
                        last++;
                    }
                    else 
                    {
                        O.state = O.TurnRight(O.state);
                        Step(R, R, C, C);
                        last++;
                    }
                }
                else if (A.Look(R - O.N.r, C - O.N.c) != '.')
                {
                    O.state = O.TurnRight(O.state);
                    Step(R, R, C, C);
                    last++;
                }
                else 
                {
                    if (last < 4)
                    {
                        O.state = O.TurnLeft(O.state);
                        Step(R, R, C, C);
                        last++;
                    }
                    else 
                    {
                        Step(R, R + O.N.r, C, C + O.N.c);
                        last = 0;
                    }
                }
            }
        }

    }

    public class Labyrinth
    {
        int width, height;
        char[,] Map;
        public int LabW() { return width; }
        public int LabH() { return height; }
        public void ReadMap() //Takes care of input = reads width and height, loads the map from s.i.
        {
            bool success = Int32.TryParse(Console.ReadLine(), out width);
            success = Int32.TryParse(Console.ReadLine(), out height);
            if (success)
            {
                Map = new char[height+2, width+2];
                for (int i = 0; i <= height+1; i++) {
                    for (int j = 0; j <= width+1; j++) {
                        if (i == 0 || j == 0 || i == height+1) Map[i, j] = 'X';
                        else Map[i,j] = (char)Console.Read();
                    }
                }
            }
            else {
                Console.WriteLine("ERROR");
            }
        }

        public void WriteMap() //Draws the current state of the map to s.o.
        {   
            for (int i=1; i < height+1; i++) {
                for (int j = 1; j <= width+1; j++) {
                    Console.Write(Map[i, j]);
                }
            }
        }

        public char Look(int row, int col) //Returns requested position on the map
        {
            return Map[row, col];
        }

        public void Edit(int row,int col, char plaster) { //Replaces char on the spec position by our 'plaster' char
            Map[row, col] = plaster;
        }
    }
    
    


    class MainClass
    {
        public static void Main(string[] args)
        {
            int count = 0;
            Labyrinth A = new Labyrinth();
            A.ReadMap();
            Orientation O = new Orientation(A);
            Beast Minotaur = new Beast(A,O,"Minos");

            while (count++ < 20) {
                Minotaur.Move();
                A.WriteMap();
                Console.WriteLine();
            }
        }
    }
}