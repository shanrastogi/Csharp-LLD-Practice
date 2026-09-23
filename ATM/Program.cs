using ATM.model;
using ATM.repository;
using ATM.service;

public class Program
{
    public static void Main(string[] args)
    {
        Card card = new Card(
               "CARD123",
               "1234",
               new Account("ACC123", 5000)
       );

        ATMModel atm1 = new ATMModel("ATM1", 5, 5, 20);
        ATMModel atm2 = new ATMModel("ATM2", 5, 2, 5);

        ATMRepository atmRepository = new ATMRepository();
        atmRepository.Save(atm1);
        atmRepository.Save(atm2);

        ATMMachine atmMachine2 = new ATMMachine("ATM2", atmRepository);

        atmMachine2.InsertCard(card);
        atmMachine2.EnterPin("1234");
        atmMachine2.SelectOption("WITHDRAW");
        atmMachine2.DispenseCash(2500);
    }
}