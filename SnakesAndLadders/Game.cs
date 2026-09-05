public class Game
{
    private int noOfPlayers;
    private int noOfDice;
    private int noOfSnakes;
    private int noOfLadders;
    private Board board;
    private Queue<Player> playersQueue;
    private Dice dice;

    public Game()
    {
        noOfPlayers = 2;
        noOfDice = 1;
        noOfSnakes = 5;
        noOfLadders = 5;

        board = new Board(100);
        dice = new Dice(noOfDice);
        playersQueue = new Queue<Player>();

        AddPlayer("Player 1");
        AddPlayer("Player 2");

        initBoardObstacles();
    }

    public void StartGame()
    {
        Console.WriteLine("Starting the game with " + noOfPlayers + " players, " + noOfDice + " dice, " + noOfSnakes + " snakes and " + noOfLadders + " ladders.");

        while (true)
        {
            Player currentPlayer = playersQueue.Dequeue();
            Console.WriteLine(currentPlayer.Name + "'s turn. Current position: " + currentPlayer.Position);
            int roll = dice.Roll();
            Console.WriteLine(currentPlayer.Name + " rolled a " + roll);
            int newPosition = board.getNewPosition(currentPlayer, roll);
            currentPlayer.Position = newPosition;
            Console.WriteLine(currentPlayer.Name + " moved to position " + newPosition);

            if (newPosition == 100)
            {
                Console.WriteLine(currentPlayer.Name + " has won the game!");
                break;
            }

            playersQueue.Enqueue(currentPlayer);
        }

    }

    public void initBoardObstacles()
    {
        // Initialize snakes
        board.AddObstacle(ObstacleFactory.CreateObstacle(ObstacleType.Snake, 16, 6));
        board.AddObstacle(ObstacleFactory.CreateObstacle(ObstacleType.Snake, 47, 26));
        board.AddObstacle(ObstacleFactory.CreateObstacle(ObstacleType.Snake, 49, 11));
        board.AddObstacle(ObstacleFactory.CreateObstacle(ObstacleType.Snake, 56, 53));
        board.AddObstacle(ObstacleFactory.CreateObstacle(ObstacleType.Snake, 62, 19));

        // Initialize ladders
        board.AddObstacle(ObstacleFactory.CreateObstacle(ObstacleType.Ladder, 2, 38));
        board.AddObstacle(ObstacleFactory.CreateObstacle(ObstacleType.Ladder, 7, 14));
        board.AddObstacle(ObstacleFactory.CreateObstacle(ObstacleType.Ladder, 8, 31));
        board.AddObstacle(ObstacleFactory.CreateObstacle(ObstacleType.Ladder, 15, 26));
        board.AddObstacle(ObstacleFactory.CreateObstacle(ObstacleType.Ladder, 21, 42));
    }

    public void AddPlayer(string playerName)
    {
        playersQueue.Enqueue(new Player(playerName));
    }
}