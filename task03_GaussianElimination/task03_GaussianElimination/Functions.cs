using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace task03_GaussianElimination
{
    public static class Functions
    {
        public static void getCofactor(int[,] mat, int[,] temp, int p, int q, int n)
        {
            int i = 0, j = 0;
            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < n; col++)
                {
                    if (row != p && col != q)
                    {
                        temp[i, j++] = mat[row, col];
                        if (j == n - 1)
                        {
                            j = 0;
                            i++;
                        }
                    }
                }
            }
        }
        public static double determinantOfMatrix(int[,] mat, int n)
        {
            double D = 0;
            if (n == 1)
            {
                return mat[0, 0];
            }
            int[,] temp = new int[n, n];
            int sign = 1;
            for (int f = 0; f < n; f++)
            {
                getCofactor(mat, temp, 0, f, n);
                D += sign * mat[0, f]
                     * determinantOfMatrix(temp, n - 1);
                sign = -sign;
            }
            return D;
        }

        public static void display(int[,] mat, int row, int col)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                    Console.Write(mat[i, j]);

                Console.Write("\n");
            }
        }

        public static int[,] Change(int[,] mat, int[] roots, int j, int n)
        {
            for (int i = 0; i < n; i++)
            {
                mat[i, j] = roots[i];
            }
            return mat;
        }


        public static double[] FindRootsSimple(int n, int[,] matrix, int[] arr)
        {
            double[] result = new double[n];
            double main_det = Functions.determinantOfMatrix(matrix, n);
            for (int i = 0; i < n; i++)
            {
                int[,] temp = matrix.Clone() as int[,];
                int[,] new_mat = Functions.Change(temp, arr, i, n);
                result[i] = Functions.determinantOfMatrix(new_mat, n) / main_det;
            }
            return result;
        }

        public static double[] CalcRoots(int i_beg, int i_end, int n, int[,] matrix, int[] arr, double[] result)
        {
            double main_det = Functions.determinantOfMatrix(matrix, n);
            for (int i = i_beg; i < i_end; i++)
            {
                int[,] temp = matrix.Clone() as int[,];
                int[,] new_mat = Functions.Change(temp, arr, i, n);
                result[i] = Functions.determinantOfMatrix(new_mat, n) / main_det;
            }
            return result;
        }

        public static double[] FindRootsThread(int n, int[,] matrix, int[] arr, int col_threads, double[] result)
        {
            int col = (n / col_threads) + 1;
            Thread[] threads = new Thread[col_threads];

            for (int i = 0; i < col_threads; i++)
            {
                int ii = i;
                threads[i] = new Thread(() => { result = CalcRoots(ii * col, Math.Min(n, (ii + 1) * col), n, matrix, arr, result); });
                threads[i].Start();
            }
            for (int i = 0; i < col_threads; i++)
            {
                threads[i].Join();
            }
            return result;
        }
    }
}
