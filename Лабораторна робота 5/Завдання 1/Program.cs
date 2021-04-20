using System;

namespace Завдання_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix matrixB = new Matrix(5,5);
            matrixB.RandomMatrix();
            Matrix matrixA = new Matrix(5, 5);
            matrixA.RandomMatrix();

            Matrix matrixE = matrixA * matrixB;
            matrixE.Output();

        }
    }

    class Matrix
    {
        private int _rows;
        private int _columns;
        private int[,] _matrix;
        public Matrix(int rows, int columns)
        {
            this._rows = rows;
            this._columns = columns;
        }
        public Matrix(int[,] matrix)
        {
            this._rows = matrix.GetLength(0);
            this._columns = matrix.GetLength(1);
            this._matrix = matrix;
        }

        public Matrix(Matrix matrix)
        {
            this._rows = matrix._rows;
            this._columns = matrix._columns;
        }
        
        public void RandomMatrix()
        {
            _matrix = new int[_rows, _columns];
            Random random = new Random();

            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                    _matrix[i, j] = random.Next(-20, 20);
            }

        }

        public void Output()
        {
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                    Console.Write(_matrix[i, j] + "\t");

                Console.WriteLine();
            }
        }
        public static Matrix operator *(Matrix A, Matrix B)
        {
            Matrix multiMatrix = new Matrix(A._rows, B._columns);
            multiMatrix._matrix = new int[A._rows, B._columns];
            for (int i = 0; i < A._rows; i++)
            {
                for (int j = 0; j < B._columns; j++)
                {
                    multiMatrix._matrix[i, j] = 0;
                    for (int k = 0; k < A._columns; k++)
                        multiMatrix._matrix[i, j] += A._matrix[i, k] * B._matrix[k, j];
                }
            }
            return multiMatrix;
        }

    }
}
