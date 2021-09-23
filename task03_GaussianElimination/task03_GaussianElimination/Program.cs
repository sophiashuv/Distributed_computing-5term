using System;

namespace task03_GaussianElimination
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter n: ");
            uint n = uint.Parse(Console.ReadLine());

            Matrix matrix = new Matrix(n);

            double[] res;
            var watch1 = System.Diagnostics.Stopwatch.StartNew();
            res = Matrix.GetRootsLoop(matrix);
            watch1.Stop();

         
            Console.WriteLine($"Time with 1 thread: {watch1.Elapsed}");


            for (uint i = 2; i <= 10; i += 2)
            {
                for (int j = 0; j < n; j++)
                {
                    res[j] = 0;
                }

                var watch2 = System.Diagnostics.Stopwatch.StartNew();
                res = Matrix.GetRootsThread(matrix, i, res);
                watch2.Stop();

                Console.WriteLine($"Time with  {i} threads: {watch2.Elapsed}");
            }
        }
    }
}
