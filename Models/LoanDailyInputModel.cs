using System.ComponentModel.DataAnnotations;

namespace LoanCalculator.Models
{
    /// <summary>
    /// Сбор данных для второй странницы
    /// </summary>
    public class LoanDailyInputModel
    {
        [Required(ErrorMessage = "Введите сумму займа")]
        [Range(1000, 10000000, ErrorMessage = "Сумма займа должна быть в диапазоне от 1000 до 10 000 000")]
        public decimal LoanAmount { get; set; }

        [Required(ErrorMessage = "Введите срок займа в днях")]
        [Range(1, 36500, ErrorMessage = "Срок займа должен быть в диапазоне от 1 до 36500 дней")]
        public int LoanTermDays { get; set; }

        [Required(ErrorMessage = "Введите дневную процентную ставку")]
        [Range(0.001, 100, ErrorMessage = "Процентная ставка должна быть в диапазоне от 0.001% до 100%")]
        public decimal DailyInterestRate { get; set; }

        [Required(ErrorMessage = "Введите шаг платежа в днях")]
        [Range(1, 365, ErrorMessage = "Шаг платежа должен быть в диапазоне от 1 до 365 дней")]
        public int PaymentStepDays { get; set; }
    }
}