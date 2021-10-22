using System;
using System.Threading;

namespace task06_DijikstasAllgorithm
{
    public class DijikstraSearch
    {
        public int N { get; set; }
        public int StartVertex { get; set; }
        public int[] ResDist { get; set; }
        public bool[] CheckVertex { get; set; }
        public int[,] Matrix { get; set; }

        public DijikstraSearch(int n, int startVertex)
        {
            N = n;
            StartVertex = startVertex;
            ResDist = new int[N];
            CheckVertex = new bool[N];
            for (int i = 0; i < N; i++)
            {
                ResDist[i] = int.MaxValue;
                CheckVertex[i] = false;
            }
            ResDist[startVertex] = 0;

            GenerateRandomMatrix();
        }

        public void GenerateRandomMatrix()
        {
            int ind = 0;
            Matrix = new int[N, N];
            Random random = new Random();
            for (int i = 0; i < N; i++)
            {
                for (int j = ind; j < N; j++)
                {
                    int num = random.Next(0, 20);
                    if (i != j)
                    {
                        Matrix[i, j] = num;
                        Matrix[j, i] = num;
                    }
                    else
                    {
                        Matrix[i, j] = 0;
                    }
                }
                ind++;
            }
        }

        public void PrintMatrix()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(Matrix[i, j] + "  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void PrintResult()
        {
            Console.Write($"Dist from Vertex {StartVertex} \n");
            for (int i = 0; i < N; i++)
                Console.Write($"{i} \t\t {ResDist[i]}\n");
        }

        public int FindMin()
        {
            int min = int.MaxValue;
            int min_index = -1;

            for (int i = 0; i < N; i++)
            {
                if (CheckVertex[i] == false && ResDist[i] <= min)
                {
                    min = ResDist[i];
                    min_index = i;
                }
            }
            return min_index;
        }

        public void DijkstraAlgorithm(int start, int stop)
        {
            for (int count = start; count < stop; count++)
            {
                int u = FindMin();
                CheckVertex[u] = true;

                for (int vertex = 0; vertex < N; vertex++)
                {
                    if (!CheckVertex[vertex] && Matrix[u, vertex] != 0 && ResDist[u] != int.MaxValue && ResDist[u] + Matrix[u, vertex] < ResDist[vertex])
                    {
                        ResDist[vertex] = ResDist[u] + Matrix[u, vertex];
                    }
                }
            }
        }

        public void DijkstraAlgorithmInParallel(int numberOfThreads)
        {
            if (numberOfThreads > N)
                numberOfThreads = N;

            int count = N / numberOfThreads;
            Thread[] threads = new Thread[numberOfThreads];

            for (int t = 0; t < numberOfThreads; t++)
            {
                int start = t * count;
                int stop = 0;

                if (t + 1 == numberOfThreads)
                    stop = N;
                else
                    stop = start + count;
                threads[t] = new Thread(() => { DijkstraAlgorithm(start, stop); });
            }

            foreach (var thread in threads)
                thread.Start();
            foreach (var thread in threads)
                thread.Join();
        }

        public void Reset()
        {
            ResDist = new int[N];
            CheckVertex = new bool[N];
            for (int i = 0; i < N; i++)
            {
                ResDist[i] = int.MaxValue;
                CheckVertex[i] = false;
            }
            ResDist[StartVertex] = 0;
        }

    }
}
