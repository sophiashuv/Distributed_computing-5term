using System;
using System.Data.SqlClient;
using System.Threading;

namespace task01_MatrixSum
{
    public class Matrix
    {
        public uint Rows { get; set; }
        public uint Columns { get; set; }

        public int[,] MatrixArray { get; set; }

        public int this[long i, long j]
        {
            get
            {
                if (i < 0 || i >= Rows)
                    throw new IndexOutOfRangeException("Row is out of range");
                if (j < 0 || j >= Columns)
                    throw new IndexOutOfRangeException("Column is out of range");
                return MatrixArray[i, j];
            }
            set
            {
                if (i < 0 || i >= Rows)
                    throw new IndexOutOfRangeException("Row is out of range");
                if (j < 0 || j >= Columns)
                    throw new IndexOutOfRangeException("Column is out of range");
                MatrixArray[i, j] = value;
            }
        }

        public Matrix(uint rows, uint columns, bool rand = true)
        {
            Rows = rows;
            Columns = columns;
            MatrixArray = new int[Rows, Columns];
            if (rand)
            {
                GenerateRandomMatrix(MatrixArray, Rows, Columns);
            }
        }

        private static void GenerateRandomMatrix(int[,] matrixArray, uint rows, uint columns)
        {
            Random randNum = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrixArray[i, j] = randNum.Next(1000);
                }
            }
        }

        public static Matrix AddMatrixLoops(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows || a.Columns != b.Columns)
                throw new ArgumentException("Matrix must have same dimensions");

            var resultMatrix = new Matrix(a.Rows, b.Columns, false);
            for (int i = 0; i < resultMatrix.Rows; i++)
            {
                for (int j = 0; j < resultMatrix.Columns; j++)
                {
                    resultMatrix[i, j] = a[i, j] + b[i, j];
                }
            }
            return resultMatrix;
        }

        public static Matrix AddMatrixThreads(Matrix a, Matrix b, uint amount)
        {
            Thread[] threads = new Thread[amount];
            uint start = 0;
            var step = (a.Columns * a.Rows) / (amount);
            var end = step + 1;
            var resultMatrix = new Matrix(a.Rows, b.Columns, false);
            for (int i = 0; i < amount; i++)
            {
                var st = start;
                var en = end;

                threads[i] = new Thread(() =>
                {
                    for (var j = st; j <= en; j++)
                    {
                        resultMatrix[j / a.Columns, j % a.Columns] =
                            a[j / a.Columns, j % a.Columns] + b[j / a.Columns, j % a.Columns];
                    }
                });

                threads[i].Start();
                // Thread.Sleep(2000);
                start = end + 1;
                end = i == amount - 2 ? a.Rows * b.Columns - 1 : end + step;
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
            return resultMatrix;
        }

        public override string ToString()
        {
            string toReturn = " ";
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                    toReturn += MatrixArray[i, j].ToString() + " ";
                toReturn += "\n ";
            }
            return toReturn;
        }
    }
}
