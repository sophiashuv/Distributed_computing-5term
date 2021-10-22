using System;
using System.Threading;

namespace task07_FloydAlgorithm
{
    class Program
    {
        static int size;
        static int INF = 100000;

        static void Main(string[] args)
        {
            Console.Write("Size of matrix: ");
            size = Convert.ToInt32(Console.ReadLine());

            Console.Write("Number of threads: ");
            var numberOfThreads = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            var f_a = new FloydSearch(size);

            DateTime start, end;

            start = DateTime.Now;
            f_a.FloydAlgorithm(0, size);
            end = DateTime.Now;
            var time = end - start;
            Console.WriteLine($"Time Without threads: {time}");
            Console.WriteLine();


            for (int i = 2; i <= numberOfThreads; i += 1)
            {
                f_a.Reset();
                start = DateTime.Now;
                f_a.FloydInParallel(i);
                end = DateTime.Now;
                var time2 = end - start;
                Console.WriteLine($"Time Threads-{i}: {time2}");
                Console.WriteLine($"Eficence Threads-{i}: {time / (i * time2)}\n");
            }
            Console.ReadKey();
        }
    }
}
