using System;
/*  3x3 grid x (little x) vs o (little o)
 *  objective: not to get 3 in line (column or diagonal)
 * 
 *  1st line of input: number, diagrams of 3x3 {x,o,.}
 *      "number
 * 
 *      1st diagram
 *  
 *      2nd diagram
 * 
 *      ...
 * 
 *      nth diagram"
 *  output: who won which game
 *      ON...X (capital, N = nobody)
 *  
 *  1st line of input: symbol ox
 *      write explanation of rules and controls... 
 *          -will be coordinate based column:[123] row:[123]
 *      ask for input
 *      Chimp plays
 *      PlayGround state writes out.
 *      ...and so on. q for quit - uses Chimp to deduce the perfect play outcome and quits
 * 
 *  DESIGN:
 *      class PlayGround 
 *          static char[3,3]
 *          method Edit(symbol, col, row)
 *          method Print() - obviously...
 *          method Read()   - Default - just dots
 *                          - Diagram - reads from input
 *          
 * 
 *      class Input
 *          method FirstLine() - gets number of test games, or player choice of side
 *          method Coordinates() - gets coordinates from players next move
 * 
 *      class Chimp - program that plays the game
 *          public Chimp(char a) a from {x,o}
 *          method Move() - decides where to move
 * 
 *      procedure FirstLine
 */


namespace AntiTickTackToe
{
    static class PlayGround 
    {
        static public char[,] Ground = new char[3, 3];
        static public int left;
        static public int Xs;
        static public int Os;

