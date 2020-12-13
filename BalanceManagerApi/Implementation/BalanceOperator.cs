using Balances;

namespace BalanceManagerApi.Implementation
{
    public class BalanceOperator : IBalanceOperator
    {
        private readonly IBalanceManager _from;
        private readonly IBalanceManager _to;

        public BalanceOperator(IBalanceManager from, IBalanceManager to)
        {
            _from = from;
            _to = to;
        }

        public ErrorCode ChangeBalance(string transactionid, decimal amount)
        {
            if (_from.GetBalance() < amount)
            {
                return ErrorCode.NotEnoughtBalance;
            }

            var fromTransactionResult = _from.DecreaseBalance(amount, transactionid);

            if (fromTransactionResult != ErrorCode.Success)
            {
                return fromTransactionResult;
            }

            var toTransactionResult = _to.IncreaseBalance(amount, transactionid);

            if (toTransactionResult != ErrorCode.Success)
            {
                _from.Rollback(transactionid);
            }

            return toTransactionResult;
        }
    }
}
