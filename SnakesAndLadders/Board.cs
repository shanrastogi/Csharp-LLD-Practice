public class Board
{
    private int size;
    private List<Cell> cells = new List<Cell>();
    public Board(int size)
    {
        this.size = size;

        for (int i = 0; i <= size + 1; i++)
        {
            cells.Add(new Cell(i));
        }
    }

    public bool AddObstacle(Obstacle obstacle)
    {
        Cell startCell = cells[obstacle.StartPosition];
        Cell endCell = cells[obstacle.EndPosition];

        if (startCell.HasObstacle() || endCell.HasObstacle())
        {
            return false; // Cannot add obstacle if start or end cell already has an obstacle
        }

        startCell.SetObstacle(obstacle);
        return true;
    }

    public int getNewPosition(Player player, int offset)
    {
        int newPosition = player.Position + offset;
        if (newPosition > size)
        {
            return player.Position; // Player stays in the same position if they exceed the board size
        }

        Cell newCell = cells[newPosition];
        int finalPosition = newCell.getFinalPosition();

        if (finalPosition < newPosition)
        {
            Console.WriteLine($"{player.Name} encountered a {newCell.Obstacle!.GetObstacleType()} and moved to position {finalPosition}.");
        }
        else if (finalPosition > newPosition)
        {
            Console.WriteLine($"{player.Name} encountered a {newCell.Obstacle!.GetObstacleType()} and moved to position {finalPosition}.");
        }
        else
        {
            Console.WriteLine($"{player.Name} moved to position {finalPosition}.");
        }
        return finalPosition;
    }

    public void PrintBoard()
    {
        for (int i = 1; i <= size; i++)
        {
            Cell cell = cells[i];
            if (cell.HasObstacle())
            {
                Console.WriteLine($"Cell {i}: {cell.Obstacle!.GetObstacleType()} to {cell.Obstacle!.EndPosition}");
            }
            else
            {
                Console.WriteLine($"Cell {i}: No obstacle");
            }
        }
    }



}