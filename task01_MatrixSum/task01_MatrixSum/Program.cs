using System;
using System.Threading;

namespace task01_MatrixSum
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            uint rows = 5;
            uint columns = 11;
            uint threds = 10;

            var firstMatrix = new Matrix(rows, columns);
            var secondMatrix = new Matrix(rows, columns);

            Console.WriteLine($" First Matrix: \n{firstMatrix}\n");
            Console.WriteLine($" Second Matrix: \n{secondMatrix}\n");

            var res1 = Matrix.AddMatrixLoops(firstMatrix, secondMatrix);
            var res2 = Matrix.AddMatrixThreads(firstMatrix, secondMatrix, threds);


            Console.WriteLine($" AddMatrixLoop: \n{res1}\n");
            Console.WriteLine($" AddMatrixThreads: \n{res2}\n");


            rows = 10000;
            columns = 10000;
            firstMatrix = new Matrix(rows, columns);
            secondMatrix = new Matrix(rows, columns);

            Console.WriteLine($" rows: {rows}\n columns: {columns}");
            var watch3 = System.Diagnostics.Stopwatch.StartNew();
            var res3 = Matrix.AddMatrixLoops(firstMatrix, secondMatrix);
            watch3.Stop();
            Console.WriteLine($" One thread: {watch3.Elapsed}");
            for (uint i = 2; i <= 10; i += 1)
            {
                watch3 = System.Diagnostics.Stopwatch.StartNew();
                res3 = Matrix.AddMatrixThreads(firstMatrix, secondMatrix, i);
                watch3.Stop();
                Console.WriteLine($" {i} threads: {watch3.Elapsed}");

            }
            for (uint i = 100; i <= 1000; i += 100)
            {
                watch3 = System.Diagnostics.Stopwatch.StartNew();
                res3 = Matrix.AddMatrixThreads(firstMatrix, secondMatrix, i);
                watch3.Stop();
                Console.WriteLine($" {i} threads : {watch3.Elapsed}");
            }

        }
    }
}
