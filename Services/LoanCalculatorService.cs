using LoanCalculator.Models;
using System;
using System.Collections.Generic;

namespace LoanCalculator.Services
{
    /// <summary>
    /// Метод для расчета аннуитетных платежей на основе месячных данных
    /// </summary>
    public class LoanCalculatorService : ILoanCalculatorService
    {
        public LoanResultModel CalculateLoan(decimal loanAmount, int loanTermMonths, decimal annualInterestRate)
        {
            decimal monthlyInterestRate = annualInterestRate / 100 / 12;
            decimal monthlyPayment = loanAmount * monthlyInterestRate / (1 - (decimal)Math.Pow(1 + (double)monthlyInterestRate, -loanTermMonths));

            var result = new LoanResultModel
            {
                MonthlyPayment = monthlyPayment,
                Schedule = GenerateMonthlyPaymentSchedule(loanAmount, loanTermMonths, monthlyInterestRate, monthlyPayment)
            };

            result.TotalOverpayment = result.Schedule.Sum(s => s.InterestPayment);

            return result;
        }

        /// <summary>
        /// Метод для расчета аннуитетных платежей на основе дневных данных
        /// </summary>
        /// <param name="loanAmount"></param>
        /// <param name="loanTermDays"></param>
        /// <param name="dailyInterestRate"></param>
        /// <param name="paymentStepDays"></param>
        /// <returns></returns>
        public LoanResultModel CalculateLoanInDays(decimal loanAmount, int loanTermDays, decimal dailyInterestRate, int paymentStepDays)
        {
            decimal dailyRate = dailyInterestRate / 100;
            int numberOfPayments = loanTermDays / paymentStepDays;

            decimal dailyPayment = loanAmount * dailyRate / (1 - (decimal)Math.Pow(1 + (double)dailyRate, -numberOfPayments));

            var result = new LoanResultModel
            {
                MonthlyPayment = dailyPayment, 
                Schedule = GenerateDailyPaymentSchedule(loanAmount, loanTermDays, dailyRate, paymentStepDays, dailyPayment)
            };

            result.TotalOverpayment = result.Schedule.Sum(s => s.InterestPayment);

            return result;
        }
        /// <summary>
        /// Метод для генерации графика платежей (месяцы)
        /// </summary>
        /// <param name="loanAmount"></param>
        /// <param name="loanTermMonths"></param>
        /// <param name="monthlyInterestRate"></param>
        /// <param name="monthlyPayment"></param>
        /// <returns></returns>
        private List<PaymentScheduleItem> GenerateMonthlyPaymentSchedule(decimal loanAmount, int loanTermMonths, decimal monthlyInterestRate, decimal monthlyPayment)
        {
            var schedule = new List<PaymentScheduleItem>();
            decimal remainingBalance = loanAmount;

            for (int i = 1; i <= loanTermMonths; i++)
            {
                decimal interestPayment = remainingBalance * monthlyInterestRate;
                decimal principalPayment = monthlyPayment - interestPayment;
                remainingBalance -= principalPayment;

                schedule.Add(new PaymentScheduleItem
                {
                    PaymentNumber = i,
                    PaymentDate = DateTime.Now.AddMonths(i),
                    PrincipalPayment = principalPayment,
                    InterestPayment = interestPayment,
                    RemainingBalance = remainingBalance
                });
            }

            return schedule;
        }
        /// <summary>
        /// Метод для генерации графика платежей (дни)
        /// </summary>
        /// <param name="loanAmount"></param>
        /// <param name="loanTermDays"></param>
        /// <param name="dailyInterestRate"></param>
        /// <param name="paymentStepDays"></param>
        /// <param name="dailyPayment"></param>
        /// <returns></returns>
        private List<PaymentScheduleItem> GenerateDailyPaymentSchedule(decimal loanAmount, int loanTermDays, decimal dailyInterestRate, int paymentStepDays, decimal dailyPayment)
        {
            var schedule = new List<PaymentScheduleItem>();
            decimal remainingBalance = loanAmount;

            for (int i = 1; i <= loanTermDays / paymentStepDays; i++)
            {
                decimal interestPayment = remainingBalance * dailyInterestRate;
                decimal principalPayment = dailyPayment - interestPayment;
                remainingBalance -= principalPayment;

                schedule.Add(new PaymentScheduleItem
                {
                    PaymentNumber = i,
                    PaymentDate = DateTime.Now.AddDays(i * paymentStepDays),
                    PrincipalPayment = principalPayment,
                    InterestPayment = interestPayment,
                    RemainingBalance = remainingBalance
                });
            }

            return schedule;
        }
    }
}
