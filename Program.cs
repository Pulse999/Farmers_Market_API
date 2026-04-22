var builder = WebApplication.CreateBuilder(args);

// ✅ Enable MVC (Views) + API
builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles(); // ✅ Required for views

app.UseRouting();

app.UseAuthorization();

// ✅ This enables homepage "/"
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// ✅ Keep API working
app.MapControllers();

app.Run();