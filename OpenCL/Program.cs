using System;
using System.Diagnostics;
using System.Threading;
using OpenCLTemplate;

namespace OpenCL
{
    class Program
    {
        static CLCalc.Program.Kernel multiplyMatrix;

        static string programSource = @"__kernel void 
                MatrixMul(__global float * result,      
                      __global float * matrix1,
                      __global float * matrix2,
                      __global int * q)
            {
                
                int i = get_global_id(0);
                int j = get_global_id(1);

                int p = get_global_size(0);
                int r = get_global_size(1);
                result[i + p * j] = 0;
                int QQ = q[0];
                for (int k = 0; k < QQ; k++)
                {
                    result[i + p * j] += matrix1[i + p * k] * matrix2[k + QQ * j];
                }
        }";


        public static void FillMatrix(float[,] matrix, int rows, int columns, int lower, int upper)
        {
            Random random = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = random.Next(lower, upper);
                }
            }
        }

        public static void PrintMatrix(float[,] matrix, int rows, int columns)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }


        public static float[] ConvertMatrixToArray(float[,] matrix, int rows, int columns)
        {
            float[] result = new float[rows * columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result[i + rows * j] = matrix[i, j];
                }
            }
            return result;
        }

        public static float[,] ConvertArrayToMatrix(float[] array, int rows, int columns)
        {
            float[,] result = new float[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result[i, j] = array[i + rows * j];
                }
            }
            return result;
        }

        public static void ResetMatrix(float[,] resultM, int rows, int colums)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < colums; j++)
                {
                    resultM[i, j] = 0;
                }
            }
        }

        public static void MatrixMultiply(float[,] firstM, float[,] secondM, float[,] resultM, int firstRows, int firstColumns, int secondRows, int secondColumns)
        {
            for (int i = 0; i < secondRows; i++)
            {
                for (int j = 0; j < firstColumns; j++)
                {
                    for (int k = 0; k < secondColumns; k++)
                    {
                        resultM[i, j] += firstM[i, k] * secondM[k, j];
                    }
                }
            }
        }

        public static void MultiplieMatrixThreads(float[,] firstM, float[,] secondM, float[,] resultM, int firstRows, int firstColumns, int secondRows, int secondColumns, uint amount)
        {
            Thread[] threads = new Thread[amount];
            int start = 0;
            int step = (int)((firstColumns * secondRows) / (amount));
            int end = step + 1;

            for (int i = 0; i < amount; i++)
            {
                var st = start;
                var en = end;

                threads[i] = new Thread(() =>
                {
                    for (var j = st; j <= en; j++)
                    {
                        for (int l = 0; l < firstColumns; l++)
                        {
                            resultM[j / firstColumns, j % firstColumns] +=
                                firstM[j / firstColumns, l] * secondM[l, j % firstColumns];
                        }
                    }
                });

                threads[i].Start();
                // Thread.Sleep(2000);
                start = end + 1;
                end = i == amount - 2 ? firstRows * secondColumns - 1 : end + step;
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
        }


        static void Main(string[] args)
        {
            CLCalc.InitCL();
            CLCalc.Program.Compile(programSource);
            multiplyMatrix = new CLCalc.Program.Kernel("MatrixMul");

            int firstRows = 1000;
            int firstColumns = 1000;

            int secondRows = 1000;
            int secondColumns = 1000;

            float[,] matrix1 = new float[firstRows, firstColumns];
            float[,] matrix2 = new float[secondRows, secondColumns];

            FillMatrix(matrix1, firstRows, firstColumns, 0, 10);
            FillMatrix(matrix2, secondRows, secondColumns, 0, 10);

            //Console.WriteLine("\n  MATRIX 1");
            //PrintMatrix(matrix1, firstRows, firstColumns);
            //Console.WriteLine("  MATRIX 2");
            //PrintMatrix(matrix2, secondRows, secondColumns);

            Stopwatch time = Stopwatch.StartNew();

            float[] arrMatrix1 = ConvertMatrixToArray(matrix1, firstRows, firstColumns);
            float[] arrMatrix2 = ConvertMatrixToArray(matrix2, secondRows, secondColumns);
            float[] arrRes = new float[firstRows * secondColumns];
            int[] arrQ = new int[1] { firstColumns };

            CLCalc.Program.Variable varRes = new CLCalc.Program.Variable(arrRes);
            CLCalc.Program.Variable varMatrix1 = new CLCalc.Program.Variable(arrMatrix1);
            CLCalc.Program.Variable varMatrix2 = new CLCalc.Program.Variable(arrMatrix2);
            CLCalc.Program.Variable varQ = new CLCalc.Program.Variable(arrQ);

            CLCalc.Program.Variable[] arguments = new CLCalc.Program.Variable[4] { varRes, varMatrix1, varMatrix2, varQ };
            int[] workers = new int[2] { firstRows, secondColumns };

            multiplyMatrix.Execute(arguments, workers);
            varRes.ReadFromDeviceTo(arrRes);
            varRes.Dispose();

            float[,] matrixResult = ConvertArrayToMatrix(arrRes, firstRows, secondColumns);

            time.Stop();
            Console.WriteLine($"\nOpenCl Mltiplication: {time.Elapsed.Minutes} minutes {time.Elapsed.Seconds} seconds and {time.Elapsed.Milliseconds} milliseconds");

            //Console.WriteLine("\n\tRESULT");
            //PrintMatrix(matrixResult, firstRows, secondColumns);
            //Console.WriteLine();

            ResetMatrix(matrixResult, secondRows, firstColumns);
            time = Stopwatch.StartNew();
            MatrixMultiply(matrix1, matrix2, matrixResult, firstRows, firstColumns, secondRows, secondColumns);
            time.Stop();
            Console.WriteLine($"\nSimple Mltiplication: {time.Elapsed.Minutes} minutes {time.Elapsed.Seconds} seconds and {time.Elapsed.Milliseconds} milliseconds");

            //Console.WriteLine("\n\tRESULT");
            //PrintMatrix(matrixResult, firstRows, secondColumns);
            //Console.WriteLine();

            ResetMatrix(matrixResult, secondRows, firstColumns);
            uint amount = 10;
            time = Stopwatch.StartNew();
            MultiplieMatrixThreads(matrix1, matrix2, matrixResult, firstRows, firstColumns, secondRows, secondColumns, amount);
            time.Stop();
            Console.WriteLine($"\n{amount} Threads Mltiplication: {time.Elapsed.Minutes} minutes {time.Elapsed.Seconds} seconds and {time.Elapsed.Milliseconds} milliseconds");

            //Console.WriteLine("\n\tRESULT");
            //PrintMatrix(matrixResult, firstRows, secondColumns);
            //Console.WriteLine();
            Console.ReadKey();
        }
    }
}
