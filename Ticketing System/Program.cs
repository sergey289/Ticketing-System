using Ticketing_Systems.Data;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddControllers(); // Регистрация контроллеров


// Add services to the container.
builder.Services.AddRazorPages();


// Add services to the container.
builder.Services.AddSingleton<SQL>();

builder.Services.AddTransient<TicketingSystemService.TicketingSystemService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapGet("/", () => Results.Redirect("/Main"));

app.MapControllers();

app.Run();
