public class Snake : Obstacle
{
    public Snake(int startPosition, int endPosition) : base(startPosition, endPosition)
    {
        if (startPosition <= endPosition)
        {
            throw new ArgumentException("Snake's start position must be greater than its end position.");
        }
    }

    public override ObstacleType GetObstacleType()
    {
        return ObstacleType.Snake;
    }
}