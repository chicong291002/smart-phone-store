using ApiIntegration.Slides;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using ShoeStore.AdminApp.ApiIntegration.Products;
using SmartPhoneStore.AdminApp.ApiIntegration.Categories;
using SmartPhoneStore.AdminApp.ApiIntegration.Products;
using SmartPhoneStore.AdminApp.ApiIntegration.Users;
using System;
using LazZiya.ExpressLocalization;
using FluentValidation.AspNetCore;
using SmartPhoneStore.WebApp.LocalizationResources;
using SmartPhoneStore.ViewModels.System.Users.CheckUserValidator;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using SmartPhoneStore.WebApp.Models;
using Stripe;
using Westwind.AspNetCore.Markdown;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
var cultures = new[]
           {
                new CultureInfo("en"),
                new CultureInfo("vi"),
            };
// Add services to the container.
builder.Services.AddControllersWithViews()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>())
                .AddExpressLocalization<ExpressLocalizationResource, ViewLocalizationResource>(ops =>
                {
                    // When using all the culture providers, the localization process will
                    // check all available culture providers in order to detect the request culture.
                    // If the request culture is found it will stop checking and do localization accordingly.
                    // If the request culture is not found it will check the next provider by order.
                    // If no culture is detected the default culture will be used.

                    // Checking order for request culture:
                    // 1) RouteSegmentCultureProvider
                    //      e.g. http://localhost:1234/tr
                    // 2) QueryStringCultureProvider
                    //      e.g. http://localhost:1234/?culture=tr
                    // 3) CookieCultureProvider
                    //      Determines the culture information for a request via the value of a cookie.
                    // 4) AcceptedLanguageHeaderRequestCultureProvider
                    //      Determines the culture information for a request via the value of the Accept-Language header.
                    //      See the browsers language settings

                    // Uncomment and set to true to use only route culture provider
                    ops.UseAllCultureProviders = false;
                    ops.ResourcesPath = "LocalizationResources";
                    ops.RequestLocalizationOptions = o =>
                    {
                        o.SupportedCultures = cultures;
                        o.SupportedUICultures = cultures;
                        o.DefaultRequestCulture = new RequestCulture("vi");
                    };
                });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/vi/Login/Login"; // neu chua login thi Redirect ve day 
        options.AccessDeniedPath = "/User/Forbidden/";
    });

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<ISlideApiClient, SlideApiClient>();
builder.Services.AddTransient<IProductApiClient, ProductApiClient>();
builder.Services.AddTransient<ICategoryApiClient, CategoryApiClient>();
builder.Services.AddTransient<IUserApiClient, UserApiClient>();
builder.Services.AddTransient<ICouponApiClient, CouponApiClient>();
builder.Services.AddTransient<IOrderApiClient, OrderApiClient>();
builder.Services.AddMarkdown();
builder.Services.AddMvc()
     // have to let MVC know we have a controller
     .AddApplicationPart(typeof(MarkdownPageProcessorMiddleware).Assembly);
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
StripeConfiguration.ApiKey = "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";

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// This is your real test secret API key.
app.UseHttpsRedirection();
app.UseMarkdown();
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.UseRequestLocalization();

app.MapControllerRoute(
    name: "Product Category",
    pattern: "{culture}/categories/{id}",
    new
    {
        controller = "Product",
        action = "Category"
    });


app.MapControllerRoute(
   name: "Product Details",
    pattern: "{culture}/san-pham/{id}",
    new
    {
        controller = "Product",
        action = "Detail"
    });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
