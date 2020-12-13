using Balances;

namespace BalanceManagerApi.Implementation
{
    public interface IBalanceOperator
    {
        ErrorCode ChangeBalance(string transactionid, decimal amount);
    }
}
