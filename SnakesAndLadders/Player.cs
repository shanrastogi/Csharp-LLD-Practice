public class Player
{
    public string Name { get; private set; }
    public int Position { get; set; }

    public Player(string name)
    {
        Name = name;
        Position = 1; // Starting position
    }
}