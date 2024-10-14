using LoanCalculator.Models;
using LoanCalculator.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoanCalculator.Controllers
{
    public class LoanController : Controller
    {
        private readonly LoanCalculatorService _loanCalculatorService;

        /// <summary>
        /// Инициализация сервиса
        /// </summary>
        public LoanController()
        {
            _loanCalculatorService = new LoanCalculatorService(); 
        }
        /// <summary>
        /// Возврат
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Вызов сервиса расчётов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Calculate(LoanInputModel model)
        {
            if (ModelState.IsValid)
            {
                LoanResultModel result = _loanCalculatorService.CalculateLoan(
                    model.LoanAmount,
                    model.LoanTerm,
                    model.InterestRate);

                return View("Result", result);
            }
            return View("Index", model);
        }
    }
}
