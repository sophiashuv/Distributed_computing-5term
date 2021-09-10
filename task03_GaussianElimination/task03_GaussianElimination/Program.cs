using System;

namespace task03_GaussianElimination
{
    static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Enter num of rows:");
                var n = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter num of columns:");
                var m = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter num of threads:");
                var threads = Convert.ToInt32(Console.ReadLine());
                if (threads >= n * m)
                {
                    Console.WriteLine("Number of threads should not be bigger than number of elements in matrices.");
                    return;
                }
                if (n < 1 || m < 1)
                {
                    throw new ArgumentException();
                }

                var matrix1 = new Matrix(n, m);
                matrix1.Initialize();

                var matrix2 = new Matrix(n, m);
                matrix2.Initialize();
                //Console.WriteLine("Do you want to print matrices? 1 - Yes, else - no");
                //var printMatrices = Console.ReadLine() == "1";
                //if (printMatrices)
                //{
                //    Console.WriteLine($"\nMatrix 1:\n{matrix1}");
                //    Console.WriteLine($"\nMatrix 2:\n{matrix2}");
                //}

                Console.WriteLine("Add in 1 thread:");
                var oneThreadResultMatrix = matrix1 + matrix2;


                Console.WriteLine($"Add in {threads} threads:");
                var multiThreadResultMatrix = matrix1.AddMatrix(matrix2, threads);

                //if (printMatrices)
                //{
                //    Console.WriteLine($"\nResult matrix:\n{oneThreadResultMatrix}");
                //    Console.WriteLine($"Result matrices are equal: {oneThreadResultMatrix.ToString() == multiThreadResultMatrix.ToString()}");
                //}
            }
            catch
            {
                Console.WriteLine("Value should be positive integer.");
            }

            Console.ReadKey();
        }
    }
}
