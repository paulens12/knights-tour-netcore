using System;
using System.Collections.Generic;
using System.Text;

namespace knights_tour_netcore
{
    public class Move
    {
        public MoveState MoveState { get; private set; }
        public int U { get; private set; }
        public int V { get; private set; }

        public Move(MoveState ms, int u, int v)
        {
            MoveState = ms;
            U = u + 1;
            V = v + 1;
        }
    }

    public enum MoveState
    {
        OK,
        OutOfBounds,
        Occupied
    }

    public class Board
    {
        private Stack<int> moves = new Stack<int>();

        private Tuple<int, int>[] transform = new Tuple<int, int>[8]
        {
            new Tuple<int, int>(2,1),
            new Tuple<int, int>(1,2),
            new Tuple<int, int>(-1,2),
            new Tuple<int, int>(-2,1),
            new Tuple<int, int>(-2,-1),
            new Tuple<int, int>(-1,-2),
            new Tuple<int, int>(1,-2),
            new Tuple<int, int>(2,-1)
        };

        public int Size { get; private set; }
        private int[][] board; //board[x][y]
        private int u, v;
        public int U
        {
            get
            {
                return u + 1;
            }
        }
        public int V
        {
            get
            {
                return v + 1;
            }
        }
        public int Level { get; private set; }

        public Board(int size, int u, int v)
        {
            if (size < 5 || size > 7)
                throw new ArgumentOutOfRangeException("Board size", "Size can be 5 - 7.");
            if (u > size || u < 1)
                throw new ArgumentOutOfRangeException("Initial x coordinate", "Initial coordinates must be inside the board.");
            if (v > size || v < 1)
                throw new ArgumentOutOfRangeException("Initial y coordinate", "Initial coordinates must be inside the board.");
            Size = size;
            board = new int[size][];
            for(int i=0; i<size; i++)
            {
                board[i] = new int[size];
                for(int j=0; j<size; j++)
                {
                    board[i][j] = 0;
                }
            }
            this.u = u - 1;
            this.v = v - 1;
            board[this.u][this.v] = 1;
            Level = 2;
        }

        public void Print(Action<string> writeLine)
        {
            StringBuilder buffer = new StringBuilder("    -", Size * Size * 4);
            for (int i = 0; i < Size; i++)
            {
                buffer.Append("----");
            }
            buffer.Append(">X,U\n    ");

            for (int i = 1; i <= Size; i++)
            {
                buffer.AppendFormat("{0,4}", i);
            }

            string spacer = buffer.Append("\n").ToString();
            buffer.Clear();
            //writeLine(spacer);

            buffer.Append("Y,V^\n");

            for (int i = Size - 1; i >= 0; i--)
            {
                //buffer.Clear();
                buffer.AppendFormat("{0, 3}|", i+1);
                for (int j = 0; j < Size; j++)
                {
                    buffer.AppendFormat("{0,4}", board[j][i]);
                }
                //writeLine(buffer.Append("|").ToString());
                buffer.Append("\n");
            }

            //writeLine(spacer);
            writeLine(buffer.Append(spacer).ToString());
        }

        public int Get(int x, int y)
        {
            if (x > Size || x < 1 || y > Size || y < 1)
                throw new IndexOutOfRangeException();
            return board[x-1][y-1];
        }

        public MoveState TryMove(int r, out int newU, out int newV)
        {
            newU = U + transform[r].Item1;
            newV = V + transform[r].Item2;
            MoveState ms = GetMoveState(newU - 1, newV - 1);
            if(ms == MoveState.OK)
            {
                u += transform[r].Item1;
                v += transform[r].Item2;
                moves.Push(r);
                board[u][v] = Level;
                Level++;
            }
            return ms;
        }

        public void Backtrack()
        {
            //do
            //{
                board[u][v] = 0;
                int step = moves.Pop();
                u -= transform[step].Item1;
                v -= transform[step].Item2;
                Level--;
            //}
            //while (moves.Peek() == 7);
        }

        private MoveState GetMoveState(int u, int v)
        {
            if (u >= Size || v >= Size || u < 0 || v < 0)
                return MoveState.OutOfBounds;
            if (board[u][v] != 0)
                return MoveState.Occupied;
            return MoveState.OK;
        }
    }
}
