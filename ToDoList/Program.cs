using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ToDoList.Data;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowAll",
//         policy => policy.AllowAnyOrigin()
//             .AllowAnyMethod()
//             .AllowAnyHeader());
// });



DotNetEnv.Env.Load();

var host = Environment.GetEnvironmentVariable("POSTGRES_HOST");
var database = Environment.GetEnvironmentVariable("POSTGRES_DB");
var user = Environment.GetEnvironmentVariable("POSTGRES_USER");
var password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
var port = Environment.GetEnvironmentVariable("POSTGRES_PORT");
var connectionString = $"Host={host};Port={port};Username={user};Password={password};Database={database};";
builder.Services.AddDbContext<TodoContext>(options =>
{
    options.UseNpgsql(connectionString ?? throw new InvalidOperationException());
    
});


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<TodoContext>();
builder.Services.AddControllersWithViews();


builder.Services.AddControllers();
builder.Services.AddOpenApi();


var app = builder.Build();

// app.UseCors("AllowAll");



if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("ToDo List API")
            .WithTheme(ScalarTheme.DeepSpace);
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseDefaultFiles();
app.UseStaticFiles();


app.MapControllers();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
    .WithStaticAssets();

app.Run();