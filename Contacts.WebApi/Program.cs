using Contacts.WebApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(option =>
                            option.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();  // If we try to access web API with HTTP it redirect to HTTPS

//  Get Contacts Endpoint
app.MapGet("/api/contacts", async (ApplicationDbContext db) =>
{
    var contacts = await db.Contacts.ToListAsync();
    return Results.Ok(contacts);
});

app.Run();
