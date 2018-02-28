using System;
using System.Text;

namespace knights_tour_netcore
{
    class Program
    {
        private static int finalLevel;
        private static int step = 1;
        // arguments: n u v
        // n - size of board
        // u - initial x coordinate (1-based)
        // v - initial y coordinate (1-based)
        static void Main(string[] args)
        {
            Board b = null;
            try
            {
                b = new Board(int.Parse(args[0]), int.Parse(args[1]), int.Parse(args[2]));
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Arguments: [board size] [initial x coordinate] [initial y coordinate]\nAll arguments are required");
                return;
            }
            catch(FormatException)
            {
                Console.WriteLine("Please enter valid numbers");
                return;
            }
            catch(ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Invalid argument: " + e.Message);
                return;
            }
            b.Print(Console.WriteLine);

            finalLevel = b.Size * b.Size + 1;

            PrintData(b);
            /*
            bool solved = false;
            while(!solved)
            {
                //solve...
                for(int i=0; i<8; i++)
                {
                    Step(b, i);
                }
                solved = true;
            }*/
            Solve(b);
            b.Print(Console.WriteLine);
        }

        private static bool Solve(Board b)
        {
            if (step >= 13830)
                Console.Write("");
            for (int r = 0; r < 8; r++)
            {
                if (b.Level == finalLevel)
                    return true;
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("{0,5}) ", step++);
                for (int i = 0; i < b.Level - 2; i++)
                    builder.Append("-");
                string stateText = "";
                int newU, newV;
                int lvl = b.Level;
                MoveState ms = b.TryMove(r, out newU, out newV);
                switch (ms)
                {
                    case MoveState.Occupied:
                        stateText = "Thread";
                        break;
                    case MoveState.OutOfBounds:
                        stateText = "Out";
                        break;
                    case MoveState.OK:
                        stateText = "Free";
                        break;
                }
                builder.AppendFormat("R{0}. U={1}, V={2}, L={3}. {4}.", r + 1, newU, newV, lvl, stateText);
                if (ms == MoveState.OK)
                {
                    builder.AppendFormat(" Board[{0},{1}]:={2}.", newU, newV, lvl);
                }
                else if (r == 7)
                {
                    builder.Append(" Backtrack.");
                    //Console.WriteLine(builder.ToString());
                    //b.Backtrack();
                    //continue;
                }//else
                Console.WriteLine(builder.ToString());
                if(ms == MoveState.OK)
                    if(Solve(b)) return true;
            }
            //Console.WriteLine("backtrack");
            b.Backtrack();
            return false;
        }


        
        private static void PrintData(Board b)
        {
            Console.WriteLine("Part 1. Data");
            Console.WriteLine("1) Board {0}x{0}.", b.Size);
            Console.WriteLine("2) Initial position X={0}, Y={1}, L={2}.\n", b.U, b.V, b.Level);
        }
    }
}
