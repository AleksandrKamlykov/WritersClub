using Microsoft.EntityFrameworkCore;
using WritersClub.Data;
using WritersClub.Interfaces;
using WritersClub.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationContext>(opts => {
    opts.UseSqlServer(
    builder.Configuration["ConnectionStrings:DefaultConnection"]);
});
builder.Services.AddScoped<IUser, UserRepository>();
//builder.Services.AddScoped<IBook, BookRepository>();
builder.Services.AddScoped<IGenre, GenreRepository>();
var app = builder.Build();



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();