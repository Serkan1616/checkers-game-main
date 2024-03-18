using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace Project3
{
    class Program
    {

        public static int cursorx = 5, cursory = 5;   // position of cursor
        static ConsoleKeyInfo cki;               // required for readkey
        static int chooser = 0;
        public static bool userTurn = true;
        public static bool compMoveChooser = false;
        public static String[,] boardArray =
        {
            {"o","o","o",".",".",".",".","." },
            {"o","o","o",".",".",".",".","." },
            {"o","o","o",".",".",".",".","." },
            {".",".",".",".",".",".",".","." },
            {".",".",".",".",".",".",".","." },
            {".",".",".",".",".","x","x","x" },
            {".",".",".",".",".","x","x","x" },
            {".",".",".",".",".","x","x","x" }
        };
        public static int[,] computerPieces =
        {
            {0,0}, {1,0}, {2,0}, {0,1}, {1,1}, {2,1}, {0,2}, {1,2}, {2,2}
        };
        public static void framePrinter()
        {
            Console.Clear();
            //column numbers 
            for (int i = 1; i < 9; i++)
            {
                Console.SetCursorPosition(2 * i + 1, 0);
                Console.Write(" " + i + " ");
            }
            //rownumbers 
            for (int i = 1; i < 9; i++)
            {
                Console.SetCursorPosition(0, i + 1);
                Console.Write(i);
            }
            //top and bottom bounds of frame
            Console.SetCursorPosition(1, 1);
            Console.WriteLine(" +-----------------+");
            Console.SetCursorPosition(1, 10);
            Console.WriteLine(" +-----------------+");
            //left and right bounds of frame
            for (int i = 1; i < 9; i++)
            {
                Console.SetCursorPosition(1, i + 1);
                Console.WriteLine(" |                 |");
            }
        }
        //function that prints the board and pieces
        public static void boardPrinter(String[,] arr)
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Console.SetCursorPosition(2 * column + 4, row + 2);
                    Console.WriteLine(arr[row, column]);
                }
            }
        }


        public static int cursoryToRow(int cursy)
        {
            return cursy - 2;
        }
        public static int cursorxToColumn(int cursx)
        {
            return cursx - 2;
        }



        public static void playerMove()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {       // true: there is a key in keyboard buffer
                    cki = Console.ReadKey(true);       // true: do not write character 
                    if (cki.Key == ConsoleKey.Z && boardArray[cursoryToRow(cursory), cursorxToColumn(cursorx)] == "x")
                    {   // key and boundary control
                        if (chooser == 1)
                        {
                            chooser = 0;
                        }
                        else if (chooser == 0)
                        {
                            chooser = 1;
                        }

                        break;
                    }

                    if (cki.Key == ConsoleKey.RightArrow && cursorx < 9)
                    {   // key and boundary control
                        Console.SetCursorPosition(2 * cursorx, cursory);
                        cursorx++;
                    }
                    if (cki.Key == ConsoleKey.LeftArrow && cursorx > 2)
                    {
                        Console.SetCursorPosition(2 * cursorx, cursory);
                        cursorx--;
                    }
                    if (cki.Key == ConsoleKey.UpArrow && cursory > 2)
                    {
                        Console.SetCursorPosition(2 * cursorx, cursory);
                        cursory--;
                    }
                    if (cki.Key == ConsoleKey.DownArrow && cursory < 9)
                    {
                        Console.SetCursorPosition(2 * cursorx, cursory);
                        cursory++;
                    }

                }

                Console.SetCursorPosition(2 * cursorx, cursory);    // refresh X (current position)

                //Thread.Sleep(25);     // sleep 50 ms

            }
        }

        public static void playerZMove()
        {
            string playerMoveType = "neither";
            int oldcursorx = cursorx;
            int oldcursory = cursory;
            int xboundry = 0;
            int yboundry = 0;
            while (true)
            {

                if (Console.KeyAvailable)
                {       // true: there is a key in keyboard buffer
                    cki = Console.ReadKey(true);       // true: do not write character 


                    if (cki.Key == ConsoleKey.RightArrow && cursorx < 9)
                    {
                        if (cursorx < 8)
                        {
                            if (boardArray[cursoryToRow(cursory), cursorxToColumn(cursorx) + 1] == "." && cursory == oldcursory && playerMoveType != "jump")
                            {
                                if (xboundry == 1)
                                {
                                    continue;
                                }
                                Console.SetCursorPosition(2 * cursorx, cursory);
                                boardArray[cursoryToRow(cursory), cursorxToColumn(cursorx)] = ".";
                                Console.WriteLine(".");
                                playerMoveType = "step";
                                yboundry++;
                                cursorx++;
                            }
                            else if (boardArray[cursoryToRow(cursory), cursorxToColumn(cursorx) + 1] != "." && boardArray[cursoryToRow(cursory), cursorxToColumn(cursorx) + 2] == "." && playerMoveType != "step")
                            {

                                Console.SetCursorPosition(2 * cursorx, cursory);
                                boardArray[cursoryToRow(cursory), cursorxToColumn(cursorx)] = ".";
                                Console.WriteLine(".");
                                xboundry++;
                                cursorx += 2;
                                playerMoveType = "jump";
                            }
                        }
                        else
                        {
                            if (boardArray[cursoryToRow(cursory), cursorxToColumn(cursorx) + 1] == "." && cursory == oldcursory && playerMoveType != "jump")
                            {
                                if (xboundry == 1)
                                {
                                    continue;
                                }
                                Console.SetCursorPosition(2 * cursorx, cursory);
                                boardArray[cursoryToRow(cursory), cursorxToColumn(cursorx)] = ".";
                                Console.WriteLine(".");
                                xboundry++;
                                playerMoveType = "step";
                                cursorx++;
                            }
                        }
                    }
                    if (cki.Key == ConsoleKey.LeftArrow && cursorx > 2)
                    {
                        if (cursorx > 3)
                        {
                            if (boardArray[cursoryToRow(cursory), cursorxToColumn(cursorx) - 1] == "." && cursory == oldcursory && playerMoveType != "jump")
                            {
                                if (xboundry == -1)
                                {
                                    continue;
                                }
                                Console.SetCursorPosition(2 * cursorx, cursory);
                                boardArray[cursoryToRow(cursory), cursorxToColumn(cursorx)] = ".";
                                Console.WriteLine(".");
                                xboundry--;
                                playerMoveType = "step";
                                cursorx--;
                            }
                            else if (boardArray[cursoryToRow(cursory), cursorxToColumn(cursorx) - 1] != "." && boardArray[cursoryToRow(cursory), cursorxToColumn(cursorx) - 2] == "." && playerMoveType != "step")
                            {

                                Console.SetCursorPosition(2 * cursorx, cursory);
                                boardArray[cursoryToRow(cursory), cursorxToColumn(cursorx)] = ".";
                                Console.WriteLine(".");
                                xboundry--;
                                cursorx -= 2;
                                playerMoveType = "jump";
                            }
                        }
                        else
                        {
                            if (boardArray[cursoryToRow(cursory), cursorxToColumn(cursorx) - 1] == "." && cursory == oldcursory && playerMoveType != "jump")
                            {
                                if (xboundry == -1)
                                {
                                    continue;
                                }
                                Console.SetCursorPosition(2 * cursorx, cursory);
                                boardArray[cursoryToRow(cursory), cursorxToColumn(cursorx)] = ".";
                                Console.WriteLine(".");
                                xboundry--;
                                playerMoveType = "step";
                                cursorx--;
                            }
                        }
                    }
                    if (cki.Key == ConsoleKey.UpArrow && cursory > 2)
                    {
                        if (cursory > 3)
                        {
                            if (boardArray[cursoryToRow(cursory) - 1, cursorxToColumn(cursorx)] == "." && cursorx == oldcursorx && playerMoveType != "jump")
                            {
                                if (yboundry == -1)
                                {
                                    continue;
                                }
                                Console.SetCursorPosition(2 * cursorx, cursory);
                                boardArray[cursoryToRow(cursory), cursorxToColumn(cursorx)] = ".";
                                Console.WriteLine(".");
                                yboundry--;
                                playerMoveType = "step";
                                cursory--;
                            }
                            else if (boardArray[cursoryToRow(cursory) - 1, cursorxToColumn(cursorx)] != "." && boardArray[cursoryToRow(cursory) - 2, cursorxToColumn(cursorx)] == "." && playerMoveType != "step")
                            {

                                Console.SetCursorPosition(2 * cursorx, cursory);
                                boardArray[cursoryToRow(cursory), cursorxToColumn(cursorx)] = ".";
                                Console.WriteLine(".");
                                yboundry--;
                                cursory -= 2;
                                playerMoveType = "jump";
                            }
                        }
                        else
                        {
                            if (boardArray[cursoryToRow(cursory) - 1, cursorxToColumn(cursorx)] == "." && cursorx == oldcursorx && playerMoveType != "jump")
                            {
                                if (yboundry == -1)
                                {
                                    continue;
                                }
                                Console.SetCursorPosition(2 * cursorx, cursory);
                                boardArray[cursoryToRow(cursory), cursorxToColumn(cursorx)] = ".";
                                Console.WriteLine(".");
                                yboundry--;
                                playerMoveType = "step";
                                cursory--;
                            }
                        }
                    }
                    if (cki.Key == ConsoleKey.DownArrow && cursory < 9)
                    {
                        if (cursory < 8)
                        {
                            if (boardArray[cursoryToRow(cursory) + 1, cursorxToColumn(cursorx)] == "." && cursorx == oldcursorx && playerMoveType != "jump")
                            {
                                if (yboundry == 1)
                                {
                                    continue;
                                }
                                Console.SetCursorPosition(2 * cursorx, cursory);
                                boardArray[cursoryToRow(cursory), cursorxToColumn(cursorx)] = ".";
                                Console.WriteLine(".");
                                yboundry++;
                                playerMoveType = "step";
                                cursory++;
                            }
                            else if (boardArray[cursoryToRow(cursory) + 1, cursorxToColumn(cursorx)] != "." && boardArray[cursoryToRow(cursory) + 2, cursorxToColumn(cursorx)] == "." && playerMoveType != "step")
                            {

                                Console.SetCursorPosition(2 * cursorx, cursory);
                                boardArray[cursoryToRow(cursory), cursorxToColumn(cursorx)] = ".";
                                Console.WriteLine(".");
                                yboundry++;
                                cursory += 2;
                                playerMoveType = "jump";
                            }
                        }
                        else
                        {
                            if (boardArray[cursoryToRow(cursory) + 1, cursorxToColumn(cursorx)] == "." && cursorx == oldcursorx && playerMoveType != "jump")
                            {
                                if (yboundry == 1)
                                {
                                    continue;
                                }
                                Console.SetCursorPosition(2 * cursorx, cursory);
                                boardArray[cursoryToRow(cursory), cursorxToColumn(cursorx)] = ".";
                                Console.WriteLine(".");
                                yboundry++;
                                playerMoveType = "step";
                                cursory++;
                            }
                        }
                    }

                }

                Console.SetCursorPosition(2 * cursorx, cursory);    // refresh X (current position)
                Console.WriteLine("x");

                if (cki.Key == ConsoleKey.X && boardArray[cursoryToRow(cursory), cursorxToColumn(cursorx)] == ".")
                {
                    userTurn = false;

                    boardArray[cursoryToRow(cursory), cursorxToColumn(cursorx)] = "x";
                    chooser = 0;

                    break;
                }

                Thread.Sleep(25);     // sleep 50 ms

            }
        }

        public static bool computerJumpOrStep()
        {
            bool flag = false;
            int pieceVal = 0;
            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 6; x++)
                {
                    if (boardArray[y, x] == "o" && x * y >= pieceVal && ((boardArray[y, x + 1] != "." && boardArray[y, x + 2] == ".") || boardArray[y + 1, x] != "." && boardArray[y + 2, x] == "."))
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == true) break;
            }
            if (flag == true)
            {
                return true;
            }
            else return false;
        }

        public static void computerJumpMove()
        {
            int piecex = 0;
            int piecey = 0;
            int pieceVal = 0;
            bool availableMove = true;
            string moveType = "both";
            Random rnd = new Random();
            int randSelect = 8;




            while (true)
            {

                while (true)
                {
                    randSelect = rnd.Next(0, 9);
                    piecex = computerPieces[randSelect, 0];
                    piecey = computerPieces[randSelect, 1];
                    if (piecex <= 5 && piecey <= 5)
                    {
                        break;
                    }
                    else continue;
                }
                piecex = computerPieces[randSelect, 0];
                piecey = computerPieces[randSelect, 1];
                if (((boardArray[piecey, piecex + 1] != "." && boardArray[piecey, piecex + 2] == ".") || (boardArray[piecey + 1, piecex] != "." && boardArray[piecey + 2, piecex] == ".")))
                {
                    break;
                }
                else continue;
            }

            piecex = computerPieces[randSelect, 0];
            piecey = computerPieces[randSelect, 1];



            while (availableMove)
            {

                if (piecex < 6 && piecey < 6)
                {
                    if ((boardArray[piecey, piecex + 1] != "." && boardArray[piecey, piecex + 2] == ".") && (boardArray[piecey + 1, piecex] != "." && boardArray[piecey + 2, piecex] == "."))
                    {
                        moveType = "both";

                    }
                    else if (boardArray[piecey, piecex + 1] != "." && boardArray[piecey, piecex + 2] == ".")
                    {
                        moveType = "right";

                    }
                    else if (boardArray[piecey + 1, piecex] != "." && boardArray[piecey + 2, piecex] == ".")
                    {
                        moveType = "down";

                    }
                }
                else if (piecex == 7 && piecey < 6)
                {
                    if (boardArray[piecey + 1, piecex] != "." && boardArray[piecey + 2, piecex] == ".")
                    {
                        moveType = "down";

                    }
                }
                else if (piecex < 6 && piecey == 7)
                {
                    if (boardArray[piecey, piecex + 1] != "." && boardArray[piecey, piecex + 2] == ".")
                    {
                        moveType = "right";

                    }
                }
                else if (piecex == 6 && piecey < 6)
                {
                    if (boardArray[piecey + 1, piecex] != "." && boardArray[piecey + 2, piecex] == ".")
                    {
                        moveType = "down";

                    }
                }
                else if (piecey == 6 && piecex < 6)
                {
                    if (boardArray[piecey, piecex + 1] != "." && boardArray[piecey, piecex + 2] == ".")
                    {
                        moveType = "right";

                    }
                }
                else if ((piecex == 6 && piecey == 6) || (piecex == 7 && piecey == 7))
                {
                    compMoveChooser = false;
                    break;
                }


                switch (moveType)
                {
                    case "both":
                        int downOrRight = rnd.Next(0, 2);
                        switch (downOrRight)
                        {
                            case 0://down
                                boardArray[piecey + 2, piecex] = boardArray[piecey, piecex];
                                boardArray[piecey, piecex] = ".";
                                computerPieces[randSelect, 1] += 2;
                                piecey = piecey + 2;
                                break;
                            case 1://right
                                boardArray[piecey, piecex + 2] = boardArray[piecey, piecex];
                                boardArray[piecey, piecex] = ".";
                                computerPieces[randSelect, 0] += 2;
                                piecex = piecex + 2;
                                break;
                        }
                        break;

                    case "down":
                        boardArray[piecey + 2, piecex] = boardArray[piecey, piecex];
                        boardArray[piecey, piecex] = ".";
                        computerPieces[randSelect, 1] += 2;
                        piecey = piecey + 2;
                        break;
                    case "right":
                        boardArray[piecey, piecex + 2] = boardArray[piecey, piecex];
                        boardArray[piecey, piecex] = ".";
                        computerPieces[randSelect, 0] += 2;
                        piecex = piecex + 2;
                        break;

                }

                boardPrinter(boardArray);
                Thread.Sleep(200);
                if (piecex < 6 && piecey < 6)
                {
                    if (boardArray[piecey, piecex] == "o" && piecex * piecey > pieceVal && ((boardArray[piecey, piecex + 1] != "." && boardArray[piecey, piecex + 2] == ".") || boardArray[piecey + 1, piecex] != "." && boardArray[piecey + 2, piecex] == "."))
                    {
                        availableMove = true;
                        continue;
                    }
                    else
                    {
                        availableMove = false;
                        break;
                    }
                }
                else if ((piecex == 6 || piecex == 7) && piecey < 6)
                {
                    if (boardArray[piecey, piecex] == "o" && piecex * piecey > pieceVal && boardArray[piecey + 1, piecex] != "." && boardArray[piecey + 2, piecex] == ".")
                    {
                        availableMove = true;
                        continue;
                    }
                    else
                    {
                        availableMove = false;
                        break;
                    }
                }
                else if ((piecey == 6 || piecey == 7) && piecex < 6)
                {
                    if (boardArray[piecey, piecex] == "o" && piecex * piecey > pieceVal && boardArray[piecey, piecex + 1] != "." && boardArray[piecey, piecex + 2] == ".")
                    {
                        availableMove = true;
                        continue;
                    }
                    else
                    {
                        availableMove = false;
                        break;
                    }
                }
                else
                {
                    availableMove = false;
                    break;
                }

            }
            userTurn = true;

        }




        //computer step move 
        public static void computerStepMove()
        {
            int randSelect = 8;
            int piecex = 0;
            int piecey = 0;
            int piece = piecex * piecey;
            bool compmove = false;
            string moveType = "both";
            Random rnd = new Random();
            //deciding which piece gooing to move
            while (!compmove)
            {
                randSelect = rnd.Next(0, 9);
                piecex = computerPieces[randSelect, 0];
                piecey = computerPieces[randSelect, 1];

                if (boardArray[piecey, piecex] == "o")
                {
                    if (piecex < 7 && piecey < 7)
                    {
                        if (boardArray[piecey, piecex + 1] == "." && boardArray[piecey + 1, piecex] == ".")
                        {
                            moveType = "both";
                            compmove = true;
                        }
                        else if (boardArray[piecey, piecex + 1] != "." && boardArray[piecey + 1, piecex] == ".")
                        {
                            moveType = "down";
                            compmove = true;
                        }
                        else if (boardArray[piecey, piecex + 1] == "." && boardArray[piecey + 1, piecex] != ".")
                        {
                            moveType = "right";
                            compmove = true;
                        }

                    }
                    else if (piecex == 7 && piecey < 7)
                    {
                        if (boardArray[piecey + 1, piecex] == ".")
                        {
                            moveType = "down";
                            compmove = true;
                        }


                    }
                    else if (piecex < 7 && piecey == 7)
                    {
                        if (boardArray[piecey, piecex + 1] == ".")
                        {
                            moveType = "right";
                            compmove = true;
                        }

                    }
                }


                else continue;

            }
            switch (moveType)
            {
                case "both":
                    int downOrRight = rnd.Next(0, 2);
                    switch (downOrRight)
                    {
                        case 0://down
                            boardArray[piecey + 1, piecex] = boardArray[piecey, piecex];
                            boardArray[piecey, piecex] = ".";
                            computerPieces[randSelect, 1]++;
                            userTurn = true;
                            break;
                        case 1://right
                            boardArray[piecey, piecex + 1] = boardArray[piecey, piecex];
                            boardArray[piecey, piecex] = ".";
                            computerPieces[randSelect, 0]++;
                            userTurn = true;
                            break;
                    }
                    break;
                case "down":
                    boardArray[piecey + 1, piecex] = boardArray[piecey, piecex];
                    boardArray[piecey, piecex] = ".";
                    computerPieces[randSelect, 1]++;
                    userTurn = true;
                    break;
                case "right":
                    boardArray[piecey, piecex + 1] = boardArray[piecey, piecex];
                    boardArray[piecey, piecex] = ".";
                    computerPieces[randSelect, 0]++;
                    userTurn = true;
                    break;
            }

        }



        public static bool winnerChecker()
        {
            int PLAYERPIECENUMBER = 9;
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (boardArray[y, x] == "x")
                    {
                        PLAYERPIECENUMBER--;
                    }
                    if (PLAYERPIECENUMBER == 0)
                    {
                        Console.SetCursorPosition(22, 2);
                        Console.WriteLine("HUMANITY HAS WON!");
                        Thread.Sleep(50000);
                        return true;
                    }
                }
            }
            int COMPUTERPICENUMBER = 9;

            for (int y = 7; y > 4; y--)
            {
                for (int x = 7; x > 4; x--)
                {
                    if (boardArray[y, x] == "o")
                    {
                        COMPUTERPICENUMBER--;
                    }
                    if (COMPUTERPICENUMBER == 0)
                    {
                        Console.SetCursorPosition(30, 5);
                        Console.WriteLine("COMPUTER HAS WON!");
                        Thread.Sleep(50000);
                        return true;
                    }
                }
            }
            return false;
        }


        static void Main(string[] args)
        {

            framePrinter();
            boardPrinter(boardArray);
            bool isGameEnded = false;
            while (true)
            {
                Console.SetWindowSize(50, 13);                       
                if (userTurn == true)
                {
                    if (chooser == 0)
                    {
                        playerMove();

                    }
                    else
                    {
                        playerZMove();

                    }
                }


                else if (userTurn == false)
                {


                    compMoveChooser = computerJumpOrStep();

                    if (compMoveChooser == false)
                    {
                        computerStepMove();


                    }
                    else if (compMoveChooser == true)
                    {
                        computerJumpMove(); 


                    }

                }


                boardPrinter(boardArray);

                isGameEnded = winnerChecker();
                if (isGameEnded == true)
                {
                    break;

                }
                else
                {
                    continue;
                }

            }



        }
    }

}
