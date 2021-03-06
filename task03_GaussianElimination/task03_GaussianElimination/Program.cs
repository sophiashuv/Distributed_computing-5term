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
            var time1 = watch1.Elapsed.Seconds;


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
                var time2 = watch2.Elapsed.Seconds;
                var ef = time1/(time2*i);

                Console.WriteLine($"Time with  {i} threads: {watch2.Elapsed}");
                Console.WriteLine($"Eficient with  {i} threads: {ef}");
            }
        }
    }
}
