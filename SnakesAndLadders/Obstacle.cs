public abstract class Obstacle
{
    public int StartPosition { get; private set; }
    public int EndPosition { get; private set; }

    public Obstacle(int startPosition, int endPosition)
    {
        StartPosition = startPosition;
        EndPosition = endPosition;
    }

    public int movePlayer() { return EndPosition; }

    public abstract ObstacleType GetObstacleType();
}