using CinemaApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CinemaApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // 1. Add movies
    if (!context.Movies.Any(m => m.Title == "Spider-Man: No Way Home"))
    {
        context.Movies.Add(
            new Movie
            {
                Title = "Spider-Man: No Way Home",
                Description = "Peter Parker seeks Doctor Strange’s help when his identity is revealed.",
                Genre = "Action / Sci-Fi",
                Director = "Jon Watts",
                ImageUrl = "/images/spiderman.jpg",
                TrailerUrl = "https://www.youtube.com/watch?v=JfVOs4VSpmA",
                ReleaseDate = DateTime.Now
            });
    }

    if (!context.Movies.Any(m => m.Title == "Avengers: Infinity War"))
    {
        context.Movies.Add(
            new Movie
            {
                Title = "Avengers: Infinity War",
                Description = "The Avengers assemble for a final battle against Thanos.",
                Genre = "Sci-Fi / Adventure",
                Director = "Anthony and Joe Russo",
                ImageUrl = "/images/avengers.jpg",
                TrailerUrl = "https://www.youtube.com/watch?v=6ZfuNTqbHE8",
                ReleaseDate = DateTime.Now.AddDays(20)
            });
    }

    context.SaveChanges();

    // 2. Add sessions
    if (!context.Sessions.Any())
    {
        var spiderman = context.Movies
            .First(m => m.Title.Contains("Spider-Man"));

        context.Sessions.AddRange(
            new Session
            {
                MovieId = spiderman.Id,
                StartTime = DateTime.Today.AddHours(10),
                Format = "3D",
                TicketPrice = 150
            },
            new Session
            {
                MovieId = spiderman.Id,
                StartTime = DateTime.Today.AddHours(13),
                Format = "3D",
                TicketPrice = 150
            },
            new Session
            {
                MovieId = spiderman.Id,
                StartTime = DateTime.Today.AddHours(18),
                Format = "IMAX",
                TicketPrice = 220
            }
        );

        context.SaveChanges();
    }
}


app.Run();
