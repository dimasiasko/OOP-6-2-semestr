using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Завдання_1
{
    class Program
    {
        static void Main(string[] args)
        {

            
            try
            {

            }
            catch (Exception e)
            { 
                Console.WriteLine(e.Message);
            }
            
        }
    }
    public class Saver
    {
        private Saver() { }
       
        public static Saver Instance { get; } = new Saver();

        
        private static StreamWriter sw = new StreamWriter(Environment.CurrentDirectory);
        private static StreamReader sr = new StreamReader(Environment.CurrentDirectory);

        
        private static readonly List<int[,]> matrices = new List<int[,]>();
        public List<int[,]> Matrices => matrices;


        public void Save()
        {
            using (sw)
            {
                string result;
                try
                {
                    for (int i = 0; i < Matrices.Count; i++)
                    {
                        sw.WriteLine(Matrices[i]);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
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

        public int ElemAt(int x, int y)
        {
            return _matrix[x, y];
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
            try
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
            catch (Exception)
            {
                Console.WriteLine("Умножение не возможно! Количество столбцов первой матрицы не равно количеству строк второй матрицы.");
                return new Matrix(0, 0);
            }
            
        }
        public int Determinant()
        {
            int sum = 0;
            for (int k = 0; k < _rows; k++)
            {
                int first = 1;
                int second = 1;
                for (int i = 0; i < _rows; i++)
                {
                    first *= _matrix[i, (i + k) % _rows];
                    second *= _matrix[i, (_rows - 1 - i + k) % _rows];
                }
                sum += first;
                sum -= second;
            }
            return sum;
        }
        public Matrix Transpon()
        {
            int[,] result = new int[_columns, _rows];
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                    result[j, i] = _matrix[i, j];
            }
            return new Matrix(result);
        }
        private int[,] GetMinorMatrix(int x, int y)
        {
            int[,] result = new int[_rows - 1, _columns - 1];
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                    if ((i != x) && (j != y))
                        result[i > x ? i - 1 : i, j > y ? j - 1 : j] = _matrix[i, j];
            }
            
            return result;
        }
        private Matrix CoMatrix()
        {
            int[,] co_matrix = new int[_rows, _columns];
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    Matrix mir = new Matrix(GetMinorMatrix(i, j));
                    co_matrix[i, j] = mir.Determinant();
                }
            }
            
            return new Matrix(co_matrix);
        }
        public Matrix GetObrMatrix()
        {
            return CoMatrix().Transpon() / Determinant();
        }
        public static Matrix operator / (Matrix matrix, int value)
        {
            int[,] result = new int[matrix._rows, matrix._columns];
            for (int i = 0; i < matrix._rows; i++)
            {
                for (int j = 0; j < matrix._columns; j++)
                    result[i, j] = matrix.ElemAt(i, j) / value;
            }
            return new Matrix(result);
        }
    }
}
