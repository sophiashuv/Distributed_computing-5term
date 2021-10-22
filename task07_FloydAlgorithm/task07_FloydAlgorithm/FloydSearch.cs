using System;
using System.Threading;

namespace task07_FloydAlgorithm
{
    public class FloydSearch
    {
        public int Size;
        static int INF = 100000;
        public int[,] Matrix;
        public int[,] BeginMatrix;

        public FloydSearch(int size)
        {
            Size = size;
            Matrix = new int[size, size];
            BeginMatrix = new int[size, size];
            GenerateMatrix();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    BeginMatrix[i, j] = Matrix[i, j];
                }
            }
        }

        public void GenerateMatrix()
        {
            Random random = new Random();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    int num = random.Next(1, 200);
                    if (i == j)
                    {
                        Matrix[i, j] = 0;
                    }
                    else
                    {
                        Matrix[i, j] = (num % 38 != 0 || num % 10 != 0) ? num : INF;
                    }
                }
            }
        }

        public void PrintMatrix()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (Matrix[i, j] == INF)
                        Console.Write("# ");
                    else
                        Console.Write(Matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void FloydAlgorithm(int begin, int stop)
        {
            for (int k = 0; k < Size; k++)
            {
                for (int i = 0; i < Size; i++)
                {
                    for (int j = begin; j < stop; j++)
                    {
                        Matrix[i, j] = Math.Min(Matrix[i, j], Matrix[i, k] + Matrix[k, j]);
                    }
                }
            }
        }

        public void FloydInParallel(int numberOfThreads)
        {
            if (numberOfThreads > Size)
                numberOfThreads = Size;

            int count = Size / numberOfThreads;
            Thread[] threads = new Thread[numberOfThreads];

            for (int t = 0; t < numberOfThreads; t++)
            {
                int start = t * count;
                int stop = 0;

                if (t + 1 == numberOfThreads)
                    stop = Size;
                else
                    stop = start + count;

                threads[t] = new Thread(() => { FloydAlgorithm(start, stop); });
            }

            foreach (var thread in threads)
                thread.Start();

            foreach (var thread in threads)
                thread.Join();
        }

        public void Reset()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Matrix[i, j] = BeginMatrix[i, j];
                }
            }
        }
    }
}
