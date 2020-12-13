namespace Balances
{
    // Token: 0x02000003 RID: 3
    public enum ErrorCode
    {
        // Token: 0x04000005 RID: 5
        Success = 10,
        // Token: 0x04000006 RID: 6
        TransactionRejected,
        // Token: 0x04000007 RID: 7
        NotEnoughtBalance,
        // Token: 0x04000008 RID: 8
        DuplicateTransactionId,
        // Token: 0x04000009 RID: 9
        TransactionNotFound,
        // Token: 0x0400000A RID: 10
        TransactionAlreadyMarkedAsRollback,
        // Token: 0x0400000B RID: 11
        TransactionRollbacked,
        // Token: 0x0400000C RID: 12
        UnknownError = 111
    }
}
