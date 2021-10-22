using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace task06_DijikstasAllgorithm
{
    class Program
    {
        static int n;
        static int startVertex;

        static void Main(string[] args)
        {
            Console.Write("Enter the number of vertices: ");
            n = Convert.ToInt32(Console.ReadLine());

            Console.Write($"Enter start vertex(from 0 to {n - 1}): ");
            startVertex = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter number of threads: ");
            var numberOfThreads = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            DijikstraSearch d_a = new DijikstraSearch(n, startVertex);

            DateTime start, end;

            start = DateTime.Now;
            d_a.DijkstraAlgorithm(0, n);
            end = DateTime.Now;
            //d_a.PrintResult();

            var time = end - start;
            Console.WriteLine($"Time Without threads: {time}");
            Console.WriteLine();


            for (int i = 2; i <= numberOfThreads; i += 1)
            {
                d_a.Reset();
                start = DateTime.Now;
                d_a.DijkstraAlgorithmInParallel(i);
                //d_a.PrintResult();
                end = DateTime.Now;
                var time2 = end - start;
                Console.WriteLine($"Time Threads-{i}: {time2}");
                Console.WriteLine($"Eficence Threads-{i}: {time/(i * time2)}\n");
            }
            Console.ReadKey();
        }
    }
}
