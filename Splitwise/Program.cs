using Splitwise.enums;
using Splitwise.model;
using Splitwise.repository;
using Splitwise.service;

public class Program
{
    public static void Main(string[] args)
    {
        // Users
        User shubh = new User("u1", "Shubh");
        User bob = new User("u2", "Bob");
        User tom = new User("u3", "Tom");
        User jake = new User("u4", "Jake");

        InMemoryGroupRepository repo = new InMemoryGroupRepository();
        BalanceSheetService balanceSheetService = new BalanceSheetService();
        ExpenseService expenseService = new ExpenseService(balanceSheetService);
        DebtSimplificationService simplificationService = new DebtSimplificationService();

        GroupService groupService = new GroupService(repo, expenseService, simplificationService);

        /* ---------- Create Groups ---------- */
        string goaGroupId = groupService.CreateGroup("Goa Trip", new List<User> { shubh, bob, tom });
        string miscGroup = groupService.CreateGroup("Non-Group Expenses", new List<User> { shubh, bob, tom, jake });

        /* ---------- Add Expenses ---------- */
        groupService.AddExpense(goaGroupId,
            "Lunch Day-1", 100, shubh,
            new List<User> { shubh, bob }, SplitType.EQUAL, null);

        groupService.AddExpense(goaGroupId,
            "Lunch Day-2", 100, bob,
            new List<User> { bob, tom }, SplitType.EQUAL, null);

        /* ---------- Simplify & Print ---------- */
        //groupService.SimplifyDebts(goaGroupId);
        groupService.PrintBalances(goaGroupId);
    }
}