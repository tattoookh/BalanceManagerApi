using System.Collections.Generic;

namespace Balances
{
    // Token: 0x02000004 RID: 4
    public class GameBalanceManager : IBalanceManager
    {
        // Token: 0x06000009 RID: 9 RVA: 0x00002330 File Offset: 0x00000530
        public ErrorCode IncreaseBalance(decimal amount, string transactionId)
        {
            if (Helper.RandomGenerator(0, 100) < 2)
            {
                return ErrorCode.UnknownError;
            }
            if (amount <= 0m)
            {
                return ErrorCode.TransactionRejected;
            }
            object lockObject = GameBalanceManager._LockObject;
            lock (lockObject)
            {
                if (GameBalanceManager._Transactions.ContainsKey(transactionId))
                {
                    return ErrorCode.DuplicateTransactionId;
                }
                GameBalanceManager._Balance += amount;
                GameBalanceManager._Transactions[transactionId] = amount;
                if (Helper.RandomGenerator(0, 100) < 2)
                {
                    return ErrorCode.UnknownError;
                }
            }
            return ErrorCode.Success;
        }

        // Token: 0x0600000A RID: 10 RVA: 0x000023CC File Offset: 0x000005CC
        public ErrorCode DecreaseBalance(decimal amount, string transactionId)
        {
            if (Helper.RandomGenerator(0, 100) < 2)
            {
                return ErrorCode.UnknownError;
            }
            if (amount <= 0m)
            {
                return ErrorCode.TransactionRejected;
            }
            object lockObject = GameBalanceManager._LockObject;
            lock (lockObject)
            {
                if (GameBalanceManager._Balance - amount < 0m)
                {
                    return ErrorCode.NotEnoughtBalance;
                }
                if (GameBalanceManager._Transactions.ContainsKey(transactionId))
                {
                    return ErrorCode.DuplicateTransactionId;
                }
                GameBalanceManager._Balance -= amount;
                GameBalanceManager._Transactions[transactionId] = -1m * amount;
                if (Helper.RandomGenerator(0, 100) < 2)
                {
                    return ErrorCode.UnknownError;
                }
            }
            return ErrorCode.Success;
        }

        // Token: 0x0600000B RID: 11 RVA: 0x0000248C File Offset: 0x0000068C
        public ErrorCode Rollback(string transactionId)
        {
            if (Helper.RandomGenerator(0, 100) < 2)
            {
                return ErrorCode.UnknownError;
            }
            object lockObject = GameBalanceManager._LockObject;
            lock (lockObject)
            {
                if (!GameBalanceManager._Transactions.ContainsKey(transactionId))
                {
                    return ErrorCode.TransactionNotFound;
                }
                if (GameBalanceManager._Transactions[transactionId] == 0m)
                {
                    return ErrorCode.TransactionAlreadyMarkedAsRollback;
                }
                if (GameBalanceManager._Balance + GameBalanceManager._Transactions[transactionId] < 0m)
                {
                    return ErrorCode.NotEnoughtBalance;
                }
                GameBalanceManager._Balance += GameBalanceManager._Transactions[transactionId];
                GameBalanceManager._Transactions[transactionId] = 0m;
                if (Helper.RandomGenerator(0, 100) < 2)
                {
                    return ErrorCode.UnknownError;
                }
            }
            return ErrorCode.Success;
        }

        // Token: 0x0600000C RID: 12 RVA: 0x00002568 File Offset: 0x00000768
        public ErrorCode CheckTransaction(string transactionId)
        {
            object lockObject = GameBalanceManager._LockObject;
            lock (lockObject)
            {
                if (!GameBalanceManager._Transactions.ContainsKey(transactionId))
                {
                    return ErrorCode.TransactionNotFound;
                }
                if (GameBalanceManager._Transactions[transactionId] == 0m)
                {
                    return ErrorCode.TransactionRollbacked;
                }
            }
            return ErrorCode.Success;
        }

        // Token: 0x0600000D RID: 13 RVA: 0x000025D4 File Offset: 0x000007D4
        public decimal GetBalance()
        {
            return GameBalanceManager._Balance;
        }

        // Token: 0x0400000D RID: 13
        private static object _LockObject = new object();

        // Token: 0x0400000E RID: 14
        private static decimal _Balance = 10000m;

        // Token: 0x0400000F RID: 15
        private static Dictionary<string, decimal> _Transactions = new Dictionary<string, decimal>();
    }
}
