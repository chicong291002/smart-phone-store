using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SmartPhoneStore.AdminApp.ApiIntegration.Categories;
using SmartPhoneStore.AdminApp.ApiIntegration.Products;
using SmartPhoneStore.AdminApp.ApiIntegration.Roles;
using SmartPhoneStore.AdminApp.ApiIntegration.Users;
using SmartPhoneStore.ViewModels.System.Users.CheckUserValidator;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Login/Index"; // neu chua login thi Redirect ve day 
            options.AccessDeniedPath = "/User/Forbidden/";
        });

builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation(
    fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());

builder.Services.AddTransient<IUserApiClient, UserApiClient>();
builder.Services.AddTransient<IRoleApiClient, RoleApiClient>();
builder.Services.AddTransient<IProductApiClient,ProductApiClient>();
builder.Services.AddTransient<ICategoryApiClient, CategoryApiClient>();

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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

app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
