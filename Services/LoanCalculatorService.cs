using System;
using System.Collections.Generic;
using LoanCalculator.Models;

namespace LoanCalculator.Services
{
    public class LoanCalculatorService
    {
        /// <summary>
        /// Сервис для расчёта. Сначала идёт расчёт месячной процентной ставки, затем аннуитетного коэфицента, затем цикл расчитывает каждый платёж.
        /// </summary>
        /// <param name="loanAmount"></param>
        /// <param name="loanTerm"></param>
        /// <param name="interestRate"></param>
        /// <returns></returns>
        public LoanResultModel CalculateLoan(decimal loanAmount, int loanTerm, decimal interestRate)
        {
            var result = new LoanResultModel();
            result.Schedule = new List<PaymentScheduleItem>();

            decimal monthlyRate = interestRate / 12 / 100;  
            int totalMonths = loanTerm;

            decimal annuityFactor = (monthlyRate * (decimal)Math.Pow((double)(1 + monthlyRate), totalMonths)) /
                                    (decimal)(Math.Pow((double)(1 + monthlyRate), totalMonths) - 1);

            result.MonthlyPayment = loanAmount * annuityFactor;
            decimal remainingBalance = loanAmount;
            
            for (int month = 1; month <= totalMonths; month++)
            {
                decimal interestPayment = remainingBalance * monthlyRate;
                decimal principalPayment = result.MonthlyPayment - interestPayment;
                remainingBalance -= principalPayment;

                result.Schedule.Add(new PaymentScheduleItem
                {
                    PaymentNumber = month,
                    PaymentDate = DateTime.Now.AddMonths(month),
                    PrincipalPayment = principalPayment,
                    InterestPayment = interestPayment,
                    RemainingBalance = remainingBalance
                });
            }

            result.TotalOverpayment = result.MonthlyPayment * totalMonths - loanAmount;
            return result;
        }
    }
}
