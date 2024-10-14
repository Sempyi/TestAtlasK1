using LoanCalculator.Services;

var builder = WebApplication.CreateBuilder(args);

// Добавление служб в контейнер.
builder.Services.AddControllersWithViews();

// Регистрация LoanCalculatorService в контейнере служб.
builder.Services.AddSingleton<LoanCalculatorService>();

var app = builder.Build();

// Конфигурация HTTP-конвейера.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Loan}/{action=Index}/{id?}");

app.Run();