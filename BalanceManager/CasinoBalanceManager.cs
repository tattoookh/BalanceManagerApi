using System.Collections.Generic;

namespace Balances
{
    // Token: 0x02000002 RID: 2
    public class CasinoBalanceManager : IBalanceManager
    {
        // Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
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
            object lockObject = CasinoBalanceManager._LockObject;
            lock (lockObject)
            {
                if (CasinoBalanceManager._Transactions.ContainsKey(transactionId))
                {
                    return ErrorCode.DuplicateTransactionId;
                }
                CasinoBalanceManager._Balance += amount;
                CasinoBalanceManager._Transactions[transactionId] = amount;
                if (Helper.RandomGenerator(0, 100) < 2)
                {
                    return ErrorCode.UnknownError;
                }
            }
            return ErrorCode.Success;
        }

        // Token: 0x06000003 RID: 3 RVA: 0x000020F4 File Offset: 0x000002F4
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
            object lockObject = CasinoBalanceManager._LockObject;
            lock (lockObject)
            {
                if (CasinoBalanceManager._Balance - amount < 0m)
                {
                    return ErrorCode.NotEnoughtBalance;
                }
                if (CasinoBalanceManager._Transactions.ContainsKey(transactionId))
                {
                    return ErrorCode.DuplicateTransactionId;
                }
                CasinoBalanceManager._Balance -= amount;
                CasinoBalanceManager._Transactions[transactionId] = -1m * amount;
                if (Helper.RandomGenerator(0, 100) < 2)
                {
                    return ErrorCode.UnknownError;
                }
            }
            return ErrorCode.Success;
        }

        // Token: 0x06000004 RID: 4 RVA: 0x000021B4 File Offset: 0x000003B4
        public ErrorCode Rollback(string transactionId)
        {
            if (Helper.RandomGenerator(0, 100) < 2)
            {
                return ErrorCode.UnknownError;
            }
            object lockObject = CasinoBalanceManager._LockObject;
            lock (lockObject)
            {
                if (!CasinoBalanceManager._Transactions.ContainsKey(transactionId))
                {
                    return ErrorCode.TransactionNotFound;
                }
                if (CasinoBalanceManager._Transactions[transactionId] == 0m)
                {
                    return ErrorCode.TransactionAlreadyMarkedAsRollback;
                }
                if (CasinoBalanceManager._Balance + CasinoBalanceManager._Transactions[transactionId] < 0m)
                {
                    return ErrorCode.NotEnoughtBalance;
                }
                CasinoBalanceManager._Balance += CasinoBalanceManager._Transactions[transactionId];
                CasinoBalanceManager._Transactions[transactionId] = 0m;
                if (Helper.RandomGenerator(0, 100) < 2)
                {
                    return ErrorCode.UnknownError;
                }
            }
            return ErrorCode.Success;
        }

        // Token: 0x06000005 RID: 5 RVA: 0x00002290 File Offset: 0x00000490
        public ErrorCode CheckTransaction(string transactionId)
        {
            object lockObject = CasinoBalanceManager._LockObject;
            lock (lockObject)
            {
                if (!CasinoBalanceManager._Transactions.ContainsKey(transactionId))
                {
                    return ErrorCode.TransactionNotFound;
                }
                if (CasinoBalanceManager._Transactions[transactionId] == 0m)
                {
                    return ErrorCode.TransactionRollbacked;
                }
            }
            return ErrorCode.Success;
        }

        // Token: 0x06000006 RID: 6 RVA: 0x000022FC File Offset: 0x000004FC
        public decimal GetBalance()
        {
            return CasinoBalanceManager._Balance;
        }

        // Token: 0x04000001 RID: 1
        private static object _LockObject = new object();

        // Token: 0x04000002 RID: 2
        private static decimal _Balance = 10000m;

        // Token: 0x04000003 RID: 3
        private static Dictionary<string, decimal> _Transactions = new Dictionary<string, decimal>();
    }
}
