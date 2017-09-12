using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coffeetable
{
    class Program
    {
        static void Main(string[] args)
        {
            // Wall: x; Coffeetable: c; Desk: -  . The location is:
            // - - c -
            // - x x -
            // x c - - 
            List<Tuple<int, int>> cofferLocations = new List<Tuple<int, int>>();
            cofferLocations.Add(new Tuple<int, int>(0, 3));
            //cofferLocations.Add(new Tuple<int, int>(2, 1));
            List<Tuple<int, int>> wallLoations = new List<Tuple<int, int>>();
            wallLoations.Add(new Tuple<int, int>(0, 2));
            wallLoations.Add(new Tuple<int, int>(2, 3));
            wallLoations.Add(new Tuple<int, int>(2, 0));
            Tuple<int,int> deskLocation = new Tuple<int, int>(1, 0);
            Tuple<int,int> deskLocation2 = new Tuple<int, int>(2, 2);
            Console.WriteLine($"------------------");
            int distance = DistanceToCoffee(4, 5, deskLocation, cofferLocations, wallLoations);
            Console.WriteLine($"Shortest distance of [1,0 ]is {distance}");
            Console.WriteLine($"------------------");
            int distance2 = DistanceToCoffee(4, 5, deskLocation2, cofferLocations, wallLoations);
            Console.WriteLine($"Shortest distance of [2,2 ]is {distance2}");
            Console.WriteLine($"------------------");
        }

        public static int DistanceToCoffee(int numRows, int numColumns, Tuple<int,int> deskLocation, List<Tuple<int,int>> coffeeLocation,List<Tuple<int,int>> wall)
        {
            if(numRows<=0 || numColumns <= 0 || coffeeLocation.Count==0)
            {
                return 0;
            }
            //create an array to represent the locations. 0 means coffeetable, row*column+1 means wall, row*column means desk.
            // - - c -                  12 12  0 12
            // - x x -        =>        12 13 13 12
            // x c - -                  13 0  12 12
            int[,] layout = new int[numRows, numColumns];
            int matrix = numRows * numColumns;
            int beyond = matrix + 1;
            for(int i = 0; i < numRows; i++)
            {
                for(int j=0;j<numColumns; j++)
                {
                    Tuple<int,int> temp = Tuple.Create(i, j);
                    if (coffeeLocation.Contains(temp))
                    {
                        layout[i, j] = 0;
                    } 
                    else if (wall.Contains(temp))
                    {
                        layout[i, j] = beyond;
                    }
                    else
                    {
                        layout[i, j] = matrix;
                    }

                }
            }
            Console.WriteLine("---The Locations is:----");
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    if(i==deskLocation.Item1 && j == deskLocation.Item2)
                    {
                        Console.Write($"\t ({layout[i, j]})");
                    }
                    else
                    {
                        Console.Write($"\t {layout[i, j]}");
                    }

                }
                Console.Write("\n");

            }
            Console.WriteLine("----The Calculate process is: ----");
            int distance = SubInstance(numRows, numColumns, deskLocation.Item1, deskLocation.Item2, layout);
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    if (i == deskLocation.Item1 && j == deskLocation.Item2)
                    {
                        Console.Write($"\t ({layout[i, j]})");
                    }
                    else
                    {
                        Console.Write($"\t {layout[i, j]}");
                    }

                }
                Console.Write("\n");

            }
            return distance;
        }

        public static int SubInstance(int numRows, int numColumns,int row, int column, int[,] layout)
        {
            int returnnum;
            //beyond border
            if (row<0 || row>=numRows || column<0 || column >= numColumns)
            {
                returnnum = numRows * numColumns + 2;
            }
            //else if(layout[row,column]==0 || layout[row, column] == numColumns * numRows + 1)
            else if(layout[row,column]!=numRows*numColumns)
            {
                returnnum = layout[row, column];
            }
            else
            {
                layout[row, column] = -1;
                int top, bottom, left, right;
                top = left = right = bottom = numRows * numColumns;
                if ((row - 1) >= 0 && layout[row - 1, column] != -1)
                {
                    top = SubInstance(numRows, numColumns, row - 1, column, layout);
                }
                if ((row + 1) < numRows && layout[row + 1, column] != -1)
                {
                    bottom = SubInstance(numRows, numColumns, row + 1, column, layout);
                }
                if ((column - 1) >= 0 && layout[row, column - 1] != -1)
                {
                    left = SubInstance(numRows, numColumns, row, column - 1, layout);
                }
                if ((column + 1) < numColumns && layout[row, column + 1] != -1)
                {
                    right = SubInstance(numRows, numColumns, row, column+1, layout);
                }
                returnnum= Math.Min(Math.Min(top, bottom), Math.Min(left, right)) + 1;
                layout[row, column] = returnnum;
            }
            Console.WriteLine($"[{row},{column}] returns: {returnnum}");
            return returnnum;
        }
    }
}
