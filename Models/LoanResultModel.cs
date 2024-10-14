using System.Collections.Generic;

namespace LoanCalculator.Models
{
    public class LoanResultModel
    {
        public decimal MonthlyPayment { get; set; }    // Ежемесячный платеж
        public decimal TotalOverpayment { get; set; }  // Итоговая переплата
        public List<PaymentScheduleItem> Schedule { get; set; } // График платежей
    }

    public class PaymentScheduleItem
    {
        public int PaymentNumber { get; set; }         // Номер платежа
        public DateTime PaymentDate { get; set; }      // Дата платежа
        public decimal PrincipalPayment { get; set; }  // Платеж по телу
        public decimal InterestPayment { get; set; }   // Платеж по процентам
        public decimal RemainingBalance { get; set; }  // Остаток долга
    }
}
