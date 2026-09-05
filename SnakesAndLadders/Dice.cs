public class Dice
{
    private static Random random = new Random();
    private int noOfDice;

    public Dice(int noOfDice)
    {
        this.noOfDice = noOfDice;
    }

    public int Roll()
    {
        int total = 0;
        for (int i = 0; i < noOfDice; i++)
        {
            total += random.Next(1, 7); // Random number between 1 and 6
        }
        return total;
    }
}