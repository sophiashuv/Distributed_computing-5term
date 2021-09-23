using System;
using System.Threading;

namespace task03_GaussianElimination
{
    public class Matrix
    {
        public uint N { get; set; }

        public int[,] MatrixArray { get; set; }

        public int[] Roots { get; set; }

        public int this[long i, long j]
        {
            get
            {
                if (i < 0 || i >= N)
                    throw new IndexOutOfRangeException("Row is out of range");
                if (j < 0 || j >= N)
                    throw new IndexOutOfRangeException("Column is out of range");
                return MatrixArray[i, j];
            }
            set
            {
                if (i < 0 || i >= N)
                    throw new IndexOutOfRangeException("Row is out of range");
                if (j < 0 || j >= N)
                    throw new IndexOutOfRangeException("Column is out of range");
                MatrixArray[i, j] = value;
            }
        }

        public int this[long i]
        {
            get
            {
                if (i < 0 || i >= N)
                    throw new IndexOutOfRangeException("Row is out of range");
                return Roots[i];
            }
            set
            {
                if (i < 0 || i >= N)
                    throw new IndexOutOfRangeException("Row is out of range");
                Roots[i] = value;
            }
        }

        public Matrix(uint n, bool rand = true)
        {
            N = n;
            MatrixArray = new int[N, N];
            Roots = new int[N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    MatrixArray[i, j] = 0;
                }
                Roots[i] = 0;
            }
            if (rand)
            {
                GenerateRandomMatrix(MatrixArray, N);
                GenerateRandomArray(Roots, N);
            }
        }

        public Matrix(uint n, int[,] matrix, int[] roots)
        {
            N = n;
            MatrixArray = new int[N, N];
            Roots = new int[N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    MatrixArray[i, j] = matrix[i, j];
                }
                Roots[i] = roots[i];
            }
        }

        private static void GenerateRandomMatrix(int[,] matrixArray, uint n)
        {
            Random randNum = new Random();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrixArray[i, j] = randNum.Next(10);
                }
            }
        }

        private static void GenerateRandomArray(int[] roots, uint n)
        {
            Random randNum = new Random();
            for (int i = 0; i < n; i++)
            {
                roots[i] = randNum.Next(10);
            }
        }

        public static void getSubMatrix(Matrix matrix, Matrix temp, int p, int q)
        {
            int i = 0, j = 0;
            for (int row = 0; row < matrix.N; row++)
            {
                for (int col = 0; col < matrix.N; col++)
                {
                    if (row != p && col != q)
                    {
                        temp[i, j++] = matrix[row, col];
                        if (j == matrix.N - 1)
                        {
                            j = 0;
                            i++;
                        }
                    }
                }
            }
        }
        public static double getMatrixDeterminant(Matrix matrix, uint n)
        {
            double determinant = 0;
            if (n == 1)
            {
                return matrix[0, 0];
            }
            Matrix temp = new Matrix(matrix.N);
            int sign = 1;
            for (int i = 0; i < n; i++)
            {
                getSubMatrix(matrix, temp, 0, i);
                determinant += sign * matrix[0, i]
                     * getMatrixDeterminant(temp, n - 1);
                sign = -sign;
            }
            return determinant;
        }

        public static double[] GetRootsLoop(Matrix matrix)
        {
            double[] result = new double[matrix.N];
            double determinant = getMatrixDeterminant(matrix, matrix.N);
            for (uint i = 0; i < matrix.N; i++)
            {
                Matrix newMatrixt = new Matrix(matrix.N, matrix.MatrixArray.Clone() as int[,], matrix.Roots);
                for (int j = 0; j < newMatrixt.N; j++)
                {
                    newMatrixt[j, i] = newMatrixt[j];
                }

                result[i] = getMatrixDeterminant(newMatrixt, matrix.N) / determinant;
            }
            return result;
        }

        public static double[] CalculateSubRoots(uint begin, uint end, Matrix matrix, double[] result)
        {
            double determinant = getMatrixDeterminant(matrix, matrix.N);
            for (uint i = begin; i < end; i++)
            {
                Matrix newMatrixt = new Matrix(matrix.N, matrix.MatrixArray.Clone() as int[,], matrix.Roots);
                for (int j = 0; j < newMatrixt.N; j++)
                {
                    newMatrixt[j, i] = newMatrixt[j];
                }
                result[i] = getMatrixDeterminant(newMatrixt, matrix.N) / determinant;
            }
            return result;
        }

        public static double[] GetRootsThread(Matrix matrix, uint amount, double[] result)
        {
            var step = (matrix.N / amount) + 1;
            Thread[] threads = new Thread[amount];

            for (uint i = 0; i < amount; i++)
            {
                uint i1 = i;
                threads[i] = new Thread(() => { result = CalculateSubRoots(i1 * step, Math.Min(matrix.N, (i1 + 1) * step), matrix, result); });
                threads[i].Start();
            }
            for (int i = 0; i < amount; i++)
            {
                threads[i].Join();
            }
            return result;
        }


        public override string ToString()
        {
            string toReturn = " ";
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                    toReturn += MatrixArray[i, j].ToString() + " ";
                toReturn += "\n ";
            }
            return toReturn;
        }
    }
}
