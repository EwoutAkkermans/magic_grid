namespace magic_grid;

class Program
{
    static void Main()
    {
        Console.WriteLine("enter the size of the grid (n): ");
        if (int.TryParse(Console.ReadLine(), out int n))
        {
            if (n > 2)
            {
                if (n % 2 == 0)
                {
                    Console.WriteLine("n must be odd"); //non-essential rule
                    return;
                }
                else
                {
                    int[,] grid = GenerateGrid(n);
                    GenerateHtmlFile(grid, "grid.html");
                    Console.WriteLine("HTML file generated with size {0}", n);
                    return;
                }
            }
            else
            {
                Console.WriteLine("n must be greater than 2");
            }
        }
    }

    static int[,] GenerateGrid(int n)
    {
        int[,] grid = new int[n, n];

        //set start values, these can also be random
        int row = 0;
        int column = n/2;

        int counter = 1;
        grid[row, column] = counter++;

        while (counter <= n * n)
        {
            int rowCheck = row - 1 < 0 ? n - 1 : row - 1;
            int columnCheck = column + 1 >= n ? 0 : column + 1;

            if (grid[rowCheck, columnCheck] == 0)
            {   //move up and right
                grid[rowCheck, columnCheck] = counter++;
                row = rowCheck;
                column = columnCheck;
            }
            else
            {   //move down one
                int rowDown = row + 1 >= n ? 0 : row + 1;
                grid[rowDown, column] = counter++;
                row = rowDown;
            }
        }
        return grid;
    }

    static void GenerateHtmlFile(int[,] grid, string filename) //entirely written by copilot
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine("<!DOCTYPE html>");
            writer.WriteLine("<html>");
            writer.WriteLine("<head>");
            writer.WriteLine("<title>Magic Grid</title>");
            writer.WriteLine("<style>");
            writer.WriteLine("table { border-collapse: collapse; }");
            writer.WriteLine("td { border: 1px solid black; width: 50px; height: 50px; text-align: center; }");
            writer.WriteLine("</style>");
            writer.WriteLine("</head>");
            writer.WriteLine("<body>");
            writer.WriteLine("<table>");

            for (int row = 0; row < grid.GetLength(0); row++)
            {
                writer.WriteLine("<tr>");

                for (int column = 0; column < grid.GetLength(1); column++)
                {
                    writer.WriteLine("<td>{0}</td>", grid[row, column]);
                }

                writer.WriteLine("</tr>");
            }

            writer.WriteLine("</table>");
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
        }
    }
}