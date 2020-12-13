namespace Balances
{
    // Token: 0x02000006 RID: 6
    public interface IBalanceManager
    {
        // Token: 0x06000010 RID: 16
        ErrorCode IncreaseBalance(decimal amount, string transactionId);

        // Token: 0x06000011 RID: 17
        ErrorCode DecreaseBalance(decimal amount, string transactionId);

        // Token: 0x06000012 RID: 18
        ErrorCode Rollback(string transactionId);

        // Token: 0x06000013 RID: 19
        ErrorCode CheckTransaction(string transactionId);

        // Token: 0x06000014 RID: 20
        decimal GetBalance();
    }
}
