using System;
using System.Collections.Generic;
using LoanCalculator.Models;

namespace LoanCalculator.Services
{
    public class LoanCalculatorService
    {
        public LoanResultModel CalculateLoan(decimal loanAmount, int loanTerm, decimal interestRate)
        {
            var result = new LoanResultModel();
            result.Schedule = new List<PaymentScheduleItem>();

            decimal monthlyRate = interestRate / 12 / 100;  // Месячная процентная ставка
            int totalMonths = loanTerm;

            // Расчет аннуитетного коэффициента
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
