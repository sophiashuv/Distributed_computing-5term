using System;

namespace task2_MatrixMultiplication
{
    class Program
    {
        static void Main(string[] args)
        {
            uint rows1 = 9;
            uint columns1 = 7;
            uint rows2 = 7;
            uint columns2 = 19;
            uint threds = 2;

            var firstMatrix = new Matrix(rows1, columns1);
            var secondMatrix = new Matrix(rows2, columns2);

            Console.WriteLine($" First Matrix: \n{firstMatrix}\n");
            Console.WriteLine($" Second Matrix: \n{secondMatrix}\n");

            var res1 = Matrix.MultiplieMatrixLoops(firstMatrix, secondMatrix);
            var res2 = Matrix.MultiplieMatrixThreads(firstMatrix, secondMatrix, threds);


            Console.WriteLine($" AddMatrixLoop: \n{res1}\n");
            Console.WriteLine($" AddMatrixThreads: \n{res2}\n");

            uint size = 1000;
            firstMatrix = new Matrix(size, size);
            secondMatrix = new Matrix(size, size);

            Console.WriteLine($" rows: {size}\n columns: {size}");
            var watch3 = System.Diagnostics.Stopwatch.StartNew();
            var res3 = Matrix.MultiplieMatrixLoops(firstMatrix, secondMatrix);
            watch3.Stop();
            Console.WriteLine($" One thread: {watch3.Elapsed}");
            for (uint i = 2; i <= 20; i += 2)
            {
                watch3 = System.Diagnostics.Stopwatch.StartNew();
                res3 = Matrix.MultiplieMatrixThreads(firstMatrix, secondMatrix, i);
                watch3.Stop();
                Console.WriteLine($" {i} threads: {watch3.Elapsed}");

            }
            for (uint i = 100; i <= 1000; i += 100)
            {
                watch3 = System.Diagnostics.Stopwatch.StartNew();
                res3 = Matrix.MultiplieMatrixThreads(firstMatrix, secondMatrix, i);
                watch3.Stop();
                Console.WriteLine($" {i} threads : {watch3.Elapsed}");
            }
        }
    }
}
