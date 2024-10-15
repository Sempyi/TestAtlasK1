using LoanCalculator.Models;

namespace LoanCalculator.Services
{
    /// <summary>
    /// Как модный парень - сделал интерфейс
    /// </summary>
    public interface ILoanCalculatorService
    {
        LoanResultModel CalculateLoan(decimal loanAmount, int loanTermMonths, decimal annualInterestRate);
        LoanResultModel CalculateLoanInDays(decimal loanAmount, int loanTermDays, decimal dailyInterestRate, int paymentStepDays);
    }
}
