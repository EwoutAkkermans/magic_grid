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
                    Console.WriteLine("n must be odd");
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

        int startcolumn = n/2;
        int startrow = 0;

        grid[startrow, startcolumn] = 1;
        int counter = 2;

        int row = startrow;
        int column = startcolumn;

        while (counter <= n * n)
        {
            //save start position with row and column
            //check cell with checkFillPosition at row - 1, column + 1
            //if checkFillPosition row is out of bounds, set move row to n - 1
            //if checkFillPosition column is out of bounds, set move column to 0
            //if checkFillPosition cell is not filled, set value of checkFillPosition cell to the value of counter + 1
            //if checkFillPosition cell is filled, make new downPosition at start position with row - 1, and same column
            //if downPosition row is out of bounds, set move row to >>0<< (this was the problem)
            //set value of downPosition cell to the value of counter + 1

            startrow = row;
            startcolumn = column;

            int checkFillPositionRow = startrow - 1;
            int checkFillPositionColumn = startcolumn + 1;

            if (!RowValid(checkFillPositionRow))
            {
                checkFillPositionRow = n - 1;
            }

            if (!ColumnValid(checkFillPositionColumn))
            {
                checkFillPositionColumn = 0;
            }

            if (!CellFilled(checkFillPositionRow, checkFillPositionColumn))
            {
                grid[checkFillPositionRow, checkFillPositionColumn] = counter;
                row = checkFillPositionRow;
                column = checkFillPositionColumn;
            }
            else
            {
                int downPositionRow = startrow + 1;
                int downPositionColumn = startcolumn;

                if (!RowValid(downPositionRow))
                {
                    downPositionRow = 0;
                }

                grid[downPositionRow, downPositionColumn] = counter;
                row = downPositionRow;
                column = downPositionColumn;
            }

            counter++;
        }

        bool RowValid(int checkRow)
        {
            return checkRow >= 0 && checkRow < n;
        }
        

        bool ColumnValid(int checkColumn)
        {
            return checkColumn >= 0 && checkColumn < n;
        }

        bool CellFilled(int checkFilledRow, int checkFilledColumn)
        {
            return grid[checkFilledRow, checkFilledColumn] != 0;
        }

        return grid;
    }

    static void GenerateHtmlFile(int[,] grid, string filename)
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