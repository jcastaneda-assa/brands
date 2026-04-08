using Microsoft.EntityFrameworkCore;
using Brands.Data;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext service.
builder.Services.AddDbContext<BrandsContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")) // Get the connection string from appsettings.json
);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BrandsContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
