using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FormularioTeste.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FormularioTesteContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FormularioTesteContext") ?? throw new InvalidOperationException("Connection string 'FormularioTesteContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Rota específica para o controller Estabelecimentos
app.MapControllerRoute(
    name: "estabelecimentos",
    pattern: "{controller=Estabelecimentos}/{action=Index}/{id?}");

// Rota padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
