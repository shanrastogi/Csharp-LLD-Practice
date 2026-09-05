public class ObstacleFactory
{
    public static Obstacle CreateObstacle(ObstacleType type, int startPosition, int endPosition)
    {
        switch (type)
        {
            case ObstacleType.Snake:
                return new Snake(startPosition, endPosition);
            case ObstacleType.Ladder:
                return new Ladder(startPosition, endPosition);
            default:
                throw new ArgumentException("Invalid obstacle type");
        }
    }
}