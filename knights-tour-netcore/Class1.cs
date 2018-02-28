using System;
using System.Collections.Generic;
using System.Text;

namespace knights_tour_netcore
{
    class Class1
    {
        private static int finalLevel;
        private static bool Solve(Board b)
        {
            for (int i = 0; i < 8; i++)
            {
                if (b.Level == finalLevel)
                {
                    return true;
                }
                else if (Step(b, i))
                {
                    //Step(b, i);
                    if (Solve(b)) return true;
                }
            }
            return false;
        }

        private static bool Step(Board b, int r)
        {
            StringBuilder builder = new StringBuilder();
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
                Console.WriteLine(builder.ToString());
                b.Backtrack();
                return false;
            }
            Console.WriteLine(builder.ToString());
            if (ms == MoveState.OK)
                return true;
            return false;
            //b.Print(Console.WriteLine);
        }
    }
}
