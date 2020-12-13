using BalanceManagerApi.Implementation;
using Balances;
using Microsoft.AspNetCore.Mvc;

namespace BalanceManagerApi.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceManager _casinoBalanceManager;
        private readonly IBalanceManager _gameBalanceManager;

        public BalanceController()
        {
            _casinoBalanceManager = new CasinoBalanceManager();
            _gameBalanceManager = new GameBalanceManager();
        }

        [HttpGet]
        public decimal Balance() => _casinoBalanceManager.GetBalance();

        [HttpPost("{transactionId}/{amount}")]
        public ErrorCode Withdraw(string transactionId, decimal amount)
        {
            var balanceOperator = new BalanceOperator(_casinoBalanceManager, _gameBalanceManager);

            return balanceOperator.ChangeBalance(transactionId, amount);
        }

        [HttpPost("{transactionId}/{amount}")]
        public ErrorCode Deposit(string transactionId, decimal amount)
        {
            var balanceOperator = new BalanceOperator(_gameBalanceManager, _casinoBalanceManager);

            return balanceOperator.ChangeBalance(transactionId, amount);
        }
    }
}
