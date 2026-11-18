using Microsoft.EntityFrameworkCore;
using olympic_assignment.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TicketContext>(options =>
    options.UseSqlite("Data Source=tickets.db"));

builder.Services.AddScoped<ITicketRepository, TicketRepository>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TicketContext>();
    db.Database.EnsureCreated();
}

app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public partial class Program { }