# CoffeeTable

How far it is from a desk to the coffee machine. 
The office layout is laid out in a grid - every cell is either a wall, which is impossible or a desk (employees can walk through other employees desks to get to a coffee machine). 
Here is a sample office: 
X : Wall 
- : desk 
C : Coffee 
--C- 
-XX- 
XC-- 
This office has 3 rows and 4 columns. 
The distance from the desk at (2,1) is 3, since it can reach the coffee machine in row 1 in three steps. The function I'd like you to implement has this signature. This is a .NET signature, so you may adjust it if you are using a different language: public static int DistanceToCoffee(int numRows, int numColumns, Tuple<int, int> DeskLocation, List<Tuple<int, int>> coffeeLocations, List<Tuple<int, int>> walls)
