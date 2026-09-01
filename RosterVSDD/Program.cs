var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<RosterVSDD.Services.IRosterService, RosterVSDD.Services.InMemoryRosterService>();
// Ensure DI registration exists for IRosterService (no-op update to unify file paths)

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

// Minimal API endpoint for JSON feed
app.MapGet("/api/roster", (RosterVSDD.Services.IRosterService svc) =>
{
    var entries = svc.GetAll();
    var json = System.Text.Json.JsonSerializer.Serialize(entries);
    return Results.Content(json, "application/json");
});

app.Run();
