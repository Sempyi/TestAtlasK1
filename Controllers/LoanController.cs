using LoanCalculator.Models;
using LoanCalculator.Services;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Прикрутил интерфейс и вторую странничку
/// </summary>
public class LoanController : Controller
{
    private readonly ILoanCalculatorService _loanCalculatorService;

    public LoanController(ILoanCalculatorService loanCalculatorService)
    {
        _loanCalculatorService = loanCalculatorService;
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
            var result = _loanCalculatorService.CalculateLoan(
                model.LoanAmount,
                model.LoanTerm,
                model.InterestRate);

            return View("Result", result);
        }
        return View("Index", model);
    }
    [HttpGet]
    public IActionResult DailyLoan()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CalculateDaily(LoanDailyInputModel model)
    {
        if (ModelState.IsValid)
        {
            LoanResultModel result = _loanCalculatorService.CalculateLoanInDays(
                model.LoanAmount,
                model.LoanTermDays,
                model.DailyInterestRate,
                model.PaymentStepDays);

            return View("Result", result);
        }
        return View("DailyLoan", model);
    }

}