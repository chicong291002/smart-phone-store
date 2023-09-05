using FluentValidation.AspNetCore;
using ShoeStore.AdminApp.Services;
using ShoeStore.Application.System.Users.DTOS;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddTransient<IUserApiClient, UserApiClient>();
// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation(
    fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());

var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIROMENT");

#if DEBUG
if (enviroment == Environments.Development)
{
    builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
}

#endif
//when update information code => system automatic update 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.Run();
