using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternFacade
{
    public class Generator
    {
        private static readonly Random random = new Random();

        public List<int> Generate(int count)
        {
            return Enumerable.Range(0, count)
              .Select(_ => random.Next(1, 6))
              .ToList();
        }
    }

    public class Splitter
    {
        public List<List<int>> Split(List<List<int>> array)
        {
            var result = new List<List<int>>();

            var rowCount = array.Count;
            var colCount = array[0].Count;

            // get the rows
            for (int r = 0; r < rowCount; ++r)
            {
                var theRow = new List<int>();
                for (int c = 0; c < colCount; ++c)
                    theRow.Add(array[r][c]);
                result.Add(theRow);
            }

            // get the columns
            for (int c = 0; c < colCount; ++c)
            {
                var theCol = new List<int>();
                for (int r = 0; r < rowCount; ++r)
                    theCol.Add(array[r][c]);
                result.Add(theCol);
            }

            // now the diagonals
            var diag1 = new List<int>();
            var diag2 = new List<int>();
            for (int c = 0; c < colCount; ++c)
            {
                for (int r = 0; r < rowCount; ++r)
                {
                    if (c == r)
                        diag1.Add(array[r][c]);
                    var r2 = rowCount - r - 1;
                    if (c == r2)
                        diag2.Add(array[r][c]);
                }
            }

            result.Add(diag1);
            result.Add(diag2);

            return result;
        }
    }

    public class Verifier
    {
        public bool Verify(List<List<int>> array)
        {
            if (!array.Any()) return false;

            var expected = array.First().Sum();

            var isMagic =  array.All(t => t.Sum() == expected) 
                && IsColumnTotalEqual(array)
                && IsLeftDiagonalEqual(array)
                && IsRightDiagonalEqual(array); 
            if (isMagic)
                Console.WriteLine($"Expected: {expected}");
            return isMagic;
        }

        private bool IsColumnTotalEqual(List<List<int>> array)
        {
            var columns = array.First().Count() - 1;
            var expected = array.First().Sum();
          
            for (var column = 0 ; column < columns; ++column)
            {
                var total = 0;
                foreach(var row in array)
                {
                    total += row[column];
                }
                if (total != expected)
                    return false;
            }
            return true;

        }
        private bool IsLeftDiagonalEqual(List<List<int>> array)
        {
            var columns = array.First().Count() - 1;
            var expected = array.First().Sum();
            var total = 0;
            for (var column = 0; column <= columns; ++column)
            {
                total += array[column][column];
            }
            return total == expected;

        }
        private bool IsRightDiagonalEqual(List<List<int>> array)
        {
            var columns = array.First().Count() - 1;
            var expected = array.First().Sum();
            var total = 0;
            var rowCount = 0;
            
            for (var column = columns; column > -1 ; column--)
            {
                total += array[rowCount][column];
                rowCount++;
            }
            return total == expected;

        }
    }

    public class MagicSquareGenerator
    {
        public List<List<int>> Generate(int size)
        {
            var rows = new List<List<int>>();
           
            var matrix = new Generator().Generate(size * size);
            var count = 0;
            for (int row = 0; row < size; row++)
            {
                var columns = new List<int>();
                for (int column = 0; column < size; column++)
                {
                    columns.Add(matrix[count]);
                    count++;
                }
                rows.Add(columns);
            }
            return rows;
        }
    }

    internal class Program
    {

        static void Main(string[] args)
        {   
            //Func< int, List<List<int>>> gen = delegate(int len)
            //{
            //    MagicSquareGenerator generator = new MagicSquareGenerator();
            //    return generator.Generate(len);
            //};

            Func<int, List<List<int>>> gen = delegate (int len)
            {
                return new List<List<int>> { 
                    new List<int> { 2,7,6},
                    new List<int> { 9,5,1},
                    new List<int> { 4,3,8},
                };
            };

            var size = 5;
            var matrix = gen(size);
            var verifier = new Verifier();
            var isMagic = verifier.Verify(matrix);

            while (!isMagic)
            {
                matrix = gen(size);
                isMagic = verifier.Verify(matrix);
            }
            foreach (var row in matrix)
            {
                foreach(var column in row)
                {
                    Console.Write($"  {column}");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Hello World!");
        }
    }
}
