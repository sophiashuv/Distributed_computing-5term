using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace task03_GaussianElimination
{
    public class Matrix
    {
        static Mutex mutexObj = new Mutex();
        private readonly int _rows;
        private readonly int _columns;
        private readonly List<int> _matrix;
        private static List<int> _matrix2;
        private static List<int> _resultMatrix;
        private static int _threads;

        public (int, int) Size => (_rows, _columns);

        public Matrix(int n, int m, IReadOnlyCollection<int> matrix = null)
        {
            _rows = n;
            _columns = m;
            _matrix = new List<int>(n * m);
            if (matrix != null)
            {
                _matrix = matrix.ToList();
            }
        }

        public void Initialize(int min = 1, int max = 100)
        {
            var random = new Random();
            for (var i = 0; i < _rows * _columns; i++)
            {
                _matrix.Add(random.Next(min, max));
            }
        }

        public int this[int x, int y]
        {
            get => _matrix[x * _columns + y];
            set => _matrix[x * _columns + y] = value;
        }

        public override string ToString()
        {
            var matrix = string.Empty;
            for (var i = 0; i < _rows * _columns; i++)
            {
                matrix += _matrix[i] + " ";
                if (i % _columns == _columns - 1)
                {
                    matrix += "\n";
                }
            }

            return matrix;
        }

        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            //if (matrix1.Size != matrix2.Size)
            //{
            //    throw new ArgumentOutOfRangeException();
            //}

            var sw = new Stopwatch();
            var rows = matrix1.Size.Item1;
            var columns = matrix1.Size.Item2;
            var matrix = new List<int>();

            sw.Start();
            for (var i = 0; i < rows * columns; i++)
            {
                matrix.Insert(i, matrix1._matrix[i] + matrix2._matrix[i]);
            }
            sw.Stop();

            Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");
            return new Matrix(rows, columns, matrix);
        }

        public Matrix AddMatrix(Matrix matrix, int numOfThreads)
        {
            //if (matrix.Size != Size)
            //{
            //    throw new ArgumentOutOfRangeException();
            //}

            var threadsArray = new Thread[numOfThreads];
            for (var i = 0; i < numOfThreads; i++)
            {
                threadsArray[i] = new Thread(AddingInThread);
            }

            _matrix2 = matrix._matrix;
            _resultMatrix = new List<int>(_columns * _rows);
            _threads = numOfThreads;
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < numOfThreads; i++)
            {
                threadsArray[i].Start(i);
                threadsArray[i].Join();
            }
            sw.Stop();
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");
            return new Matrix(_rows, _columns, _resultMatrix);
        }

        private void AddingInThread(object threadId)
        {
            mutexObj.WaitOne();
            var id = (int)threadId;
            for (var i = id * _matrix.Count / _threads; i < (id + 1) * _matrix.Count - 1 / _threads; i++)
            {
                _resultMatrix.Insert(i, _matrix[i] + _matrix2[i]);
            }
            mutexObj.ReleaseMutex();
        }
    }
}
