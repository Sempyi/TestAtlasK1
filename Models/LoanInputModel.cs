using System.ComponentModel.DataAnnotations;

namespace LoanCalculator.Models
{
    /// <summary>
    /// Сбор данных
    /// </summary>
    public class LoanInputModel
    {
        [Required(ErrorMessage = "Введите сумму займа")]
        [Range(1000, 10000000, ErrorMessage = "Сумма займа должна быть в диапазоне от 1000 до 10 000 000")]
        public decimal LoanAmount { get; set; }

        [Required(ErrorMessage = "Введите срок займа в месяцах")]
        [Range(1, 360, ErrorMessage = "Срок займа должен быть в диапазоне от 1 до 360 месяцев")]
        public int LoanTerm { get; set; }

        [Required(ErrorMessage = "Введите годовую процентную ставку")]
        [Range(0.1, 100, ErrorMessage = "Процентная ставка должна быть в диапазоне от 0.1% до 100%")]
        public decimal InterestRate { get; set; }
    }
}
