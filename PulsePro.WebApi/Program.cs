using Microsoft.EntityFrameworkCore;       
using PulsePro.WebApi;
using PulsePro.Application.Mappers;
using PulsePro.Application.Abstraction;
using PulsePro.Application.Services;
using PulsePro.Persistence.Data;
using PulsePro.Persistence.Security;
using PulsePro.Persistence.Seeds;          

var builder = WebApplication.CreateBuilder(args);

// ------------------------------------
// Swagger / MVC
builder.Services.AddOpenApi().ConfigureWebApi();

// ------------------------------------
// Mapperly
builder.Services.AddSingleton<ApplicationMapper>();

// ------------------------------------
// Інфраструктурні сервіси
builder.Services.AddSingleton<IClock, SystemClock>();
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddScoped<ITokenGenerator, JwtGenerator>();

// ------------------------------------
// DbContext  (рядок з  appsettings.json → "Default")
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IApplicationDbContext>(sp =>
    sp.GetRequiredService<ApplicationDbContext>());

// ------------------------------------
// Application-сервіси
builder.Services.AddScoped<IUserService,         UserService>();
builder.Services.AddScoped<ITrainingPlanService, TrainingPlanService>();
builder.Services.AddScoped<INutritionDayService, NutritionDayService>();
builder.Services.AddScoped<IProgressService,     ProgressService>();

var app = builder.Build();

// ------------------------------------
// APPLY MIGRATIONS + ONE-TIME SEED
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();                 // створить БД та застосує всі міграції
    await DbSeeder.SeedAsync(db);          // заповнить один раз, якщо БД порожня
}

// ------------------------------------
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();
