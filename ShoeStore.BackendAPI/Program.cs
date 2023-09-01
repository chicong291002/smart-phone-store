using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using ShoeStore.Application.Catalog.Products.Public;
using ShoeStore.Data.EF;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Declare DI
builder.Services.AddTransient<IPublicProductService, PublicProductService>();

builder.Services.AddSwaggerGen(c =>
 {
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger Shoe Store", Version = "v1" });
 });
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ShoeStoreDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ShoeStoreDb")));

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

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Shoe Store V1");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
