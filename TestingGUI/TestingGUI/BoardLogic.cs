using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TestingGUI
{
    class BoardLogic
    {
        public List<List<int>> RowsNumbers { get; } = new();
        public List<List<int>> ColumnsNumbers { get; } = new();
        public int[,] board { get; }
        public BoardLogic(int rows, int columns)
        {
            board = MakeBoard(rows, columns);
            for(int i = 0; i< rows; i++)
            {
                RowsNumbers.Add(GetNumbers(GetRow(board,i)));
            }
            for (int i = 0; i < columns; i++)
            {
                ColumnsNumbers.Add(GetNumbers(GetColumn(board, i)));
            }

        }
        private  int[,] MakeBoard(int n, int m)
        {
            var rand = new Random();
            int[,] board = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    board[i, j] = rand.Next(2);

                }

            }
            return board;
        }
        private  int[] GetColumn(int[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                    .Select(x => matrix[x, columnNumber])
                    .ToArray();
        }

        private  int[] GetRow(int[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }
        private List<int> GetNumbers(int[] group)
        {
            List<int> listOfNumbers = new();
            foreach (string series in String.Join("", group).Split("0"))
            {
                int length = series.Length;
                if (length > 0)
                    listOfNumbers.Add(length);
            }
            return listOfNumbers;
        }
        public bool CheckBoard(DataGridView dgv)
        {
            int[,] GUIboard = new int[this.board.GetLength(0), this.board.GetLength(1)];
            for (int i = 0; i < board.GetLength(1); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    if( dgv.Rows[i].Cells[j].Style.BackColor.Equals(Color.Black))
                    {
                        GUIboard[i, j] = 1;
                    }
                    else
                    {
                        GUIboard[i, j] = 0;
                    }

                }

            }
            int k = 0;
            foreach(List<int> row in RowsNumbers)
            {
                if(!GetNumbers(GetRow(GUIboard, k)).SequenceEqual(row)){
                    return false;
                }
                k++;
            }
            k = 0;
            foreach (List<int> col in ColumnsNumbers)
            {
                if (!GetNumbers(GetColumn(GUIboard, k)).SequenceEqual(col))
                {
                    return false;
                }
                k++;
            }
            return true;
        }
    }
}