        static void Read(char c) //playground defining process...
        {
            int rowMax;
            left = 0;
            Xs = 0;
            Os = 0;
            if (c == '.') rowMax = 3;
            else rowMax = 4;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < rowMax; j++)
                {
                    if (c == '.') Ground[i, j] = c;
                    else if (j < 3) 
                    { 
                        Ground[i, j] = (char)Console.Read();
                        if (Ground[i, j] == '.') left++;
                        if (Ground[i, j] == 'x') Xs++;
                        if (Ground[i, j] == 'o') Os++;
                    }
                    else Console.Read();

                }
            }
        }

        static public void Default() //we want defautl empty playground
        {
            Read('.');
        }

        static public void Diagram() //user inputs their own diagram
        {
            Read('D');
        }

        static public char Turn() //who's turn it is...
        {
            if (Os == Xs) return 'x';
            else return 'o';
        }

        static public void Edit(int row, int col, char symbol) 
        {
            Ground[row, col] = symbol;
        }

        static public void Print() 
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(Ground[i, j]);
                }
                Console.WriteLine();
            }
        }

        static public int EndGame(char symbol) //vary vary clunky and ugly way to do this...
        {
            char a, b, c, d, e, f, g, h, i;
            a = Ground[0, 0];
            b = Ground[0, 1];
            c = Ground[0, 2];
            d = Ground[1, 0];
            e = Ground[1, 1];
            f = Ground[1, 2];
            g = Ground[2, 0];
            h = Ground[2, 1];
            i = Ground[2, 2];

            if (a == symbol && b == symbol && c == symbol) return 2;
            else if (d == symbol && e == symbol && f == symbol) return 2;
            else if (g == symbol && h == symbol && i == symbol) return 2;
            else if (a == symbol && d == symbol && g == symbol) return 2;
            else if (b == symbol && e == symbol && h == symbol) return 2;
            else if (c == symbol && f == symbol && i == symbol) return 2;

            else if (a == symbol && e == symbol && i == symbol) return 2;
            else if (g == symbol && e == symbol && c == symbol) return 2;
            else if (left == 0) return 1;
            else return 0;
        }
    }

    class Input
    {
        
        string a;
        public int number; //of games to evaluate
        public char symbol; //choice of side
        public int FirstLine() //1 test mode, 2 game mode, 3 quit
        {
            a = Console.ReadLine();
            bool success = Int32.TryParse(a, out int num);
            
            if (success) 
            {
                number = num;
                return 1;
            }
            else
            {
                if (a.Length > 1 && (a[0] != 'x' || a[0] != 'o' || a[0] != 'q')) return 3;
                else 
                {
                    symbol = a[0];
                    return 2;
                }
            }
        }

        public void Coordinates() 
        {
            while (true)
            {
                Console.Write("row (1-3)?");
                bool colRead = Int32.TryParse(Console.ReadLine(), out int r);
                Console.Write("column (1-3)?");
                bool rowRead = Int32.TryParse(Console.ReadLine(), out int c);
                if ((colRead && rowRead) && (PlayGround.Ground[r-1, c-1] != 'x' && PlayGround.Ground[r-1, c-1] != 'o'))
                {
                    PlayGround.Edit(r-1, c-1, symbol);
                    break;
                }
            }
                PlayGround.left--;
        }

            
    }

    class Chimp
    {
        public char friend, foe;
        public Chimp(char friend)
        {
            this.friend = friend;
            if (friend == 'x') foe = 'o';
            else foe = 'x';
        }

        int minRow;
        int minCol;

        public void Move() 
        {
            int[,] helper = new int[3, 3];

            int h;
            int dec;

            for (int r = 0; r < 3; r++) 
            {
                for (int c = 0; c < 3; c++) 
                {
                    h = 0;
                    dec = 0;
                    if (PlayGround.Ground[r, c] == friend)
                    {
                        helper[r, c] = 100;
                        h = 10;
                        dec = 8;
                    }
                    else if (PlayGround.Ground[r, c] == foe) 
                    {
                        helper[r, c] = -100;
                        h = 4;
                        dec = 2;
                    }

                    if (h > 0)
                    {
                        if (r == 0)
                        {
                            switch (c)
                            {
                                case 0:
                                    helper[1, 0] += h;
                                    helper[2, 0] += h - dec;
                                    helper[1, 1] += h;
                                    helper[2, 2] += h - dec;
                                    helper[0, 1] += h;
                                    helper[0, 2] += h - dec;
                                    break;
                                case 1:
                                    helper[0, 0] += h;
                                    helper[0, 2] += h;
                                    helper[1, 1] += h;
                                    helper[2, 1] += h - dec;
                                    break;
                                case 2:
                                    helper[0, 1] += h;
                                    helper[0, 0] += h - dec;
                                    helper[1, 1] += h;
                                    helper[2, 0] += h - dec;
                                    helper[1, 2] += h;
                                    helper[2, 2] += h - dec;
                                    break;
                            }
                        }
                        else if (r == 1)
                        {
                            switch (c)
                            {
                                case 0:
                                    helper[0, 0] += h;
                                    helper[1, 1] += h;
                                    helper[2, 0] += h;
                                    helper[1, 2] += h - dec;
                                    break;
                                case 1:
                                    for (int i = 0; i < 3; i++)
                                    {
                                        for (int j = 0; j < 3; j++)
                                        {
                                            helper[i, j] += h;
                                        }
                                    }
                                    break;
                                case 2:
                                    helper[0, 2] += h;
                                    helper[1, 1] += h;
                                    helper[2, 2] += h;
                                    helper[1, 0] += h - dec;
                                    break;
                            }
                        }

                        else
                        {
                            switch (c)
                            {
                                case 0:
                                    helper[1, 0] += h;
                                    helper[0, 0] += h - 2;
                                    helper[1, 1] += h;
                                    helper[0, 2] += h - 2;
                                    helper[2, 1] += h;
                                    helper[2, 2] += h - 2;
                                    break;
                                case 1:
                                    helper[2, 0] += h;
                                    helper[2, 2] += h;
                                    helper[1, 1] += h;
                                    helper[0, 1] += h - 2;
                                    break;
                                case 2:
                                    helper[2, 1] += h;
                                    helper[0, 0] += h - 2;
                                    helper[1, 1] += h;
                                    helper[2, 0] += h - 2;
                                    helper[1, 2] += h;
                                    helper[0, 2] += h - 2;
                                    break;
                            }
                        }
                    }

                }
            }
            int minimum = 100;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if ((helper[i, j] > 0) && (helper[i, j] < minimum)) 
                    {
                        minimum = helper[i, j];
                        minRow = i;
                        minCol = j;

                    }
                }
            }
            PlayGround.Edit(minRow, minCol, friend);
            PlayGround.left--;

        }


    }

    class MainClass
    {
        static public void TestMode(int number) 
        {
            
            int count = 0;
            char[] result = new char[number];
            while (count < number)
            {
                Console.ReadLine();
                PlayGround.Diagram();
                Chimp Caesar = new Chimp(PlayGround.Turn());
                Chimp Brutus = new Chimp(Caesar.foe);
                char caesar;
                char brutus;
                if (Caesar.friend == 'x')
                {
                    caesar = 'X';
                    brutus = 'O';
                }
                else 
                {
                    caesar = 'O';
                    brutus = 'X';
                }

                while (true)
                {
                    int a = -1;
                    Caesar.Move();
                    if ((a = PlayGround.EndGame(Caesar.friend)) == 2){ 
                        result[count] = brutus;
                        break;
                    }
                    else if (a == 1) { 
                        result[count] = 'N';
                        break;
                    }
                    Brutus.Move();
                    if ((a = PlayGround.EndGame(Caesar.foe)) == 2) { 
                        result[count] = caesar;
                        break;
                    }
                    else if (a == 1) {
                        result[count] = 'N';
                        break;
                    }
                }
                count++;
            }
            foreach (char i in result) Console.Write(i);

        }

        static public void GameMode(Input Player) 
        {
            PlayGround.Default();
            Chimp PlayerO = new Chimp(Player.symbol);
            Chimp Caesar = new Chimp(PlayerO.foe);
            PlayGround.Print();
            Console.WriteLine("Simply input coordinates where you want to play.");
            if (PlayerO.friend == 'x') 
            {
                Player.Coordinates();
            }
            while (true) 
            {
                int a = -1;
                Caesar.Move();
                if ((a = PlayGround.EndGame(Caesar.friend)) == 2)
                {
                    PlayGround.Print();
                    Console.WriteLine("{0} wins!",Caesar.foe);
                    break;
                }
                else if (a == 1)
                {
                    PlayGround.Print();
                    Console.WriteLine();
                    Console.WriteLine("It's a draw!");
                    break;
                }
                PlayGround.Print();
                Player.Coordinates();
                if ((a = PlayGround.EndGame(Caesar.foe)) == 2)
                {
                    PlayGround.Print();
                    Console.WriteLine("{0} wins!", Caesar.friend);
                    break;
                }
                else if (a == 1)
                {
                    PlayGround.Print();
                    Console.WriteLine("It's a draw!");
                    break;
                }       
            }
        
        }

        public static void Main(string[] args)
        {
            int choice;
            Input Player = new Input();
            choice = Player.FirstLine();
            if (choice == 3)
            {
                Console.WriteLine("ERROR");
            }
            else if (choice == 1) TestMode(Player.number);
            else GameMode(Player);
        }
    }

    /*





    */

}
