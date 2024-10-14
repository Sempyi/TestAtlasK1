using System.ComponentModel.DataAnnotations; // Нужно для валидации

namespace LoanCalculator.Models
{
    public class LoanInputModel
    {
        public decimal LoanAmount { get; set; }  // Сумма займа
        public int LoanTerm { get; set; }        // Срок займа (в месяцах)
        public decimal InterestRate { get; set; } // Процентная ставка (в год)
    }
}