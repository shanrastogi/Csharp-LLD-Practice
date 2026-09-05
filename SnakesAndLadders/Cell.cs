public class Cell
{
    public int Position { get; private set; }
    public Obstacle? Obstacle { get; private set; }

    public Cell(int position, Obstacle? obstacle = null)
    {
        Position = position;
        Obstacle = obstacle;
    }

    public bool HasObstacle()
    {
        return Obstacle != null;
    }

    public int getFinalPosition()
    {
        if (HasObstacle())
        {
            return Obstacle!.movePlayer();
        }
        return Position;
    }

    public void SetObstacle(Obstacle obstacle)
    {
        Obstacle = obstacle;
    }
}