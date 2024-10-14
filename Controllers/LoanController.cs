using LoanCalculator.Models;
using LoanCalculator.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoanCalculator.Controllers
{
    public class LoanController : Controller
    {
        private readonly LoanCalculatorService _loanCalculatorService;

        public LoanController()
        {
            _loanCalculatorService = new LoanCalculatorService(); // Инициализация сервиса
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

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
