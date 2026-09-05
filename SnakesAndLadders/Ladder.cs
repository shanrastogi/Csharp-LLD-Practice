public class Ladder : Obstacle
{
    public Ladder(int startPosition, int endPosition) : base(startPosition, endPosition)
    {
        if (startPosition >= endPosition)
        {
            throw new ArgumentException("Ladder's start position must be less than its end position.");
        }
    }

    public override ObstacleType GetObstacleType()
    {
        return ObstacleType.Snake;
    }
}